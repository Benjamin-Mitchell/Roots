using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BarrelLaunch : Launch
{
	public GameObject explosion;

	private PlayerController player;
	private float explosionRadius;

	private void Awake()
	{
	}
	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(360.0f * Time.deltaTime, 0.0f, 0.0f));
    }

	public void LaunchIt(Vector3 targetPos, float angle, PlayerController playerPass, float radiusPass)
	{
		explosionRadius = radiusPass;
		player = playerPass;
		base.LaunchIt(targetPos, angle);
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Do an explosion
		GameObject exp = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
		exp.GetComponent<BarrelExplosion>().Explode(player, explosionRadius);

		if (collision.gameObject.tag != "Enemy")
            Destroy(gameObject);
	}
}
