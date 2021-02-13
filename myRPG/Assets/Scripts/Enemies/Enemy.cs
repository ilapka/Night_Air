using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] LayerMask aggroLayerMask;
    [SerializeField] float aggroRange = 10f;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] string dropItemSlug;
    [SerializeField] ParticleSystem bloodParticleSystem;
    [SerializeField] float delayBeforeDeath = 1f;
    [SerializeField] public int ID;
    [SerializeField] public int Experience;

    public Spawner Spawner { get; set; }
    public Vector3 SpawnAnchor { get; set; }

    protected Animator enemyAnim;
    protected NavMeshAgent agent;
    protected Collider[] withinAggroCollider;
    protected Player player;
    protected bool isAttacking = true;
    protected bool isDead = false;
    protected DropTable DropTable { get; set; }


    protected virtual void Start()
    {
        InitEnemy();
        InitDropItems();
    }

    void InitEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        SpawnAnchor = transform.position;
        enemyAnim.SetTrigger("Start");
    }

    void InitDropItems()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop(dropItemSlug, 80)
        };
    }


    #region Chasing, attacks and return to spawn
    void FixedUpdate()
    {
        if (!isAttacking && !isDead)
        {
            ObserveTerritory();
        }
    }

    void ObserveTerritory()
    {
        withinAggroCollider = Physics.OverlapSphere(transform.position, aggroRange, aggroLayerMask);
        if (withinAggroCollider.Length > 0)
        {
            ChasePlayer(withinAggroCollider[0].GetComponent<Player>());
        }
        else
        {
            //ReturnToSpawn or stop 
            PlayerNotFound();
        }
    }

    void PlayerNotFound()
    {
        if (Vector3.Distance(transform.position, SpawnAnchor) >= agent.stoppingDistance)
        {
            ReturnToSpawn();
        }
        else
        {
            enemyAnim.SetFloat("Speed", 0);
        }
    }

    void ReturnToSpawn()
    {
        enemyAnim.SetFloat("Speed", 1f);
        agent.SetDestination(SpawnAnchor);
    }

    void ChasePlayer(Player player)
    {
        agent.SetDestination(player.transform.position);
        enemyAnim.SetFloat("Speed", 1);
        this.player = player;

        if (!IsInvoking("PerformAttack"))
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Debug.Log(agent.remainingDistance);
                    OnAttack();
                    Invoke("PerformAttack", 0.4f);
                }
            }
        }
    }

    void PerformAttack()
    {
        if (!isDead)
        {
            enemyAnim.SetTrigger("Attack1");
            isAttacking = true;
            agent.SetDestination(transform.position);
            EnsureLookDirection();
        }
    }

    void EnsureLookDirection()
    {
        Vector3 lookDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        agent.transform.LookAt(lookDirection);
    }
    #endregion


    #region Animator events
    public virtual void OnAttack()
    {
        //some action onAttack 
    }

    public virtual void OffAttack()
    {
        isAttacking = false;
    }
    #endregion


    #region Damage and death of the enemy
    public void TakeDamage(int amount)
    {
        FloatingTextController.Instance.CreateFloatingText(amount.ToString(), transform, false);
        bloodParticleSystem.Play();
        currentHealth -= amount;

        if (currentHealth < 0)
            Die();
    }

    public void Die()
    {
        PreparationForDeath();
        DropLoot();
        OnDeath();
        Destroy(gameObject, delayBeforeDeath);
    }

    void PreparationForDeath()
    {
        isDead = true;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        FloatingTextController.Instance.CreateFloatingText("Kill", transform, true);
        enemyAnim.SetTrigger("Die");
        CombatEvents.EnemyDied(this);
        this.Spawner.Respawn();
    }

    public virtual void OnDeath()
    {
        //Some special action for inheritor
    }

    protected void DropLoot()
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            PickupItem pickupItem = Resources.Load<PickupItem>($"DropItems/{item.ObjectSlug}");
            PickupItem instance = Instantiate(pickupItem, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
    #endregion
}
