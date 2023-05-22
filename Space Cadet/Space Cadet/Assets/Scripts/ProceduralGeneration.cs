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
                if(Random.Range(1,15) < 2 && canChangeHeight)
                {
                    spawnObj(flowerPlant, x, height + 1.45f);
                }
                spawnObj(grass, x, height);
            }

            if(canChangeHeight)
            {
                int randomizer = Random.Range(1, 24);
                if(randomizer < 2)
                {
                    spawnObj(platform, x, height + 1.25f);
                }
                if(randomizer == 5 && numSpawners < 28 && canSpawn && (x < (spawnBounds - 10) || x > (spawnBounds+flatMod+15)))
                {
                    spawnObj(swarmSpawner, x, height + 1.45f);
                    numSpawners++;
                    prevSpawnerdist = Random.Range(7, 14);
                }
                if(randomizer == 6 && numSpawners < 18 && canSpawn && (x < (spawnBounds - 30) || x > (spawnBounds+flatMod+25)))
                {
                    spawnObj(honingSpawner, x, height + 4);
                    numSpawners++;
                    prevSpawnerdist = Random.Range(10, 18);
               }
                if(randomizer == 7 && numSpawners < 20 && canSpawn && (x < (spawnBounds - 60) || x > (spawnBounds+flatMod+60)))
                {
                    spawnObj(flierSpawner, x, height + 5);
                    numSpawners++;
                    prevSpawnerdist = Random.Range(6, 18);
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
