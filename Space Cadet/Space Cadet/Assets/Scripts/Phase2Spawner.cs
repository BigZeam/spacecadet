using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] Vector2 timeBounds;
    [SerializeField] bool canTakeDamage;
    [SerializeField] BotBossController bc;
    

    bool isSpawning, canSpawn;

    [SerializeField] int health;

    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn && !isSpawning)
        {
            Invoke("SpawnObj", Random.Range(timeBounds.x, timeBounds.y));
            isSpawning = true;
        }
    }

    private void SpawnObj()
    {
        Instantiate(enemyToSpawn, this.transform.position, Quaternion.identity);
        isSpawning = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(canTakeDamage)
        {
            if(other.gameObject.tag == "Bullet")
            {
                health-= other.gameObject.GetComponent<Bullet>().GetDamage();
                bc.TakeDamage(other.gameObject.GetComponent<Bullet>().GetDamage());

                if(health <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
