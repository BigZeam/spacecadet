using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("GameObjects")]
    public GameObject playerObj;
    PlayerController pc;
    Gun startingGun;    
    bool showHealth = true, showRad = false, pastFrame1 = false;

    public Slider timerSlider;
    public int globalTimer;
    public int playerLevel {get; private set;}
    bool canSetTimer, canLowerTimer;
    [SerializeField] Animator effectAnim;
    [SerializeField] GameObject losePanelObj;

    [SerializeField] List<Item> allPossibleItems = new List<Item>();

    void Start()
    {
        timerSlider.maxValue = globalTimer;
        startingGun = playerObj.GetComponent<PlayerController>().GetGunSlot1();
        startingGun.RestoreDefaults();
        pc = playerObj.GetComponent<PlayerController>();
        ResetItemCounts();
        Invoke(nameof(GameHasStarted), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSetTimer)
            SetGlobalTimer();
        else
        {
            LowerRadiationLevels();
        }
        CheckForAnim();
        timerSlider.value = globalTimer;
    }

    void SetGlobalTimer()
    {
        globalTimer--;

        timerSlider.value = globalTimer;

        if(globalTimer <= 0)
        {
            LoseGame();
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
        if(timerSlider.value < 10000 && showRad && pastFrame1)
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
        if(!canSetTimer && pastFrame1)
        {
            globalTimer = (int)timerSlider.maxValue;
            timerSlider.DOValue(timerSlider.maxValue, 1f, true);
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
    void GameHasStarted()
    {
        pastFrame1 = true;
    }
    public void LoseGame()
    {
        losePanelObj.SetActive(true);
    }
    void LowerRadiationLevels()
    {
        globalTimer+=100;
        if(globalTimer > timerSlider.maxValue)
            globalTimer = (int)timerSlider.maxValue;
    }
}
