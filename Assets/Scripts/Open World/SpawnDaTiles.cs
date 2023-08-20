using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDaTiles : MonoBehaviour
{
    WorldGenerator wrldGen;

    private void Awake()
    {
        wrldGen = FindAnyObjectByType<WorldGenerator>();
        GameObject goFloor = Instantiate(wrldGen.floorPrefab, transform.position, Quaternion.identity) as GameObject;
        goFloor.name = wrldGen.floorPrefab.name;
        goFloor.transform.SetParent(wrldGen.transform);

        if (transform.position.x > wrldGen.maxX)
        {
            wrldGen.maxX = transform.position.x;
        }

        if (transform.position.x < wrldGen.minX) 
        {
            wrldGen.minX = transform.position.x;
        }

        if (transform.position.y > wrldGen.maxY)
        {
            wrldGen.maxY = transform.position.y;
        }

        if (transform.position.y < wrldGen.minY)
        {
            wrldGen.minY = transform.position.y;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        LayerMask envMask = LayerMask.GetMask("Wall", "Floor");
        Vector2 hitSize = Vector2.one * 0.8f;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 targetPos = new Vector2(transform.position.x + x, transform.position.y + y );
                Collider2D hit = Physics2D.OverlapBox(targetPos, hitSize, 0, envMask);

                if (!hit)
                {
                    GameObject goWall = Instantiate(wrldGen.wallPrefab, targetPos, Quaternion.identity) as GameObject;
                    goWall.name = wrldGen.wallPrefab.name;
                    goWall.transform.SetParent(wrldGen.transform);
                }
                Debug.Log(targetPos);
            }
        }



        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
