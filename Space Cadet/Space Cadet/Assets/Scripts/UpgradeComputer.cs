using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeComputer : MonoBehaviour
{
    // Start is called before the first frame update
    bool canInteract;
    bool showPanel;
    bool canShop;
    [Header("General Shop Thigs")]
    [SerializeField] Item currency;
    public PlayerController playerObj;


    [Header("UI")]
    public Text ShopText;
    public Text[] names, prices;
    public Image[] splashes;
    public GameObject[] itemSlots;
    public UpgradeItem[] itemList;
    int[] itemSlotNum, timesUpgraded, numPrice;
    int x, y, z;
    public GameObject compPanel;

    public float shoptime;

    void Start()
    {
        itemSlotNum = new int[3];
        timesUpgraded = new int[itemList.Length];
        numPrice = new int[3];
        canShop = true;



        for(int i = 0; i < timesUpgraded.Length; i++)
        {
            timesUpgraded[i] = 1;
            itemList[i].ResetCost();
        }


    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
            //GenerateItemSlot();
        }
            
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            canInteract = false;
            compPanel.SetActive(false);
            //GenerateItemSlot();
        }
            
    }

    private void CheckInput()
    {
        if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E) && canShop)
            {
                canShop = false;
                showPanel = !showPanel;
                compPanel.SetActive(showPanel);
                Invoke("ShopResesttime", shoptime);
                for(int i = 0; i < itemSlots.Length; i++)
                {
                    itemSlots[i].SetActive(true);
                }
                GenerateItemSlot();
            }
        }
        if(canShop)
        {
            ShopText.text = "Shop is Open";
        }
        else {
            ShopText.text = "Check back Later";
        }
    }
    public void HidePanel()
    {
        compPanel.SetActive(false);
    }

    private void GenerateItemSlot()
    {
        
        GenerateUniqueNumber();
        FillSlots();
        
    }
    private void GenerateUniqueNumber()
    {
        x = 1;
        y = 1;
        z = 1;
        while(x==y || x==z || y==z)
        {
            x = Random.Range(0, itemList.Length);
            y = Random.Range(0, itemList.Length);
            z = Random.Range(0, itemList.Length);
        }

        itemSlotNum[0] = x;
        itemSlotNum[1] = y;
        itemSlotNum[2] = z;
    }
    private void FillSlots()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            names[i].text = itemList[itemSlotNum[i]].GetName();
            splashes[i].sprite = itemList[itemSlotNum[i]].GetSplash();
            prices[i].text = itemList[itemSlotNum[i]].GetCost().ToString() + " " + currency.itemName;
            
        }
    }


    private int GetPrice(int num)
    {
        return timesUpgraded[num] * 10;
    }



    void ShopResesttime()
    {
        canShop = true;
    }

    /// fill shop items
    public void BigMethod(int choice)
    {



        int realChoice;


        
        switch(choice)
        {
            case 1:
            realChoice = itemSlotNum[0];
            break;
            case 2:
            realChoice = itemSlotNum[1];
            break;
            default: 
            realChoice = itemSlotNum[2];
            break;
            //totherthing
        }

        if(itemList[realChoice].GetCost() <= currency.count)
        {
        
        currency.count = currency.count - itemList[realChoice].GetCost();
        itemSlots[choice -1].SetActive(false);
        switch(itemList[realChoice].name)
        {
            case UpgradeItem.ItemType.ClipSize:
            //clipsize
            playerObj.GetGunSlot1().ammoCount+=5;
            break;
            case UpgradeItem.ItemType.Damage:
            //damage
            playerObj.GetGunSlot1().damage+=5;
            break;
            case UpgradeItem.ItemType.FireRate:
            //fireRate
            playerObj.GetGunSlot1().fireRate-=.05f;
            break;
            case UpgradeItem.ItemType.HealthUp:
            //healthUP
            playerObj.IncHealth();
            break;
            case UpgradeItem.ItemType.ReloadSpeed:
            //reloadSpeed
            playerObj.IncReloadSpeed();
            break;
            case UpgradeItem.ItemType.SpeedBoost:
            //inc move speed
            playerObj.IncMoveSpeed(.5f);
            break;
            default:
            Debug.Log("Broke");
            break;
        
        }
            itemList[realChoice].SetCost(10);
        }
        
    }
}
