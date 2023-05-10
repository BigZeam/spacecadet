using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    GameManager gm;
    private void Start() {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            gm.SetTimer(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            gm.SetTimer(false);
        }
    }
}
