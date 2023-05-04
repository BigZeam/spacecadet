using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Gun owner;
    [SerializeField] GameObject explodeParticle;
    void Update()
    {
        this.transform.Translate((transform.forward*Time.deltaTime));
    }
    public void SetGun(Gun g)
    {
        owner = g;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit");
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            enemyScript.ChangeHealth(owner.damage);
            Instantiate(explodeParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Coin")
        {
            Debug.Log(other.gameObject.tag);
            Instantiate(explodeParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
