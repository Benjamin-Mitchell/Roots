using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    private bool endingGame;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !endingGame)
        {
            endingGame = true;
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
