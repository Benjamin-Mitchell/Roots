using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numScenes;
    public GameObject player;

    private CharacterController playerController;

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
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CharacterController>();
        StartLevel();

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
        playerController.enabled = false;
        player.transform.position = spawn.transform.position;
        playerController.enabled = true;
	}

    public bool PlayerReachedEnd()
	{
        if(scenes.Count == 0)
		{
            Debug.Log("YOU WIN");
            return false;
		}
        int index = Random.Range(0, scenes.Count);
        StartCoroutine(LoadLevel(scenes[index], index));// SceneManager.LoadScene(scenes[index]);
        return true;
	}

    public IEnumerator WaitToContinue(float secs)
	{
        yield return new WaitForSeconds(secs);
        PlayerReachedEnd();
	}

    private IEnumerator LoadLevel(string sceneName, int index)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        scenes.RemoveAt(index);
        StartLevel();
        //LoadScene?.Invoke(newSceneName);
    }
}
