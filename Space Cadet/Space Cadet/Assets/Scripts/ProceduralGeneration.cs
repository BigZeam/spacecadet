using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width,height, xOffset, yOffset;
    [SerializeField] int minStoneheight, maxStoneHeight;
    [SerializeField] GameObject dirt,grass,stone, platform, swarmSpawner, honingSpawner, flierSpawner, flowerPlant;
    [SerializeField] int numSpawners, spawnerDist;

    [SerializeField] GameObject comp, bossRuins, gunRuins;
    [SerializeField] int ruinBounds, bossBounds, spawnBounds;

    bool canChangeHeight;
    int prevSpawnerdist;
    bool canSpawn;
    int bossSpawnPos;
    bool earlySpawn;

    int flatZone, flatMod;
    [SerializeField] float ruinScaler;

    void Start()
    {
        prevSpawnerdist = 0;
        bossSpawnPos = Random.Range(1, 11);
        flatZone = ruinBounds;
        FindBossSpawnPosition();
        Generation();
        
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)//This will help spawn a tile on the x axis
        {
            canChangeHeight = !(x > flatZone && x < flatZone + flatMod);
            int maxHeight;
            int minHeight;
            if(canChangeHeight)
            {
                minHeight = height - 1;
                maxHeight = height + 2;
                height = Random.Range(minHeight, maxHeight);
            }
            int minStoneSpawnDistance = height - minStoneheight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

            canSpawn = x > (prevSpawnerdist +spawnerDist);
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
                if(Random.Range(1,11) < 2 && canChangeHeight)
                {
                    spawnObj(flowerPlant, x, height + 1.45f);
                }
                spawnObj(grass, x, height);
            }

            if(canChangeHeight)
            {
                int randomizer = Random.Range(1, 11);
                if(randomizer < 2)
                {
                    spawnObj(platform, x, height + 1.25f);
                }
                if(randomizer == 5 && numSpawners < 7 && canSpawn)
                {
                    spawnObj(swarmSpawner, x, height + 1.45f);
                    numSpawners++;
                    prevSpawnerdist = x;
                }
                if(randomizer == 6 && numSpawners < 6 && canSpawn)
                {
                    spawnObj(honingSpawner, x, height + 4);
                    numSpawners++;
                    prevSpawnerdist = x;
                }
                if(randomizer == 7 && numSpawners < 5 && canSpawn)
                {
                    spawnObj(flierSpawner, x, height + 5);
                    numSpawners++;
                    prevSpawnerdist = x;
                }
            }
            if(x == (spawnBounds + 8))
            {
                comp.transform.position = new Vector3(x + xOffset, height + yOffset + .5f, 0);
                
            }
            if(x == spawnBounds + flatMod)
            {
                flatZone = bossBounds;
                flatMod = 20;
            }
            if(x == ruinBounds + flatMod)
            {
                flatZone = spawnBounds;
                flatMod = 20;
            }
            if(x == ruinBounds + 7)
            {
                    if(earlySpawn)
                        spawnObj(bossRuins, x, height + ruinScaler);
                    else 
                        spawnObj(gunRuins, x, height + ruinScaler);

            }
            if(x == bossBounds + 7)
            {
                if(!earlySpawn)
                        spawnObj(bossRuins, x, height + ruinScaler);
                    else 
                        spawnObj(gunRuins, x, height + ruinScaler);
            }
            
        }
    }
    
    void spawnObj(GameObject obj,float width,float height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width + xOffset, height + yOffset), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    void FindBossSpawnPosition()
    {
        if(bossSpawnPos < 5)
        {
            flatZone = 5;
            flatMod = 20; 
            earlySpawn = true;
        }
        else 
        {
            flatMod = 14;
        }

    }

}
