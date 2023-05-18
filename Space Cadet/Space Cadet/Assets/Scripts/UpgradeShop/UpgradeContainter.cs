using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<UpgradeItem> shopItems = new List<UpgradeItem>();
    [SerializeField] UpgradeComputer mainComp;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            mainComp.ClearList();
            foreach (UpgradeItem uItem in shopItems)
            {
                mainComp.AddToItems(uItem);
            }
        }
    }

}
