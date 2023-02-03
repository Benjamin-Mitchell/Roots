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
    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
