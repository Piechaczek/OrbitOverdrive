using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSubText : MonoBehaviour
{

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshProUGUI;
    private int scoreValue = 0;
    private float startY = 0;
    private float targetY = 0;
    private float startTime = 0;
    private float targetTime = 0;
    private bool destroyAfterFinish = false;

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();   
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        rectTransform.offsetMax = new Vector2(0f, textMeshProUGUI.preferredHeight);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        float currentY = startY + t / (targetTime - startTime) * (targetY - startY);
        if (Time.time > targetTime) {
            currentY = targetY;
            if (destroyAfterFinish) {
                Destroy(gameObject);
            }
        }
        rectTransform.offsetMax = new Vector2(0f, currentY);
    }

    public void Init(string scoreText, int scoreValue) {
        textMeshProUGUI.text = scoreText + " +" + scoreValue.ToString();
        this.scoreValue = scoreValue;
    }

    public int GetScoreValue() {
        return scoreValue;
    }

    // pos = -1 means to hide and destroy
    public void NavigateToPosition(int pos, float time) {
        startY = rectTransform.offsetMax.y;
        targetY = -pos * textMeshProUGUI.preferredHeight;
       
        startTime = Time.time;
        targetTime = Time.time + time;

        destroyAfterFinish = (pos == -1);
    }
}
