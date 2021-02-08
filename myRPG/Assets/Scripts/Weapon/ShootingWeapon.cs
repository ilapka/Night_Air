using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : MonoBehaviour, IWeapon, IProjectileWeapon
{
    public List<BaseStat> Stats { get; set; }
    public Transform ProjectileSpawn { get; set; }
    public int CurrentDamage { get; set; }
    public enum TypeOfProjectile { Bullet, Fireball };

    [SerializeField] TypeOfProjectile typeOfProjectile;
    private Projectile projectile;

    void Start()
    {
        projectile = Resources.Load<Projectile>($"Weapons/Projectiles/{typeOfProjectile.ToString()}");
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
    }

    public void PerformSpecialAttack(int damage)
    {
        CurrentDamage = damage + 10;
    }

    public void CastProjectile()
    {
        Projectile fireballInstance = Instantiate(projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.forward;
        fireballInstance.Damage = CurrentDamage;
    }

    public void AttackStart()
    {
        CastProjectile();
    }

    public void AttackEnd()
    {
        //some action on end of attack
    }
}
