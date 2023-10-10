using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed;
    public Vector2 patrolinterval;
    Player_Move player;
    public float alertRange;
    public Vector2 dmgRange;

    LayerMask obstacleMask, walkableMask;
    Vector2 curPos;
    List<Vector2> canMoveList = new List<Vector2>();
    List<Node> nodesList = new List<Node>();
    bool isMoving;

    void Start()
    {
        player = FindObjectOfType<Player_Move>();
        obstacleMask = LayerMask.GetMask("Wall", "Enemy", "Player");
        walkableMask = LayerMask.GetMask("Wall", "Enemy");
        curPos = transform.position;
        StartCoroutine(Movement());
    }

    void Update()
    {


    }

    void Patrol()
    {
        canMoveList.Clear();
        Vector2 size = Vector2.one * 0.8f;
        Collider2D hitup = Physics2D.OverlapBox(curPos + Vector2.up, size, 0, obstacleMask);

        if (!hitup)
        {
            canMoveList.Add(Vector2.up);
        }

        Collider2D hitRight = Physics2D.OverlapBox(curPos + Vector2.right, size, 0, obstacleMask);

        if (!hitRight)
        {
            canMoveList.Add(Vector2.right);
        }

        Collider2D hitLeft = Physics2D.OverlapBox(curPos + Vector2.left, size, 0, obstacleMask);

        if (!hitLeft)
        {
            canMoveList.Add(Vector2.left);
        }

        Collider2D hitdown = Physics2D.OverlapBox(curPos + Vector2.down, size, 0, obstacleMask);

        if (!hitdown)
        {
            canMoveList.Add(Vector2.down);
        }

        if (canMoveList.Count > 0)
        {
            int randomIndex = Random.Range(0, canMoveList.Count);
            curPos += canMoveList[randomIndex];
        }
        StartCoroutine(MoveEnemy(Random.Range(patrolinterval.x, patrolinterval.y)));
    }

    void Attack()
    {
        int roll = Random.Range(0, 100);
        if (roll > 50)
        {
            float dmg = Mathf.Ceil(Random.Range(0, 100));
            Debug.Log(name + "attacked and hit for " + dmg);
        }
        else 
        {
            Debug.Log(name + "attack missed");
        }
    }

    IEnumerator MoveEnemy(float speed)
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, curPos) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, curPos, 5f * Time.deltaTime);
            yield return null;
        }
        transform.position = curPos;

        yield return new WaitForSeconds(speed);
        isMoving = false;
        
    }

    void checkNode(Vector2 chkPoint, Vector2 parent)
    {
        Vector2 size = Vector2.one * 0.5f;
        Collider2D hit = Physics2D.OverlapBox(chkPoint, size, 0, walkableMask);
        if (!hit)
        {
            nodesList.Add(new Node(chkPoint, parent));
        }
    }

    Vector2 nextStep(Vector2 startPos, Vector2 targetPos)
    {
        int listIndex = 0;

        Vector2 mypos = startPos;

        nodesList.Clear();
        nodesList.Add(new Node(startPos, startPos));

        while (mypos != targetPos && listIndex < 1000 && nodesList.Count > 0)
        {
            checkNode(mypos + Vector2.up, mypos);
            checkNode(mypos + Vector2.right, mypos);
            checkNode(mypos + Vector2.down, mypos);
            checkNode(mypos + Vector2.left, mypos);



            listIndex++;
            if (listIndex < nodesList.Count)
            {
                mypos = nodesList[listIndex].position;
            }

        }
        if (mypos == targetPos)
        {
            nodesList.Reverse();
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (mypos == nodesList[i].position)
                {
                    if (nodesList[i].parent == startPos)
                    {
                        return mypos;
                    }
                    mypos = nodesList[i].parent;
                }
            }

        }

        return startPos;
    }

    IEnumerator Movement() 
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!isMoving)
            {
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist <= alertRange)
                {
                    if (dist <= 1.1f)
                    {
                        Attack();
                        yield return new WaitForSeconds(Random.Range(0.5f, 1.15f));
                    }
                    else
                    {
                        Vector2 newPos = nextStep(transform.position, player.transform.position);

                        if (newPos != curPos)
                        {
                            curPos = newPos;
                            StartCoroutine(MoveEnemy(chaseSpeed));
                        }
                        else
                        {
                            Patrol();
                        }
                    }
                    
                }
                else
                {
                    Patrol();
                }
            }
            
        }
                
    }
    
}
