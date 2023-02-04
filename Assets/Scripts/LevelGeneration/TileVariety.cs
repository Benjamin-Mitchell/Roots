using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVariety : MonoBehaviour
{
    public bool canRotate = false;
    public List<GameObject> spawnLocations = new List<GameObject>();

	public List<GameObject> spawnables;

    public bool canSpawnEnemies = false;

    // Start is called before the first frame update
    void Start()
    {
        float f = Random.Range(0.0f, 1.0f);

		if (f < 0.33f)
		{
			int n = Random.Range(0, spawnLocations.Count);

			List<int> check = new List<int>();
			for (int i = 0; i < n; i++)
			{
				int a = Random.Range(0, spawnLocations.Count);

				if (!check.Contains(a))
				{
					int index = Random.Range(0, spawnables.Count);
					GameObject temp = GameObject.Instantiate(spawnables[index], spawnLocations[a].transform.position, Quaternion.identity);
					temp.transform.Rotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
					temp.transform.SetParent(gameObject.transform);
					check.Add(a);
				}
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
