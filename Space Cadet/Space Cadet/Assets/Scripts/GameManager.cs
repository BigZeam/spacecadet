using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("GameObjects")]
    public GameObject playerObj;
    Gun startingGun;    

    public Slider timerSlider;
    public int globalTimer;
    public int playerLevel {get; private set;}

    [SerializeField] List<Item> allPossibleItems = new List<Item>();

    void Start()
    {
        timerSlider.maxValue = globalTimer;
        startingGun = playerObj.GetComponent<PlayerController>().GetGunSlot1();
        startingGun.RestoreDefaults();
        ResetItemCounts();
    }

    // Update is called once per frame
    void Update()
    {
        SetGlobalTimer();
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


    void ResetItemCounts()
    {
        foreach(Item item in allPossibleItems)
        {
            item.ResetCount();
        }
    }
}
