using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public int gameDuration = 150;

    public ObstacleController obstacleController;
    public TextMeshProUGUI timerText;

    private float startTime;
    private float threshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > threshold) {
            obstacleController.AddObstacle();
            threshold += 10;
        }

        float elapsed = Time.time - startTime;
        float remaining = Mathf.Max(gameDuration - elapsed, 0);
        int seconds = Mathf.FloorToInt(remaining % 60);
        int minutes = Mathf.FloorToInt(remaining / 60.0f);

        timerText.text = minutes.ToString() + ':' + (seconds < 10 ? '0' : "") + seconds.ToString();
    }
}
