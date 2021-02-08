using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy
{
    [SerializeField] Sword skeletonSword;
    [SerializeField] GameObject skeletonBones;
    [SerializeField] float bonesExploadForse = 50f;
    [SerializeField] float bonesExploadRadius = 50f;
    
    public override void OnDeath()
    {
        GameObject Instance = Instantiate(skeletonBones, transform.position, Quaternion.identity);
        foreach (Rigidbody rigit in Instance.GetComponentsInChildren<Rigidbody>())
        {
            rigit.AddExplosionForce(bonesExploadForse, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z), bonesExploadRadius);
        }
    }

    #region Animator events
    public override void OnAttack()
    {
        base.OnAttack();
        skeletonSword.AttackStart();
    }

    public override void OffAttack()
    {
        base.OffAttack();
        skeletonSword.AttackEnd();
    }
    #endregion
}
