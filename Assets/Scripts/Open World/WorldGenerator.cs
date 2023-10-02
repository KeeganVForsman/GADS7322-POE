using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class WorldGenerator : MonoBehaviour
{
    [HideInInspector]public float minX, maxX, minY, maxY;
    public GameObject floorPrefab, wallPrefab, tilePrefab, exitPrefab;
    public GameObject[] RandomItems, RandomMons;
    LayerMask floorMask, wallMask;
    [Range(0, 100)]public int itemSpawnChance;
    [Range(0, 100)] public int monSpawnChance;


    public int totalFloorCount;

    List<Vector3> floorList = new List<Vector3>();
    

    void Start()
    {
        wallMask = LayerMask.GetMask("Wall");
        floorMask = LayerMask.GetMask("Floor");
        RandomWalker();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void RandomWalker()
    {
        Vector3 curPos = Vector3.zero;

        floorList.Add(curPos);

        while (floorList.Count < totalFloorCount)
        {
            curPos += RandomDirection();

            //InFloorList();



            if (!InFloorList(curPos))
            {
                floorList.Add(curPos);
            }
           
        }
        for (int i = 0; i < floorList.Count; i++)
        {
            GameObject goTile = Instantiate(tilePrefab, floorList[i], Quaternion.identity) as GameObject;
            goTile.name = tilePrefab.name;
            goTile.transform.SetParent(transform);
        }
        StartCoroutine(DelayProgress());


    }

    void RandomItemss(Collider2D hitFloor, Collider2D hitTop, Collider2D hitRight, Collider2D hitBottom, Collider2D hitLeft)
    {
        if ((hitTop || hitRight || hitBottom || hitLeft) && !(hitTop && hitBottom) && !(hitLeft && hitRight))
        {
            int roll = Random.Range(1, 101);
            if (roll <= itemSpawnChance)
            {
                int itemIndex = Random.Range(0, RandomItems.Length);
                GameObject goItem = Instantiate(RandomItems[itemIndex], hitFloor.transform.position, Quaternion.identity) as GameObject;
                goItem.name = RandomItems[itemIndex].name;
                goItem.transform.SetParent(hitFloor.transform);
            }
        }
    }

    bool InFloorList(Vector3 myPos)
    {
        bool inFloorList = false;
        for (int i = 0; i < floorList.Count; i++)
        {
            if (Vector3.Equals(myPos, floorList[i]))
            {
                return true;
                
            }
        }
        return false;
    }

    Vector3 RandomDirection()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return Vector3.up;
                
            case 2:
                return Vector3.right;
                
            case 3:
                return Vector3.down;
                
            case 4:
                return Vector3.left;
                
        }
        return Vector3.zero;
    }

    void RandomVegamons(Collider2D hitFloor, Collider2D hitTop, Collider2D hitRight, Collider2D hitBottom, Collider2D hitLeft)
    {
        if (!hitTop && !hitLeft && !hitBottom && !hitRight) 
        {
            int roll = Random.Range(1, 101);
            if (roll <= monSpawnChance)
            {
                int monIndex = Random.Range(0, RandomMons.Length);
                GameObject goMon = Instantiate(RandomMons[monIndex], hitFloor.transform.position, Quaternion.identity) as GameObject;
                goMon.name = RandomMons[monIndex].name;
                goMon.transform.SetParent(hitFloor.transform);
            }
        }
    }

    void ExitDoorway()
    {
        Vector3 doorPos = floorList[floorList.Count - 1];

       
        
            GameObject goDoor = Instantiate(exitPrefab, doorPos, Quaternion.identity) as GameObject;
            goDoor.name = exitPrefab.name;
            goDoor.transform.SetParent(transform);
        
    }

    IEnumerator DelayProgress()
    {
        while (FindObjectsOfType<SpawnDaTiles>().Length > 0)
        {
            yield return null;
        }

        ExitDoorway();

        Vector2 hitSize = Vector2.one * 0.8f;

        for (int x = (int)minX - 2; x <= (int)maxX + 2; x++)
        {
            for (int y = (int)minY - 2; y <= (int)maxY + 2; y++)
            {
                Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(x, y), hitSize, 0, floorMask);

                if (hitFloor)
                {
                    if (!Vector2.Equals(hitFloor.transform.position, floorList[floorList.Count - 1]))
                    {
                        Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), hitSize, 0, wallMask);
                        Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y ), hitSize, 0, wallMask);
                        Collider2D hitBottom = Physics2D.OverlapBox(new Vector2(x, y - 1), hitSize, 0, wallMask);
                        Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), hitSize, 0, wallMask);

                        RandomItemss(hitFloor, hitTop, hitRight, hitBottom, hitLeft);
                        RandomVegamons(hitFloor, hitTop, hitRight, hitBottom, hitLeft);
                        
                    }
                }
            }
        }
    }


}