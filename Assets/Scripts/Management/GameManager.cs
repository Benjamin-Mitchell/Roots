using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numScenes;
    public GameObject player;

    private List<string> scenes = new List<string>();

    public bool intro = true;
    private float introLength = 3.0f;
	// Start is called before the first frame update
	private void Awake()
	{
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        for(int i = 0; i < numScenes; i++)
		{
            scenes.Add("Level" + i.ToString());
		}
    }

	void Start()
    {
        //StartLevel();

        if(intro == true)
		{
            StartCoroutine(WaitToContinue(introLength));
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartLevel()
	{
        GameObject spawn = GameObject.Find("Start");
        player.transform.position = spawn.transform.position;
        Debug.Log("Setting start position at " + spawn.transform.position);
	}

    public bool PlayerReachedEnd()
	{
        if(scenes.Count == 0)
		{
            Debug.Log("YOU WIN");
            return false;
		}
        int index = Random.Range(0, scenes.Count);
        SceneManager.LoadScene(scenes[index]);
        scenes.RemoveAt(index);
        StartLevel();
        return true;
	}

    public IEnumerator WaitToContinue(float secs)
	{
        yield return new WaitForSeconds(secs);
        PlayerReachedEnd();
	}
}
