using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelLaunch : Launch
{
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

	private void OnCollisionEnter(Collision collision)
	{
        //Do an explosion
        if (collision.gameObject.tag != "Enemy") ;
            Destroy(gameObject);
	}
}
