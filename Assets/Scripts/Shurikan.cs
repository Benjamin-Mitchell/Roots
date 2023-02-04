using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shurikan : MonoBehaviour
{
    public int damage; 
    public string enemyTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag)
        {
            other.GetComponent<Character>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
