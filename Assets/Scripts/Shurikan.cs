using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class Shurikan : MonoBehaviour
{
    public int damage; 
    public string enemyTag;
    public GameObject impactParticles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag)
        {
            other.GetComponent<Character>().TakeDamage(damage);
        }

        if(other.tag == "Wall" || other.tag == enemyTag)
        {
            DestroyShurikan();
        }
    }

    private void DestroyShurikan()
    {
        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
