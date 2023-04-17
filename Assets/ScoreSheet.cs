using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ScoreSheet : MonoBehaviour
{
    abstract public Score GetScore(float remainingHealthPercent, float damageDealtPercent, string collidedTag);

    [System.Serializable]
    public class Score {
        public string scoreText;
        public int score;

        public Score(string scoreText, int score) {
            this.scoreText = scoreText;
            this.score = score;
        }

    }

}
