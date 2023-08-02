using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    Transform target;
    SpriteRenderer sr;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);


        if(transform.position.x > target.position.x)
        {
            sr.flipX = false;
        }
        else 
        {
            sr.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ChangeHealth();
        }
        Destroy(this.gameObject);
    }
}
