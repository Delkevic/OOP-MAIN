using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public int ID;
    public float healthToGive;
    public float manaToGive;
    
    public void Use()
    {
        PlayerHealth.instance.currentHealth += healthToGive;
    }
}
