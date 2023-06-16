using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<UpgradeItem> shopItems = new List<UpgradeItem>();
    [SerializeField] UpgradeComputer mainComp;
    [SerializeField] Item thisCurrency;
    
    private void Start() {
        foreach (UpgradeItem uItem in shopItems)
        {
            uItem.ResetCost();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            mainComp.SwitchCurrency(thisCurrency);
            mainComp.ClearList();
            mainComp.SwitchCurrency(thisCurrency);
            foreach (UpgradeItem uItem in shopItems)
            {
                mainComp.AddToItems(uItem);
            }
        }
    }

}
