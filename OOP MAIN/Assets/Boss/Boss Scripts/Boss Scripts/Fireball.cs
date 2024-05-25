using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private string ownerTag;

    public void SetOwnerTag(string tag)
    {
        ownerTag = tag;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != ownerTag)
        {
            // Çarpýþma iþlemleri (örneðin, hasar verme, yok etme vs.)
            Destroy(gameObject);  // Ateþ topunu yok et
        }
    }
}

