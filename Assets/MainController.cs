using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public static MainController INSTANCE;
    public static bool PLAYING = true;

    public int gameDuration = 150;

    public ObstacleController obstacleController;
    public AudioController audioController;
    public TextMeshProUGUI timerText;
    public ScoreWidget scoreWidget;

    public float startTime;
    private float threshold = 10;

    void Awake() {
        MainController.INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (PLAYING) {
            if (Time.time - startTime > threshold) {
                obstacleController.AddObstacle();
                threshold += 10;
            }

            float elapsed = Time.time - startTime;
            float remaining = Mathf.Max(gameDuration - elapsed, 0);
            int seconds = Mathf.FloorToInt(remaining % 60);
            int minutes = Mathf.FloorToInt(remaining / 60.0f);

            timerText.text = minutes.ToString() + ':' + (seconds < 10 ? '0' : "") + seconds.ToString();

                if (remaining <= 0){
                    EndGame();
                }
            }
    }

    public void AddScore(string text, int score) {
        scoreWidget.AddScore(text, score);
    }

    void StartGame() {
        PLAYING = true;
        startTime = Time.time;
        audioController.PlayBackground();
    }

    void EndGame() {
        PLAYING = false;
    }
}
