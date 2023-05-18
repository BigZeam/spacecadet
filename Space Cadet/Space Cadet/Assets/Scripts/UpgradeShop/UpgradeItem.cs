using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Upgrade")]
public class UpgradeItem : ScriptableObject
{
    public string Itemname;

    public Sprite splash;

    public ItemType name;

    [SerializeField] int startingCost;

    int cost;

    public string GetName()
    {
        return Itemname;
    }
    public Sprite GetSplash()
    {
        return splash;
    }
    public int GetCost()
    {
        return cost;
    }  

    public void SetCost(int newVal)
    {
        cost+=newVal;
    }

    public void ResetCost(){
        cost = startingCost;
    }
public enum ItemType{
    ClipSize, Damage, FireRate, HealthUp, ReloadSpeed, SpeedBoost, RadiationZone, RadiationPot, HealthPot, GunMod, PlantMod1, PlayerProtection, WeaponMod, RadiationMax, Flower1Growtime,
    
}
}

