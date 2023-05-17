using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossBounds, bossObj, leftTent, rightTent;
    [SerializeField] Transform bossSpawn;

    Enemy myEnemy;
    GameObject thisBoss;
    bool hasSpawned;
    int health;

    void Start()
    {
        //healthSlider.maxValue = bossObj.GetComponent<Enemy>().health;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if(thisBoss!=null)
        {
            health = myEnemy.health;
            if(health < 300)
            {
                leftTent.SetActive(true);
                rightTent.SetActive(true);
            }  
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && !hasSpawned)
        {
            bossBounds.SetActive(true);
            //sliderObj.SetActive(true);
            thisBoss = Instantiate(bossObj, bossSpawn.position, Quaternion.identity);
            myEnemy = thisBoss.GetComponent<Enemy>();
            hasSpawned = true;
        }
    }
    void UpdateUI()
    {
        //healthSlider.value = bossObj.GetComponent<Enemy>().health;

        if(thisBoss == null)
        {
            bossBounds.SetActive(false);
            leftTent.SetActive(false);
            rightTent.SetActive(false);
        }
    }
}
