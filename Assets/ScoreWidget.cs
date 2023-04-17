using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreWidget : MonoBehaviour
{

    public int targetScore = 0;
    public int displayedScore = 0;

    public TextMeshProUGUI scoreText;
    public Transform scoreMask;
    public GameObject subScoreTextPrefab;

    public float arrivalTime = 0.3f;
    public float stayTime = 2.0f;
    public float departureTime = 0.1f;
    private List<ScoreSubText> subScores = new List<ScoreSubText>();

    private float timeUntilStay = -1f;

    void Start() {
        StartCoroutine(Tmp());
    }

    private IEnumerator Tmp() {
        yield return new WaitForSeconds(3f);
        AddScore("You got points!", 100);
        yield return new WaitForSeconds(3f);
        AddScore("You got points", 100);
        yield return new WaitForSeconds(0.5f);
        AddScore("Dummy points", 50);
        yield return new WaitForSeconds(1f);
        AddScore("More points", 10);
        AddScore("Even more points", 30);
    }

    void Update() {

        if (subScores.Count > 0 && Time.time >= timeUntilStay) {
            foreach (ScoreSubText score in subScores) {
                score.NavigateToPosition(-1, departureTime);
                targetScore += score.GetScoreValue();
            }
            subScores.Clear();
        }


        if (targetScore > displayedScore) {
            if (targetScore > displayedScore + 100){
                displayedScore += 3;
            } else if (targetScore > displayedScore + 50){
                displayedScore += 2;
            } else {
                displayedScore += 1;
            }
        }

        scoreText.text = displayedScore.ToString("000000");
    }

    public void AddScore(string scoreText, int scoreValue) {
        GameObject newObject = Instantiate(subScoreTextPrefab, scoreMask, false);
        ScoreSubText subText = newObject.GetComponent<ScoreSubText>();
        subText.Init(scoreText, scoreValue);
        subScores.Add(subText);
        subText.NavigateToPosition(subScores.Count - 1, arrivalTime);
        timeUntilStay = Time.time + stayTime;
    } 

}
