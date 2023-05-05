using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Item/Create", order = 0)]
public class Item : ScriptableObject {

    public Sprite icon;
    public string itemName;
    public int count;

    public void ResetCount()
    {
        count = 1;
    }
}
