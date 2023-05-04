using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width,height, xOffset, yOffset;
    [SerializeField] int minStoneheight, maxStoneHeight;
    [SerializeField] GameObject dirt,grass,stone, platform, swarmSpawner, honingSpawner, flierSpawner;
    [SerializeField] int numSpawners;
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)//This will help spawn a tile on the x axis
        {
            // now for procedural generation we need to gradually increase and decrease the height value
            int minHeight = height - 1;
            int maxHeight = height + 2;
            height = Random.Range(minHeight, maxHeight);
            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);
            //Perlin noise.
            for (int y = 0; y < height; y++)//This will help spawn a tile on the y axis
            {
                if (y < totalStoneSpawnDistance)
                {
                    spawnObj(stone, x, y);
                }
                else
                {
                    spawnObj(dirt, x, y);
                }
               
            }
            if(totalStoneSpawnDistance == height)
            {
                spawnObj(stone, x, height);
            }
            else
            {
                spawnObj(grass, x, height);
            }

            int randomizer = Random.Range(1, 11);
            if(randomizer < 2)
            {
                spawnObj(platform, x, height + 1);
            }
            if(randomizer == 5 && numSpawners < 1)
            {
                spawnObj(swarmSpawner, x, height + 1);
                numSpawners++;
            }
            if(randomizer == 6 && numSpawners < 2)
            {
                spawnObj(honingSpawner, x, height + 4);
                numSpawners++;
            }
            if(randomizer == 7 && numSpawners < 3)
            {
                spawnObj(flierSpawner, x, height + 5);
                numSpawners++;
            }
            
        }
    }
    
    void spawnObj(GameObject obj,int width,int height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width + xOffset, height + yOffset), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

}
