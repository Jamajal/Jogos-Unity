using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyDeath;

    [SerializeField] 
    private float speed = 6f;

    //[SerializeField]
    //private float knockbackForce = 10f;

    [SerializeField]
    private int lives = 1;
    
    private GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(player)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
            //rb.AddForce(direction * speed * Time.fixedDeltaTime);
            
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 aimDirection = playerPosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 10f), Space.Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        Collider2D collider = collision.collider;
        
        //Knockback code
        /*
        Collider2D collider = collision.collider;

        if(target.tag == "Bullet")
        {
            Vector2 impulseDirection = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = impulseDirection * knockbackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        */

        if(target.tag == "Player")
        {
            PlayerControler player = target.GetComponent<PlayerControler>();
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;

        if(lives <= 0)
            Die();
    }

    private void Die()
    {
        GameObject deathEffect = Instantiate(enemyDeath, transform.position, transform.rotation);

        Destroy(deathEffect, 0.3f);
        Destroy(gameObject);
    }

}
