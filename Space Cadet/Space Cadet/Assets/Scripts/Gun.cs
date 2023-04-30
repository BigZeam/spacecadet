using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public new string name;

    public Sprite gunSprite;

    public int knockback;
    public int ammoCount, defaultAmmoCount;
    public int damage, defaultDamage;
    public int bulletSpeed, defaultBulletSpeed;
    public float fireRate, defaultFireRate;
    private void Start() {
        
    }
    public void RestoreDefaults()
    {
        ammoCount = defaultAmmoCount;
        damage = defaultDamage;
        bulletSpeed = defaultBulletSpeed;
        fireRate = defaultFireRate;
    }

}
