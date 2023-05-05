using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void Update()
    {
        this.transform.Translate((transform.forward*Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit");
        if(other.gameObject.tag == "Player")
        {
            PlayerController ps = other.gameObject.GetComponent<PlayerController>();
            ps.ChangeHealth();
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "Coin")
        {
            Debug.Log(other.gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
