using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BarrelExplosion : MonoBehaviour
{

    public VisualEffect explosion;

    float lifeTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > 3.0f)
            Destroy(gameObject);
    }

    public void Explode(PlayerController player, float radius)
	{
        //DO A PARTICLE EFFECT OR SOMETHING
        explosion.Play();


        if (Vector3.Distance(player.transform.position, transform.position) < radius)
        {
            player.TakeDamage(50);
        }
	}
}
