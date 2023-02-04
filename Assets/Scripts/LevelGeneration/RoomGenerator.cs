using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// GameObject array Grid of size GridX x GridY
// Each Grid Element contains a tile - tiles can have variation when they spawn.
// How to define variation of multiple tiles?
//  - If we want a set of tiles to turn into holes, we need a superset of tiles to govern changes
//  - 

// There should be two layers of randomness
//  - Superset to describe variety of the room, 2-3 possibilities perhaps
//  - Per-tile variation, addition of visual-only elements


public class RoomGenerator : MonoBehaviour
{
    //public GameObject Spawnable;

    public int minEnemies;
    public int maxEnemies;
    public int numWaves;

    private int wavesSpawned = 0;

    private int updatedMin;

    //TileVariety[] components;
    GameObject[] enemySpawns;

    public List<GameObject> enemiesToSpawn;

    public List<EnemyController> aliveEnemies = new List<EnemyController>();

    private GameObject end;

    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("End");
        end.SetActive(false);
        //pick some superset variations of the scene.
        foreach (Transform child in transform)
		{
            if (child.name.Contains("SuperSet"))
			{
                int target = 0;
                int numPossibilities = 0;
                List<GameObject> possibilities = new List<GameObject>();
                Transform found = child.transform.Find(target.ToString());
                while(found != null)
				{
                    target++;
                    numPossibilities++;
                    possibilities.Add(found.gameObject);
                    found = child.transform.Find(target.ToString());
                }

                //chose a random superset to enable
                int chosen = Random.Range(0, numPossibilities);

                for(int i = 0; i < numPossibilities; i++)
				{
                    if(i == chosen)
                        possibilities[i].SetActive(true);
                    else
                        possibilities[i].SetActive(false);
                }
			}
		}

        //now pick a bunch of tiles and variably add some foliag-ey type stuff.

        //This just gets active components
        //components = GameObject.FindObjectsOfType<TileVariety>();

        //      foreach(TileVariety tile in components)
        //{
        //          {//Randomly spawn foliage and stuff
        //              float f = Random.Range(0.0f, 1.0f);

        //              if (f < 0.33f)
        //              {
        //                  int n = Random.Range(0, tile.spawnLocations.Count);

        //                  List<int> check = new List<int>();
        //                  for (int i = 0; i < n; i++)
        //                  {
        //                      int a = Random.Range(0, tile.spawnLocations.Count);

        //                      //if (!check.Contains(a))
        //                      //{
        //                      //    GameObject temp = GameObject.Instantiate(Spawnable, tile.spawnLocations[a].transform.position, Quaternion.identity);
        //                      //    temp.transform.SetParent(tile.gameObject.transform);
        //                      //    check.Add(a);
        //                      //}
        //                  }
        //              }
        //          }
        //}

        //random chance for waves to increase here?

        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawnLocation");

        updatedMin = minEnemies;
        advanceWave();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void advanceWave()
	{
        if(wavesSpawned >= numWaves)
		{
            EndRoom();
            return;
		}
        int numToSpawn = updatedMin == maxEnemies ? maxEnemies + 1 : Random.Range(updatedMin, maxEnemies);

        List<int> indicesUsed = new List<int>();

        for(int i = 0; i < numToSpawn; i++)
		{
            int index;
            do
            {
                index = Random.Range(0, enemySpawns.Length);
            } while (indicesUsed.Contains(index));

            indicesUsed.Add(index);

            EnemyController temp = GameObject.Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], enemySpawns[index].transform.position, Quaternion.identity).GetComponent<EnemyController>();
            temp.SetRoomGeneration(this);
            aliveEnemies.Add(temp);
        }

        wavesSpawned++;
        StartCoroutine(waveTick());
    }

    private IEnumerator waveTick()
	{
        while(aliveEnemies.Count != 0)
		{
            yield return null;
		}

        advanceWave();
	}

    private void EndRoom()
	{
        end.SetActive(true);
    }
}
