using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("GameObjects")]
    public GameObject playerObj;
    PlayerController pc;
    Gun startingGun;    
    bool showHealth = true, showRad = true;

    public Slider timerSlider;
    public int globalTimer;
    public int playerLevel {get; private set;}
    bool canSetTimer;
    [SerializeField] Animator effectAnim;

    [SerializeField] List<Item> allPossibleItems = new List<Item>();

    void Start()
    {
        timerSlider.maxValue = globalTimer;
        startingGun = playerObj.GetComponent<PlayerController>().GetGunSlot1();
        startingGun.RestoreDefaults();
        pc = playerObj.GetComponent<PlayerController>();
        ResetItemCounts();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSetTimer)
            SetGlobalTimer();

        CheckForAnim();
    }

    void SetGlobalTimer()
    {
        globalTimer--;

        timerSlider.value = globalTimer;

        if(globalTimer <= 0)
        {
            //Debug.Log("LOS");
        }
    }

    void CheckForAnim()
    {
        if(pc.GetHealth() == 1 && showHealth)
        {
            effectAnim.SetTrigger("LowHealth");
            showHealth = false;
            Invoke(nameof(HealthTimer), 15f);
        }
        if(timerSlider.value < 10000 && showRad)
        {
            effectAnim.SetTrigger("LowRadiation");
            showRad = false;
            Invoke(nameof(RadTimer), 5f);
        }
    }
    void ResetItemCounts()
    {
        foreach(Item item in allPossibleItems)
        {
            item.ResetCount();
        }
    }

    public void SetTimer(bool b)
    {
        canSetTimer = b;
        if(!canSetTimer)
        {
            globalTimer = (int)timerSlider.maxValue;
            timerSlider.value = timerSlider.maxValue;
            showRad = true;
        }
    }
    void RadTimer()
    {
        showRad = true;
    }
    void HealthTimer()
    {
        showHealth = true;
    }
}
