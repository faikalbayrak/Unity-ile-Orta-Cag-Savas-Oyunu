using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healt;
    public float armor,armor_healt;
    void Start()
    {
        armor = 5;
        healt = 100;
        armor_healt = 100;
    }

  
    void Update()
    {
        if(healt <= 0)
        {
            Die();
        }
        if(armor_healt <= 0)
        {
            armor = 0;
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
    }
}
