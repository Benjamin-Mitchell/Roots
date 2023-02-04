using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shurikan : MonoBehaviour
{
    public int damage; 
    public string enemyTag;
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag)
        {
            other.GetComponent<Character>().TakeDamage(damage);
        }

        if(other.tag != playerTag)
        {
            DestroyShurikan();
        }
    }

    private void DestroyShurikan()
    {
        Destroy(gameObject);
    }
}
