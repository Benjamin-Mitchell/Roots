using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shurikan : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
