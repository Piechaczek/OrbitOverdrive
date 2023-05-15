using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    public static MainController INSTANCE;
    public static bool PLAYING = true;
    public static bool PAUSE_ROTATION = false;
    public static bool IN_MEDIA_RES = false;

    public int gameDuration = 150;

    public ObstacleController obstacleController;
    public AudioController audioController;
    public UIManager uIManager;
    public TextMeshProUGUI timerText;
    public ScoreWidget scoreWidget;
    public BlackHole blackHole;

    public float startTime;
    private float threshold = 10;

    void Awake() {
        MainController.INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IN_MEDIA_RES) {
            uIManager.AnimateCutout();
        }
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

            if (remaining <= 145){
                EndGame();
            }
        }
    }

    public void AddScore(string text, int score) {
        scoreWidget.AddScore(text, score);
    }

    void StartGame() {
        PLAYING = true;
        PAUSE_ROTATION = false;
        startTime = Time.time;
        audioController.PlayBackground();
    }

    void EndGame() {
        PLAYING = false;
        PAUSE_ROTATION = true;

        scoreWidget.OnEndGame();
        uIManager.OnEndGame(1f);
    }

    public void Reset() {
        blackHole.SetVisible(true);
        blackHole.Inflate();
        uIManager.OnReset();

        StartCoroutine(FinishReset());
    }

    public IEnumerator FinishReset() {
        yield return new WaitForSeconds(1f);

        // reset statics
        PLAYING = true;
        PAUSE_ROTATION = false;
        // set static
        IN_MEDIA_RES = true;

        SceneManager.LoadScene("scenes/MainScene");
    }
}
