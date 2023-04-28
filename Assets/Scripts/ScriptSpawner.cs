using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private bool canSpawn = true;

    [SerializeField]
    private GameObject spawnPlace;

    [SerializeField]
    private int lives = 3;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while(canSpawn && player)
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, spawnPlace.transform.position, Quaternion.identity);
            yield return wait;
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
        Destroy(gameObject);
    }
}
