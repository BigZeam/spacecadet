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
    ComputerManager cm;
    GameManager gm;


    [Header("UI")]
    //public Text ShopText;
    public Text[] names, prices;
    public Image[] splashes;
    //public GameObject[] itemSlots;
    public List <GameObject> itemSlots = new List<GameObject>();
    //public UpgradeItem[] itemList;
    public List<UpgradeItem> itemList = new List<UpgradeItem>();
    int[] itemSlotNum, timesUpgraded, numPrice;
    int x, y, z;
    public GameObject compPanel;

    public float shoptime;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        itemSlotNum = new int[itemSlots.Count];
        timesUpgraded = new int[itemList.Count];
        numPrice = new int[3];
        canShop = true;
        cm = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerManager>();


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
            playerObj.SetCanShoot(true);
            //GenerateItemSlot();
        }
            
    }

    private void CheckInput()
    {
        if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E) && canShop)
            {
                //canShop = false;
                playerObj.SetCanShoot(false);
                showPanel = !showPanel;
                compPanel.SetActive(showPanel);
                //Invoke("ShopResesttime", shoptime);
                for(int i = 0; i < itemSlots.Count; i++)
                {
                    itemSlots[i].SetActive(true);
                }
                GenerateItemSlot();
            }
        }
        if(canShop)
        {
            //ShopText.text = "Shop is Open";
        }
        else {
            //ShopText.text = "Check back Later";
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
        if(itemList.Count > 0)
            y = 1;
        if(itemList.Count > 1)
            z = 2;

        x = 0;
        /*
        if(itemList.Count >=3)
        {
            z = 1;
        }
        
        while(x==y || x==z || y==z)
        {
            x = Random.Range(0, itemList.Count);
            y = Random.Range(0, itemList.Count);
            if(itemList.Count >=3)
            {
                z = Random.Range(0, itemList.Count);
            }
            
        }
        */
        itemSlotNum[0] = x;
        if(itemList.Count > 0)
            itemSlotNum[1] = y;
        if(itemList.Count > 1)
            itemSlotNum[2] = z;
    }
    private void FillSlots()
    {
        for(int j = 0; j < names.Length; j++)
        {
            itemSlots[j].SetActive(false);
        }
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(i < itemList.Count)
            {
                itemSlots[i].SetActive(true);
                names[i].text = itemList[itemSlotNum[i]].GetName();
                splashes[i].sprite = itemList[itemSlotNum[i]].GetSplash();
                if(itemList[itemSlotNum[i]].GetCost() < 101)
                    prices[i].text = itemList[itemSlotNum[i]].GetCost().ToString() + " " + currency.itemName;
                else 
                    prices[i].text = "Fully Upgraded";
            }

            
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

    public void ClearList()
    {
        itemList.Clear();
    }
    public void AddToItems(UpgradeItem newItem)
    {
        itemList.Add(newItem);
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
            case UpgradeItem.ItemType.PlayerProtection:
            cm.playerProtection.SetActive(true);
            itemList[realChoice].SetCost(100);
            break;
            case UpgradeItem.ItemType.WeaponMod:
            cm.gunComputer.SetActive(true);
            itemList[realChoice].SetCost(100);
            break;
            case UpgradeItem.ItemType.PlantMod1:
            cm.plantComputer1.SetActive(true);
            itemList[realChoice].SetCost(100);
            break;
            case UpgradeItem.ItemType.Flower1Growtime:
            cm.thisFlower.SetGrowTime(1800);
            itemList[realChoice].SetCost(10);
            break;
            case UpgradeItem.ItemType.RadiationMax:
            gm.timerSlider.maxValue = gm.timerSlider.maxValue + 10000;
            break;
            case UpgradeItem.ItemType.RadiationZone:
            cm.radiationZone.transform.localScale += new Vector3(5, 0, 0);
            break;
            default:
            Debug.Log("Broke");
            break;
        
        }
            itemList[realChoice].SetCost(10);
        }
        
    }
}
