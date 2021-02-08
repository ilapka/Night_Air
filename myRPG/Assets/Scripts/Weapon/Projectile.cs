using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; } 
    public float projectileSpeed = 50f;

    Vector3 spawnPosition;

    void Start()
    {
        Range = 20f;
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * projectileSpeed);
    }
    void Update()
    {
        if (Vector3.Distance(spawnPosition, transform.position) >= Range)
        {
            Extinguish();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            Debug.Log(Damage);
            col.transform.GetComponent<Enemy>().TakeDamage(Damage);
        }
        Extinguish();
    }

    void Extinguish()
    {
        Destroy(gameObject);
    }
}
