using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject flowerObj, flowerResource, flowerSlot, flowerParticles;

    [SerializeField] int growTime;
    [SerializeField] Transform flowerPos; 
    
    int growTimer;
    bool isGrowing;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flowerSlot == null && !isGrowing)
        {
            isGrowing = true;
            Instantiate(flowerResource, flowerPos.position, Quaternion.identity);
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
}
