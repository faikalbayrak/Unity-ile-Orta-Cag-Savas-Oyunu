using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject Healt, Energy, Food, Water;
    public GameObject Weapon;
    public Image foodAlert, waterAlert;
    private float healt,maxHealt,maxEnergy, energy, food, water;
    Animator foodStatAlert, waterStatAler;
    void Start()
    {
        healt = 100f;
        energy = 100f;
        food = 200f;
        water = 150f;
        maxHealt = 100f;
        maxEnergy = 100f;

        Healt.GetComponent<Slider>().maxValue = maxHealt;
        Energy.GetComponent<Slider>().maxValue= maxEnergy;

        foodStatAlert = foodAlert.GetComponent<Animator>();
        waterStatAler = waterAlert.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StatsUpdate();

    }


    private void StatsUpdate()
    {
        Healt.GetComponent<Slider>().value = healt;
        Energy.GetComponent<Slider>().value = energy;
        Food.GetComponent<Slider>().value = food;
        Water.GetComponent<Slider>().value = water;

        if(water >= 0f && water < 10f)
        {
            waterAlert.gameObject.SetActive(true);
            waterStatAler.SetBool("isAlert", true);
            
        }
        else
        {
            waterAlert.gameObject.SetActive(false);
            waterStatAler.SetBool("isAlert", false);
        }

        if (food >= 0f && food < 10f)
        {
            foodAlert.gameObject.SetActive(true);
            foodStatAlert.SetBool("isAlert", true);
        }
        else
        {
            foodAlert.gameObject.SetActive(false);
            foodStatAlert.SetBool("isAlert", false);
        }

        if (water > 0 || food > 0)
        {
            food -= Time.deltaTime * 10f;
            water -= Time.deltaTime * 10f;
        }
        else
        {
            food = 0;
            water = 0;
        }
        

        if(water == 0 || food == 0)
        {
            if (healt > 0)
                healt -= Time.deltaTime * 10f;
            else
                Die();
        }
    }

    private void Die()
    {

    }

    
}
