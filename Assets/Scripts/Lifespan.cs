using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    public float lifespan = 10;

    private void Start()
    {
        StartCoroutine(nameof(CleanUp));
    }

    private IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(lifespan);
        //Here add animation to shrink etc
        Destroy(gameObject);
    }
}
