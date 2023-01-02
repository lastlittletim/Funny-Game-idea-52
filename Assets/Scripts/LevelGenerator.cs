using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    List<List<bool>> level;
    public Vector2 size;
    public GameObject blockPre;
    public GameObject player;

    public float roomChance;
    public int roomSize = 2;
    public float turnChance;
    public int brushSize;
    public int steps;

    public int hardLimit;

    public Tilemap tilemap;
    public RuleTile rTile;

    // Start is called before the first frame update
    void Start()
    {
        level = new List<List<bool>>();
        for (int i = 0; i < size.y; i++)
        {
            List<bool> temp = new List<bool>();
            for(int i2 = 0; i2 < size.x; i2++) temp.Add(true);
            level.Add(temp);
        }

        GenerateLevel();
        SpawnLevel();
    }

    void GenerateLevel()
    {
        int counter = 0;
        Vector2 drunkPos = new Vector2(Random.Range(0, (int)size.x - 1), Random.Range(0, (int)size.y - 1));
        Vector2 walkDirection = Vector2.right;

        int i = 0;
        while (i < steps)
        {
            counter++;
            if (counter >= hardLimit) return; //emergency stop

            drunkPos += walkDirection;
            drunkPos = new Vector2(
                Mathf.Clamp(drunkPos.x, 0, size.x - 1),
                Mathf.Clamp(drunkPos.y, 0, size.y - 1)
                );

            //initial paint
            for (int ix = -brushSize; ix < brushSize; ix++)
            {
                for (int iy = -brushSize; iy < brushSize; iy++)
                {
                    int tempX = (int)Mathf.Clamp(drunkPos.x + ix, 0, size.x - 1);
                    int tempY = (int)Mathf.Clamp(drunkPos.y + iy, 0, size.y - 1);
                    if (level[tempY][tempX])
                    {
                        level[tempY][tempX] = false; i++;
                    }
                }
            }

            if (Random.Range(0, 1f) <= turnChance) //turn chance
            {
                int seed = Random.Range(0, 4);
                switch (seed)
                {
                    case 0:
                        walkDirection = Vector2.down;
                        break;
                    case 1:
                        walkDirection = Vector2.right;
                        break;
                    case 2:
                        walkDirection = Vector2.up;
                        break;
                    case 3:
                        walkDirection = Vector2.left;
                        break;
                }
            }

            //room
            if (Random.Range(0, 1f) <= roomChance) //room chance
            {
                for (int ix = -roomSize; ix < roomSize; ix++)
                {
                    for (int iy = -roomSize; iy < roomSize; iy++)
                    {
                        int tempX = (int)Mathf.Clamp(drunkPos.x + ix, 0, size.x - 1);
                        int tempY = (int)Mathf.Clamp(drunkPos.y + iy, 0, size.y - 1);
                        if (level[tempY][tempX])
                        {
                            level[tempY][tempX] = false; i++;
                        }
                    }
                }
            }

            Debug.Log(i);
            //Debug.Log(drunkPos);
        }
    }

    void SpawnLevel()
    {
        for(int ix = 0; ix < size.x - 1; ix++)
        {
            for (int iy = 0; iy < size.y - 1; iy++)
            {
                if (level[ix][iy])
                {
                    tilemap.SetTile(new Vector3Int(ix, iy, 0), rTile);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
