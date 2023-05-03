using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject EnemyToSpawn;
    public List<GameObject> spawnedEnemies;
    public int thisSpawnerLevel;
    public float spawnerDelay;
    bool spawning;
    GameManager gm;

    void Start()
    {
        spawnedEnemies = new List<GameObject>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnerController();
    }
    void SpawnEnemy()
    {
        
        GameObject temp = Instantiate(EnemyToSpawn, this.transform.position, Quaternion.identity);

        spawnedEnemies.Add(temp);
        spawning = false;
    }

    void SpawnerController()
    {
        //Debug.Log(spawnedEnemies.Count < thisSpawnerLevel);
        if(spawnedEnemies.Count < thisSpawnerLevel && !spawning)
        {
            Invoke("SpawnEnemy", spawnerDelay);
            spawning = true;
        }

        foreach (GameObject enemy in spawnedEnemies)
        {
            if(enemy == null)
            {
                spawnedEnemies.Remove(enemy);
            }
        } 
    }
    private void OnTriggerStay2D(Collider2D other) {
        SpawnerController();
    }

}
