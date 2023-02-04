using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        transform.position = target.transform.position - offset;
    }

    void LateUpdate()
    {
        transform.position = target.position - offset;
    }
}
