using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBox : MonoBehaviour
{
    public bool checkForCollisions;
    int damage;

    private void Start()
    {
        checkForCollisions = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" && checkForCollisions)
        {
            other.gameObject.GetComponent<Enemy>().ChangeHealth(damage);
        }
    }

    public void NewDamage(int newDamage)
    {
        damage += newDamage;
    }
}
