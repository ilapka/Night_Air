using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawnAnchor;
    [SerializeField] bool respawn;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnCount;

    private float currentTime;
    private int currentEnemyCount;
    private bool spawning;

    void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }

    void Update()
    {
        if(spawning && respawn)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
    }

    public void Respawn()
    {
        currentEnemyCount--;
        if (currentEnemyCount <= 0)
        {
            spawning = true;
            currentTime = spawnDelay;
        }
    }

    void Spawn()
    {
        if (spawnCount == 0)
        {
            respawn = false;
            Destroy(this);
        }
        else
        {
            OnSpawn();

            for (int i = 0; i < spawnAnchor.Length; i++)
            {
                spawnCount--;
                Enemy instance = Instantiate(enemy, spawnAnchor[i].position, Quaternion.identity).GetComponent<Enemy>();
                instance.Spawner = this;
                spawning = false;
            }

            currentEnemyCount = spawnAnchor.Length;
        }
    }

    public virtual void OnSpawn()
    {
        //some special action for inheritors
    }
}
