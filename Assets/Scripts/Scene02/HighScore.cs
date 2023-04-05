using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI currentScoreT;
    public TextMeshProUGUI highScoreT;

    public int maxValue;

    private int highScore;

    void Start()
    {
        // update upon high score.
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreT.text = highScore.ToString();
    }

    public void Roll() {
        int currentScore = Random.Range(1, maxValue+1);
        currentScoreT.text = currentScore.ToString();

        if (currentScore > highScore) {
            PlayerPrefs.SetInt("highScore", currentScore);
            highScore = currentScore;
            // update score text
            highScoreT.text = highScore.ToString();
        }
    }

    public void Reset() {
        PlayerPrefs.DeleteKey("highScore");

        highScore = 0;
        highScoreT.text = "0";
    }
}
