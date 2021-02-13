using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomper : Enemy
{
    [SerializeField] ParticleSystem exploasionParticleSystem;
    [SerializeField] float explodRange = 4f;
    [SerializeField] int exploadDamage = 15;

    //Animator event
    public void Explosion()
    {
        exploasionParticleSystem.Play();
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= explodRange)
        {
            player.TakeDamage(exploadDamage);
        }
    }
}
