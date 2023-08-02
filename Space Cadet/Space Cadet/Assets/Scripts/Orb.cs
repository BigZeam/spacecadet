using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orb : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerTransform;
    PlayerController pc;
    [SerializeField] Vector3 _rotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] OrbBox ob;
    [SerializeField] SpriteRenderer shieldSr;
    [SerializeField] SpriteRenderer damageSr;

    bool canShield = true;
    GameManager gm;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        gm.SetOrbShop();

        GameObject[] tentSpawners = GameObject.FindGameObjectsWithTag("TentSpawner");


        foreach(GameObject spawner in tentSpawners)
        {
            spawner.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        Rotator();
        CheckInput();
    }
    private void Rotator()
    {
        transform.Rotate(_rotation * rotationSpeed * Time.deltaTime);
    }
    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canShield)
        {
            damageSr.DOFade(0, .5f);
            shieldSr.DOFade(.125f, .5f);
            pc.MakeInvulnerable();
            canShield = false;
            ob.checkForCollisions = false;
            Invoke("ResetShield", pc.orbResetTime);
            
        }
    }

    private void ResetShield()
    {
        Invoke("ShieldCoolDown", 2f);
        shieldSr.DOFade(0, .2f);
        damageSr.DOFade(.5f, .5f);
        ob.checkForCollisions = true;
    }
    private void ShieldCoolDown()
    {
        canShield = true;
    }
    public void SetDamage(int d)
    {
        ob.NewDamage(d);
        rotationSpeed += 10;
    }
}
