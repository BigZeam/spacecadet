using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovere : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+moveSpeed, transform.position.y, transform.position.z);
        if(transform.position.x > 400 || transform.position.x < -400)
        {
            Destroy(this.gameObject);
        }
    }
}
