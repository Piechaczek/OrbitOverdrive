using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public RectTransform rightPanel;
    public RectTransform leftPanel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    private ScoreWidget scoreWidget;
    public RectTransform scoreMask;

    public RectTransform gameOverPanel;
    public RectTransform gameOverText;
    public TextMeshProUGUI gameOverScoreText;
    public RectTransform gameOverButton;

    public RectTransform introCutoutPanel;
    public RectTransform introPanel;


    public Image fullPanel;

    private Vector2 resolution;

    private float endAnimationStartTime = -1f;
    private float endAnimationDuration = 1f;

    private float resetAnimationStartTime = -1f;
    private float resetAnimationDuration = 1f;

    private float cutoutAnimationStartTime = -1f;
    private float cutoutAnimationDuration = 1f;

    private void Awake()
    {
        scoreWidget = scoreText.GetComponent<ScoreWidget>();
        OnScreenUpdate();
    }
    
    private void Update ()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            OnScreenUpdate();
        }

        if (endAnimationStartTime > -1) {
            float elapsed = Mathf.Min(Time.time - endAnimationStartTime, endAnimationDuration);
            float progress = elapsed / endAnimationDuration;

            scoreWidget.SetAlpha(1 - progress);
            timeText.alpha = 1- progress;

            float gameOverPanelHeight = gameOverPanel.offsetMax.y - gameOverPanel.offsetMin.y;
            float gameOverPanelDelta = progress * (Screen.height + (gameOverPanelHeight / 2f));
            gameOverPanel.offsetMax = new Vector2(gameOverPanel.offsetMax.x, -Screen.height + gameOverPanelDelta);
            gameOverPanel.offsetMin = new Vector2(gameOverPanel.offsetMin.x, -Screen.height - gameOverPanelHeight + gameOverPanelDelta);
        
            float flashAlpha = progress * 4;
            if (flashAlpha > 1) {
                flashAlpha = 1f - (flashAlpha - 1f);
            }
            if (flashAlpha < 0) {
                flashAlpha = 0;
            }
            fullPanel.color = new Color(fullPanel.color.r, fullPanel.color.g, fullPanel.color.b, flashAlpha);

            if (elapsed == endAnimationDuration) {
                endAnimationStartTime = -1;
            }
        }

        if (resetAnimationStartTime > -1) {
            float elapsed = Mathf.Min(Time.time - resetAnimationStartTime, resetAnimationDuration);
            float progress = elapsed / resetAnimationDuration;

            float gameOverPanelHeight = gameOverPanel.offsetMax.y - gameOverPanel.offsetMin.y;
            float gameOverPanelDelta = progress * (Screen.height + (gameOverPanelHeight / 2f));
            gameOverPanel.offsetMax = new Vector2(gameOverPanel.offsetMax.x, (gameOverPanelHeight / 2f) - gameOverPanelDelta);
            gameOverPanel.offsetMin = new Vector2(gameOverPanel.offsetMin.x, -(gameOverPanelHeight / 2f) - gameOverPanelDelta);
        }

        if (MainController.IN_MEDIA_RES) {
            introPanel.gameObject.SetActive(true);
            introCutoutPanel.gameObject.SetActive(true);

            introPanel.offsetMax = new Vector2(2*Screen.width, 2*Screen.height);
            introPanel.offsetMin = new Vector2(-2*Screen.width, -2*Screen.height);
        } else {
            introPanel.gameObject.SetActive(false);
            introCutoutPanel.gameObject.SetActive(false);
        }

        if (cutoutAnimationStartTime > -1) {
            float elapsed = Mathf.Min(Time.time - cutoutAnimationStartTime, cutoutAnimationDuration);
            float progress = elapsed / cutoutAnimationDuration;

            float size = introCutoutPanel.offsetMax.x - introCutoutPanel.offsetMin.x;
            float targetSize = Mathf.Max(Screen.width, Screen.height);
            introCutoutPanel.offsetMax = new Vector2(targetSize * progress, targetSize * progress);
            introCutoutPanel.offsetMin = new Vector2(targetSize * -progress, targetSize * -progress);
        }
    }

    public void AnimateCutout() {
        cutoutAnimationStartTime = Time.time;
        cutoutAnimationDuration = 1f;
    }

    private void OnScreenUpdate() {
        resolution = new Vector2(Screen.width, Screen.height);
        leftPanel.sizeDelta = new Vector2((Screen.width - Screen.height) / 2.0f, 0.0f);
        rightPanel.sizeDelta = new Vector2((Screen.width - Screen.height) / 2.0f, 0.0f);

        timeText.rectTransform.offsetMax = new Vector2(0f,  -Screen.height * 0.4f);
        scoreText.rectTransform.offsetMax = new Vector2(0f,  -Screen.height * 0.4f);
        scoreMask.offsetMax = new Vector2(0f,  -Screen.height * 0.4f - scoreText.preferredHeight);
        scoreMask.offsetMin = new Vector2(0f, 0f);

        if (MainController.PLAYING) {
            float height = gameOverPanel.offsetMax.y - gameOverPanel.offsetMin.y;
            gameOverPanel.offsetMax = new Vector2(gameOverPanel.offsetMax.x, -Screen.height);
            gameOverPanel.offsetMin = new Vector2(gameOverPanel.offsetMin.x, -Screen.height - height);
        }
    }

    public void OnEndGame(float duaration) {
        endAnimationStartTime = Time.time;
        endAnimationDuration = duaration;
    }

    public void OnReset() {
        resetAnimationStartTime = Time.time;
        resetAnimationDuration = 1f;
    }

}
