using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Upgrade")]
public class UpgradeItem : ScriptableObject
{
    public string Itemname;

    public Sprite splash;

    public string GetName()
    {
        return Itemname;
    }
    public Sprite GetSplash()
    {
        return splash;
    }

}
