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

	private void Update()
	{
        transform.Rotate(0.0f, 720.0f*Time.deltaTime, 0.0f);
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag)
        {
            var direction = other.transform.position - transform.position;
            other.GetComponent<Character>().Knockback(direction);
            other.GetComponent<Character>().TakeDamage(damage);
        }

        if(other.tag == "Wall" || other.tag == enemyTag)
        {

            //Todo: check if is going in direction of wall - else ignore
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
