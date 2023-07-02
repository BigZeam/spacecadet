using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject flowerObj, flowerResource, flowerSlot, flowerParticles;

    [SerializeField] int growTime;
    [SerializeField] Transform flowerPos; 

    [SerializeField] bool coolFlower;
    [SerializeField] AudioClip popEffect;
    
    int growTimer;
    bool isGrowing;
    float xval, yval;


    void Start()
    {
        xval = transform.position.x;
        yval = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(flowerSlot == null && !isGrowing)
        {
            isGrowing = true;
            Instantiate(flowerResource, flowerPos.position, Quaternion.identity); 
            AudioManager.Instance.Play(popEffect);
            if(coolFlower)
            {
               
                Instantiate(flowerResource, flowerPos.position + new Vector3(.5f,0,0), Quaternion.identity);
                Instantiate(flowerResource, flowerPos.position + new Vector3(-.5f,0,0), Quaternion.identity);
            }
            flowerParticles.SetActive(true);
        }
        if(isGrowing)
        {
            growTimer++;

            if(growTimer >= growTime)
            {
                GameObject tempFlower = Instantiate(flowerObj, flowerPos.position, Quaternion.identity);
                //tempFlower.GetComponent<Rigidbody2D>().gravityScale = .5f;
                flowerSlot = tempFlower;
                isGrowing = false;
                growTimer = 0;
                flowerParticles.SetActive(false);
            }
        }
    }
    public void SetFlowerType()
    {
        if(xval < -60 || xval > 70)
        {

        }
        else if (xval < -40 || xval > 55)
        {

        }
    }
    public void SetGrowTime(int newtime)
    {
        growTime-=newtime;
    }
}
