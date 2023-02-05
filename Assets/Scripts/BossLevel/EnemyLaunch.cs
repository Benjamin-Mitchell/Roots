using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLaunch : Launch
{
    NavMeshAgent navAgent;
    EnemyController enemyController;
	private void Awake()
	{
        gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = false;
        enemyController = GetComponent<EnemyController>();
        enemyController.enabled = false;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer != LayerMask.NameToLayer("EnemyProjectile") && collision.gameObject.tag != "Enemy")
		{
            gameObject.layer = LayerMask.NameToLayer("Enemies");
            navAgent.enabled = true;
            enemyController.enabled = true;
        }
	}
}
