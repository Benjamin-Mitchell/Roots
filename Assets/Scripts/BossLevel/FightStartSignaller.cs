using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightStartSignaller : MonoBehaviour
{
    private Boss boss;
    public GameObject door;
    public Transform finalPos;

    public GameObject[] toDisableNav;
    
    public GameObject BossHealthBar;
    public TextMeshProUGUI bossText;
    public Slider bossHealthSlider;
    public Image bossHealthBackground;
    public Image bossHealthFill;

    private Color textColor, fillColor, sliderColor, backgroundColor;

    private BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        col = GetComponent<BoxCollider>();

        textColor = bossText.color;
        fillColor = bossHealthFill.color;
        sliderColor = bossHealthSlider.colors.normalColor;
        backgroundColor = bossHealthBackground.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for(int i = 0; i < toDisableNav.Length; i++)
			{
                toDisableNav[i].layer = LayerMask.NameToLayer("NotNavigable");
			}
            boss.StartTheFight();
            StartCoroutine(SmoothLerp(2.0f));

            col.enabled = false;
            BossHealthBar.SetActive(true);
            StartCoroutine(LerpUIColours(2.0f));
        }
    }

    private IEnumerator LerpUIColours(float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            bossText.color = Color.Lerp(Color.clear, textColor, (elapsedTime / time));
            
            bossHealthFill.color = Color.Lerp(Color.clear, fillColor, (elapsedTime / time));

            var block = bossHealthSlider.colors;
            block.normalColor = Color.Lerp(Color.clear, sliderColor, (elapsedTime / time));
            bossHealthSlider.colors = block;

            bossHealthBackground.color = Color.Lerp(Color.clear, backgroundColor, (elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SmoothLerp(float time)
	{
        Vector3 startingPos = door.transform.position;
        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            door.transform.position = Vector3.Lerp(startingPos, finalPos.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
