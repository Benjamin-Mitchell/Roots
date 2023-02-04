using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
		{
            if(!gameManager.PlayerReachedEnd())
			{
                GetComponent<BoxCollider>().enabled = false;
			}
		}

	}
}
