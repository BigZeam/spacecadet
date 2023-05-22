using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossBounds, bossObj, leftTent, rightTent, dialogueBoxObj;
    [SerializeField] Transform bossSpawn;

    [SerializeField] TMP_Text dialogueText;

    [SerializeField] string[] myDialogue;

    Enemy myEnemy;
    GameObject thisBoss, bossBar;
    bool hasSpawned;
    int health;
    Slider healthSlider;


    private void Awake() {
        bossBar = GameObject.FindGameObjectWithTag("BossBar");
        var sliderObj = bossBar.transform.Find("BossHPBar").GetComponent<Slider>();
        healthSlider = sliderObj;
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
            bossBar.SetActive(true);
            healthSlider.maxValue = myEnemy.health;
            dialogueBoxObj.SetActive(true);
            Invoke(nameof(ShutOffBox), 2f);
        }
    }
    void UpdateUI()
    {

        if(thisBoss!=null)
            healthSlider.value = myEnemy.health;

        if(thisBoss == null)
        {
            bossBar.SetActive(false);
            bossBounds.SetActive(false);
            leftTent.SetActive(false);
            rightTent.SetActive(false);
        }
    }
    void ShutOffBox()
    {
        dialogueBoxObj.SetActive(false);
    }
}
