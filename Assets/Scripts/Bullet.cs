using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private int damage = 1;

    [SerializeField] 
    private GameObject bulletImpact;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bulletEffect = Instantiate(bulletImpact, transform.position, transform.rotation);

        GameObject target = collision.gameObject;

        if(target.tag == "Enemy")
        {   
            EnemyController enemy = target.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }

        if(target.tag == "Spawner")
        {
            ScriptSpawner spawner = target.GetComponent<ScriptSpawner>();
            spawner.TakeDamage(damage);
        }

        Destroy(bulletEffect, 0.2f);
        Destroy(gameObject);
    }
}
