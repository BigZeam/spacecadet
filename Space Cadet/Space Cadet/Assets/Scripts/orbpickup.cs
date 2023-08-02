using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbpickup : MonoBehaviour
{
    [SerializeField] GameObject orbPrefab;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<PlayerController>().ActivateOrb();
            Instantiate(orbPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
