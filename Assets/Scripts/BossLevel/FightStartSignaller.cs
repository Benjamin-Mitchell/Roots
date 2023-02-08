using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightStartSignaller : MonoBehaviour
{
    private Boss boss;
    public GameObject door;
    public Transform finalPos;

    public GameObject[] toDisableNav;

    private BoxCollider col;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        col = GetComponent<BoxCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool started;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !started)
        {
            started = true;
            for (int i = 0; i < toDisableNav.Length; i++)
			{
                toDisableNav[i].layer = LayerMask.NameToLayer("NotNavigable");
			}
            boss.StartTheFight();
            StartCoroutine(SmoothLerp(2.0f));

            col.enabled = false;
            gameManager.StartBoss();
        }
    }


    private IEnumerator SmoothLerp(float time)
	{
        Vector3 startingPos = door.transform.position;
        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            door.transform.position = Vector3.Lerp(startingPos, finalPos.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
