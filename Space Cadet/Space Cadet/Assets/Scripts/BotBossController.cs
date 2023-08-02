using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotBossController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int totalHealth;

    [SerializeField] Slider _bossSlider;
    [SerializeField] GameObject sliderObj;

    [SerializeField] GameObject tentObjs;

    void Awake()
    {
        sliderObj.SetActive(true);

        _bossSlider.maxValue = totalHealth;

        _bossSlider.value = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int incomingDamage)
    {  
        totalHealth-= incomingDamage;

        _bossSlider.value = totalHealth;
        

        //check health threshold for boss phases
        if(totalHealth >= 2400)
        {

        }
        else if (totalHealth <=1200)
        {   
            tentObjs.SetActive(true);
        }
    }
}
