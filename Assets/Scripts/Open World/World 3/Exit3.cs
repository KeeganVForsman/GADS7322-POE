using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Exit3 : MonoBehaviour
{

    void NextLevel()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.size = Vector2.one * 0.1f;
        box.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            SceneManager.LoadScene(7);
        }
    }
}