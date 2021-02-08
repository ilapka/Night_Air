using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfSubjects { Enemy, Player };

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] int Damage;
    [SerializeField] TypeOfSubjects whoSwordHurts;
    public List<BaseStat> Stats { get; set; }
    public int CurrentDamage { get; set; }

    private BoxCollider bladeCollider;
    [SerializeField] TrailRenderer attackTrail;

    void Start()
    {
        bladeCollider = GetComponent<BoxCollider>();
        bladeCollider.enabled = false;
        attackTrail.enabled = false;
        CurrentDamage = Damage;
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
    }

    public void PerformSpecialAttack(int damage)
    {
        CurrentDamage = damage + 10;
    }

    public void AttackStart()
    {
        bladeCollider.enabled = true;
        attackTrail.enabled = true;
    }

    public void AttackEnd()
    {
        bladeCollider.enabled = false;
        attackTrail.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log($"sword attack the {col.name}");
        if (col.CompareTag(whoSwordHurts.ToString()))
        {
            col.GetComponent<ITakeDamage>().TakeDamage(CurrentDamage);
        }
    }

}
