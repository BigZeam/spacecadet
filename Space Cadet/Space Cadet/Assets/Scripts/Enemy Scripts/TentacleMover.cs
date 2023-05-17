using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform endPosition, startPosition;
    
    [SerializeField] float desiredDuration, speed;
    float elapsedTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime/desiredDuration;
        float time = Mathf.PingPong(Time.time * speed, 1);

        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, time);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ChangeHealth();
        }
    }
}
