using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public RectTransform rightPanel;
    public RectTransform leftPanel;
    public RectTransform timeText;
    public RectTransform scoreText;

    private Vector2 resolution;

    private void Awake()
    {
        OnScreenUpdate();
    }
    
    private void Update ()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            OnScreenUpdate();
        }
    }

    private void OnScreenUpdate() {
        resolution = new Vector2(Screen.width, Screen.height);
        leftPanel.sizeDelta = new Vector2((Screen.width - Screen.height) / 2.0f, 0.0f);
        rightPanel.sizeDelta = new Vector2((Screen.width - Screen.height) / 2.0f, 0.0f);

        timeText.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, Screen.height * 0.4f, 0);
        scoreText.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, Screen.height * 0.4f, 0);
    }

}
