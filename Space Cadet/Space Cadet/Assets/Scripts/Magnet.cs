using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.GetComponent<ResourceCollectable>() != null)
        {
            Debug.Log("Hastarget");
            other.gameObject.GetComponent<ResourceCollectable>().SetTarget(this.transform.position);
        }
    }
}
