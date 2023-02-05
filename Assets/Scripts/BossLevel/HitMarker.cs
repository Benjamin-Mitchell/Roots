using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    public float lifeTime = 2.0f;
    float currentLife = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLife += Time.deltaTime;
        if (currentLife >= lifeTime)
            Destroy(gameObject);
    }
}
