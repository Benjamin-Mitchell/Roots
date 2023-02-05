using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipe : MonoBehaviour
{
    private Canvas _canvas;
    private Image _blackScreen;

    public int canvasPriority = 0;

    private void Awake()
    {
        //make it a basic singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UICanvas");

        if (objs.Length > 1)
        {
            for(int i = 0; i < objs.Length; i++)
			{
                if (objs[i] != this.gameObject)
                {
                    //if mine is higher, destroy them. otherwise, destroy me.
                    if (canvasPriority > objs[i].GetComponent<CircleWipe>().canvasPriority)
                    {
                        Destroy(objs[i]);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
			}
        }

        DontDestroyOnLoad(this.gameObject);

        _canvas = GetComponent<Canvas>();
        var images = GetComponentsInChildren<Image>();
        _blackScreen = images.First(i => i.tag == "ScreenFade");
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawBlackScreen();
    }

 

    public void OpenBlackScreen()
    {
        StartCoroutine(Transition(2, 0, 1));
    }

    public void CloseBlackScreen()
    {
        StartCoroutine(Transition(2, 1, 0));
    }

    private void DrawBlackScreen()
    {
        var canvasRect = _canvas.GetComponent<RectTransform>().rect;
        var canvasWidth = canvasRect.width;
        var canvasHeight = canvasRect.height;

        var squareValue = 0f;
        if(canvasWidth > canvasHeight)
        {
            squareValue = canvasWidth;
        }
        else
        {
            squareValue = canvasHeight;
        }

        _blackScreen.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    private IEnumerator Transition(float duration, float beginRadius, float endRadius)
    {
        var time = 0f;
        while(time <= duration)
        {
            time += Time.deltaTime;
            var t = time / duration;
            var radius = Mathf.Lerp(beginRadius, endRadius, t);

            _blackScreen.material.SetFloat("_Radius", radius);
            yield return null;
        }
    }
}
