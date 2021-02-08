using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileWeapon
{
    Transform ProjectileSpawn { get; set; }
    void CastProjectile();
}
