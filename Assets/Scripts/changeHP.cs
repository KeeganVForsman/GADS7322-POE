using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeHP : MonoBehaviour
{
    public TextMeshProUGUI HP;
    private int playerhealth; // Variable to store player's health

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        HP.text = "HP: " + Player_Move.Instance.HP.ToString();
    }
}