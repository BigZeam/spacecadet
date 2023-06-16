using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealth : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] Sprite[] spawnerSprites;
    [SerializeField] int health;
    [SerializeField] GameObject parentObj;
    [SerializeField] GameObject vines;
    [SerializeField] GameObject resourceToSpawn;

    Sprite prevSprite;
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        //prevSprite = spawnerSprites[0];
    }
    // Start is called before the first frame update
    public void ChangeHealth(int damage)
    {
        health -= damage;

        if(health >= 200)
        {
            sr.sprite = spawnerSprites[0];
            if(prevSprite != spawnerSprites[0])
            {
                Instantiate(resourceToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            prevSprite = spawnerSprites[0];

        }

        else if(health > 150)
        {
            if(vines!=null)
                vines.SetActive(false);
            sr.sprite = spawnerSprites[1];
            if(prevSprite != spawnerSprites[1])
            {
                Instantiate(resourceToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            prevSprite = spawnerSprites[1];
        }
        else if(health > 75)
        {
             if(vines!=null)
                vines.SetActive(false);
            if(prevSprite != spawnerSprites[2])
            {
                Instantiate(resourceToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            prevSprite = spawnerSprites[2];
            sr.sprite = spawnerSprites[2];
        }

        if(health <= 0)
        {
            Instantiate(resourceToSpawn, transform.position + new Vector3(.5f, .75f, 0), Quaternion.identity);
            Instantiate(resourceToSpawn, transform.position + new Vector3(-.5f, .75f, 0), Quaternion.identity);
            Instantiate(resourceToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(parentObj);
        }
        
    }
}
