using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(PlayerController player, float radius)
	{
        //DO A PARTICLE EFFECT OR SOMETHING
        Debug.Log("player pos: " + player.transform.position);
        Debug.Log("barrel pos: " + transform.position);
        Debug.Log("radius: " + radius);

        if (Vector3.Distance(player.transform.position, transform.position) < radius)
        {
            player.TakeDamage(50);
        }
	}
}
