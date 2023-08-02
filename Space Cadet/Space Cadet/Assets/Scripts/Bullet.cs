using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Gun owner;
    [SerializeField] GameObject explodeParticle;

    private void Start()
    {
        Invoke(nameof(LifeEnder), 5f);
    }

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
        if(other.gameObject.tag == "Flower" || other.gameObject.tag == "FlowerBase")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            enemyScript.ChangeHealth(owner.damage);
            Instantiate(explodeParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Coin")
        {
            //Debug.Log(other.gameObject.tag);
            Instantiate(explodeParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Spawner")
        {
            other.gameObject.GetComponent<SpawnerHealth>().ChangeHealth(owner.damage);
            Destroy(this.gameObject);
        }

    }
    void LifeEnder()
    {
        Destroy(this.gameObject);
    }
    public int GetDamage()
    {
        return owner.damage;
    }
}
