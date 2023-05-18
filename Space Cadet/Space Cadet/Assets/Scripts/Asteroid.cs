using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject asteroidParticles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Random.Range(-1, 1), Random.Range(-1, -3), 1) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().ChangeHealth();
        }
        Instantiate(asteroidParticles, this.transform.position, Quaternion.identity);        
        Destroy(this.gameObject);
    }
}
