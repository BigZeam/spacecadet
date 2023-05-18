using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject asteroid;
    [SerializeField] Vector2 bounds,timebounds;
    float randomTime;
    bool inQ;
    void Start()
    {
        randomTime = Random.Range(timebounds.x,timebounds.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inQ)
        {
            Invoke(nameof(SpawnAsteroid), randomTime);
            inQ = true;
        }
    }


    void SpawnAsteroid()
    {
        Instantiate(asteroid, new Vector3(Random.Range(bounds.x, bounds.y), 100, 0), Quaternion.identity);
        inQ = false;
    }
}
