using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string creatureName;
    public int creatureLvl;
    public int creature2CurrentHp;
    public int creature2Dmg;
    public int creature2MaxHp;
    public bool isBlockingP2 = false;



    public bool DamageTookC2(int dmg)
    {
        if (isBlockingP2 == true)
        {
            dmg -= 5;
        }
        creature2CurrentHp -= dmg;

        if (creature2CurrentHp <= 0)
            return true;
        else
            return false;
    }

    public void Player_2Heal(int Health)
    {
        creature2CurrentHp += Health;
        if (creature2CurrentHp > creature2MaxHp)
            creature2CurrentHp = creature2MaxHp;
    }
    
    public void BlockedP2()
    {
        isBlockingP2 = true;
    }

}
