using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manage equipment/unequip and weapon strikes 
/// </summary>
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] GameObject playerHand;
    private GameObject EquippedWeapon;
    private Transform spawnProjectile;
    private Item equippedItem;
    private CharacterStats characterStats;
    private Animator playerAnimator;
    private IWeapon equippedWeapon;
    private bool isAttack = false;
    private bool iProjectile = false;

    private void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().CharacterStats;
        playerAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Method loads the prefab from the resources, assigns it the appropriate fields of the "ItemToEquip" argument
    /// and adds it to the hand
    /// </summary>
    /// <param name="ItemToEquip"></param>
    public void EquipWeapon(Item ItemToEquip)
    {
        if (EquippedWeapon != null)
        {
            UnequipWeapon();
        }

        EquippedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + ItemToEquip.ObjectSlug), 
            playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponentInChildren<IWeapon>();

        if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        {
            EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
            iProjectile = true;
        }

        equippedWeapon.Stats = ItemToEquip.Stats;
        EquippedWeapon.transform.SetParent(playerHand.transform);
        characterStats.AddStatBonus(ItemToEquip.Stats);
        equippedItem = ItemToEquip;
        UIEventHandler.ItemEquipped(ItemToEquip);
        UIEventHandler.StatsChanged();
    }

    /// <summary>
    /// Puts the item back into inventory
    /// </summary>
    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(equippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(equippedWeapon.Stats);
        Destroy(playerHand.transform.GetChild(0).gameObject);
        UIEventHandler.StatsChanged();
        iProjectile = false;
    }

    void Update()
    {
        if (!isAttack && EquippedWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                ChooseAttack();
        }
    }

    private void ChooseAttack()
    {
        if (iProjectile)
        {
            PerformShootGun();
        }
        else
        {
            if (Random.value >= 0.5)
                PerforSwordAttack();
            else
                PerformSwordSpecialAttack();
        }
    }

    private void PerformShootGun()
    {
        equippedWeapon.PerformAttack(CalculateDamage());
        playerAnimator.SetTrigger("ShootGun");
    }

    private void PerforSwordAttack()
    {
        equippedWeapon.PerformAttack(CalculateDamage());
        playerAnimator.SetTrigger("SwordAttack1");
    }

    private void PerformSwordSpecialAttack()
    {
        equippedWeapon.PerformSpecialAttack(CalculateDamage());
        playerAnimator.SetTrigger("SwordAttack2");
    }

    public void AttackStart()
    {
        equippedWeapon.AttackStart();
        isAttack = true;
    }

    public void AttackEnd()
    {
        equippedWeapon.AttackEnd();
        isAttack = false;
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() * 2) +
            Random.Range(2, 8);         //for varied slightly
        damageToDeal += CalculateCrit(damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if (Random.value <= characterStats.GetStat(BaseStat.BaseStatType.СritСhance).GetCalculatedStatValue() / 100f)   //crit-chance
        {
            int critDamage = (int) (damage * Random.Range(.25f, 5f));
            return critDamage;
        }

        return 0;
    }
}
