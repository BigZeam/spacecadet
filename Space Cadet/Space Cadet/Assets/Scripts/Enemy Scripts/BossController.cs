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
    bool hasSpawned, halftext, finaltext;
    int health;
    Slider healthSlider;
    GameManager gm;


    private void Awake() {
        bossBar = GameObject.FindGameObjectWithTag("BossBar");
        var sliderObj = bossBar.transform.Find("BossHPBar").GetComponent<Slider>();
        healthSlider = sliderObj;
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
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
                if(halftext == false)
                    DisplayText("victory will be mine", 2f);

                halftext = true;
                leftTent.SetActive(true);
                rightTent.SetActive(true);
            }  
        }
        else if (thisBoss == null && hasSpawned)
        {
            if(!finaltext)
            {
                DisplayText("You win this time!", 4f);
                gm.FadeOut();
            }
            finaltext = true;
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

    public void DisplayText(string thisText, float timeforText)
    {
        StartCoroutine(ShortText(thisText, timeforText));
    }

    private IEnumerator ShortText(string newText, float textTime)
    {
        dialogueBoxObj.SetActive(true);
        dialogueText.text = newText;

        yield return new WaitForSeconds(textTime);

        dialogueBoxObj.SetActive(false);
    }
}
