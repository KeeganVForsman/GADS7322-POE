using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            //Enemy--;

            //if (Enemy == 0)
            {
                Destroy(gameObject);

                SceneManager.LoadScene(0);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}