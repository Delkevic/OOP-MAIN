using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
