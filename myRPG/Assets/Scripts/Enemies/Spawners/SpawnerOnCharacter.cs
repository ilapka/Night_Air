using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOnCharacter : Spawner
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject shield;

    public override void OnSpawn()
    {
        anim.SetTrigger("Spawn");
    }

    private void OnDestroy()
    {
        Destroy(shield);
    }
}
