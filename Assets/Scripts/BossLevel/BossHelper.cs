using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHelper : MonoBehaviour
{
    public CircleWipe circleWipe;
    private PlayerController player;
    public GameObject UI;

    bool loadedIntro = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        circleWipe.OpenBlackScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead && !loadedIntro)
		{
            StartCoroutine(LoadLevel("Intro"));
            loadedIntro = true;
		}

    }

    private IEnumerator LoadLevel(string sceneName)
    {
        circleWipe?.CloseBlackScreen();
        yield return new WaitForSeconds(2);
        Destroy(UI);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
    }
}
