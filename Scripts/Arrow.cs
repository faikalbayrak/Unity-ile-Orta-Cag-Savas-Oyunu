using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody fizikArrow;

    private void Start()
    {
        fizikArrow = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
