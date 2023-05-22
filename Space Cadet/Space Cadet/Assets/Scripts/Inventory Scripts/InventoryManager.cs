using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    [SerializeField] Transform itemContent;
    [SerializeField] GameObject inventoryItem;
    [SerializeField] Item healthPot;
    PlayerController pc;
    GameManager gm;

    private void Awake() {
        Instance = this;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Add(Item item)
    {
        foreach(Item prevItem in items)
        {
            if(prevItem.itemName == item.itemName)
            {
                prevItem.count = prevItem.count+1;
                return;
            }
        }
        items.Add(item);
        ListItems();
    }
    public void Remove(Item item)
    {
        foreach(Item prevItem in items)
        {
            if(prevItem.itemName == item.itemName)
            {
                prevItem.count = prevItem.count-1;
                if(prevItem.count <=0)
                {
                    items.Remove(item);
                }
            }
        }
        ListItems();
    }

    public void ListItems()
    {
        foreach(Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemNum = obj.transform.Find("Num/NumText").GetComponent<TMP_Text>();
            var buttonObj = obj.transform.Find("ConsumableButton");

            buttonObj.gameObject.SetActive(item.consumable);

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemNum.text = item.count.ToString();
            
            buttonObj.GetComponent<Button>().onClick.AddListener(GivePlayerHealth);
        }
    }
    void GivePlayerHealth()
    {
        if(pc.GetHealth() < 3)
        {
            pc.IncHealth();
            Remove(healthPot);
            Invoke("ListItems",.5f);
        }
    }
}
