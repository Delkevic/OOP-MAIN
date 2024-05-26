using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float speed;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Instance.characterThrow(speed);
    }
}
