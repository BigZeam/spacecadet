using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossBounds, bossObj;
    [SerializeField] Transform bossSpawn;

    GameObject thisBoss;
    bool hasSpawned;

    void Start()
    {
        //healthSlider.maxValue = bossObj.GetComponent<Enemy>().health;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && !hasSpawned)
        {
            bossBounds.SetActive(true);
            //sliderObj.SetActive(true);
            thisBoss = Instantiate(bossObj, bossSpawn.position, Quaternion.identity);
            hasSpawned = true;
        }
    }
    void UpdateUI()
    {
        //healthSlider.value = bossObj.GetComponent<Enemy>().health;

        if(thisBoss == null)
        {
            bossBounds.SetActive(false);
        }
    }
}
