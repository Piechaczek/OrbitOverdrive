using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public RectTransform rightPanel;
    public RectTransform leftPanel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public RectTransform scoreMask;

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

        timeText.rectTransform.offsetMax = new Vector2(0f,  -Screen.height * 0.4f);
        scoreText.rectTransform.offsetMax = new Vector2(0f,  -Screen.height * 0.4f);
        scoreMask.offsetMax = new Vector2(0f,  -Screen.height * 0.4f - scoreText.preferredHeight);
        scoreMask.offsetMin = new Vector2(0f, 0f);
    }

}
