using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karaambar : MonoBehaviour
{
    public GameObject card;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        card.SetActive(true);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
