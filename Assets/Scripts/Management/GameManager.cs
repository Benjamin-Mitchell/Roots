using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numScenes;
    public GameObject player;

    private CharacterController playerController;

    private List<string> scenes = new List<string>();

    public bool intro = true;
    private float introLength = 3.0f;

    private NavMeshSurface surface;
    private PlayerController playerHealth;
    public Slider playerHealthSlider;

    public CircleWipe circleWipe;

	// Start is called before the first frame update
	private void Awake()
	{
        //test
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        AddScenes();
    }

    private void AddScenes()
    {
        scenes = new List<string>();
        for (int i = 0; i < numScenes; i++)
        {
            scenes.Add("Level" + i.ToString());
        }
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

	void Start()
    {
        //StartLevel();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CharacterController>();
        StartLevel();

        if(intro)
		{
            StartCoroutine(WaitToContinue(introLength));
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth == null)
        {
            return;
        }
        playerHealthSlider.value = (float)playerHealth.currentHealth / (float)playerHealth.maxHealth;

        if (playerHealth.isDead)
        {
            playerHealth.isDead = false;
            StartCoroutine(Restart());
        }
    }

    void StartLevel()
	{
        circleWipe.OpenBlackScreen();
        GameObject spawn = GameObject.Find("Start");
        playerController.enabled = false;
        player.transform.position = spawn.transform.position;
        playerController.enabled = true;

        GameObject temp = GameObject.FindGameObjectWithTag("Nav");
        if (temp != null)
        {
            surface = temp.GetComponent<NavMeshSurface>();
            surface.BuildNavMesh();
        }
	}

    public bool PlayerReachedEnd()
	{
        if(scenes.Count == 0)
		{
            StartCoroutine(LoadBossLevel("BossLevel0"));
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
        intro = false;
	}

    private IEnumerator LoadLevel(string sceneName, int index)
    {
        circleWipe.CloseBlackScreen();
        yield return new WaitForSeconds(2);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        scenes.RemoveAt(index);
        StartLevel();
        //LoadScene?.Invoke(newSceneName);
    }

    public IEnumerator Restart()
    {
        circleWipe.CloseBlackScreen();
        yield return new WaitForSeconds(2);
        //Todo: Show death screen
        playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.isDead = false;
        playerHealth.endingGame = false;
        var asyncLoadLevel = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        intro = true;
        StartLevel();
        StartCoroutine(WaitToContinue(introLength));
    }

    private IEnumerator LoadBossLevel(string sceneName)
    {
        circleWipe.CloseBlackScreen();
        yield return new WaitForSeconds(2);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
    }
}
