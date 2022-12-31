using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    List<List<bool>> level;
    public Vector2 size;
    public GameObject blockPre;
    public float turnChance;
    public int steps;

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
        Vector2 drunkPos = new Vector2(Random.Range(0, (int)size.x - 1), Random.Range(0, (int)size.y - 1));
        Vector2 walkDirection = Vector2.right;

        for (int i = 0; i < steps; i++)
        {
            drunkPos += walkDirection;
            drunkPos = new Vector2(
                Mathf.Clamp(drunkPos.x, 0, size.x -1),
                Mathf.Clamp(drunkPos.y, 0, size.y - 1)
                );
            if (level[(int)drunkPos.x][(int)drunkPos.y] == false) i--;
            else level[(int)drunkPos.x][(int)drunkPos.y] = false;

            if (Random.Range(0, 1f) <= turnChance)
            {
                int seed = Random.Range(0, 7);
                walkDirection = new Vector2(
                    (int)Mathf.Sin(Mathf.PI / 2 * seed),
                    (int)Mathf.Cos(Mathf.PI / 2 * seed)
                    );
            }

            Debug.Log(drunkPos);
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
                    Instantiate(blockPre, new Vector2(ix,iy), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
