
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private GameObject startMenu;
    private GameObject gameOverTip;
    private GameObject gamePauseBg;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI livesText;
    private TMP_InputField playNameInput;
    private Button restartBtn;
    private Slider volumnSlider;

    void Start()
    {
        startMenu = transform.Find("StartMenu").gameObject;
        gameOverTip = transform.Find("GameOverTip").gameObject;
        gamePauseBg = transform.Find("PauseCoverBg").gameObject;
        scoreText = transform.Find("ScoreText").gameObject.GetComponent<TextMeshProUGUI>();
        livesText = transform.Find("LivesText").gameObject.GetComponent<TextMeshProUGUI>();
        restartBtn = gameOverTip.transform.Find("RestartBtn").GetComponent<Button>();
        volumnSlider = transform.Find("VolumnSlider").GetComponent<Slider>();
        playNameInput = startMenu.transform.Find("PlayNameInput").GetComponent<TMP_InputField>();

        restartBtn.onClick.AddListener(EventSystem.GameRestartEvent);
        volumnSlider.onValueChanged.AddListener(AdjustVolumn);
        playNameInput.onEndEdit.AddListener(SetPlayName);
    }

    public void UpdateScore(float score) {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int live) {
        livesText.text = $"Lives: {live}";
    }

    public void OnGameStart() {
        startMenu.SetActive(false);
        volumnSlider.gameObject.SetActive(false);
    }

    public void GamePause() {
        gamePauseBg.SetActive(true);
    }

    public void GamePauseOver() {
        gamePauseBg.SetActive(false);
    }

    public void OnGameOver() {
        gameOverTip.SetActive(true);
    }

    public void AdjustVolumn(float _) {
        float volumn = volumnSlider.value;
        EventSystem.VolumnAdjustEvent(volumn);
    }

    public void SetPlayName(string name) {
        EventSystem.SetPlayNameEvent(name);
    }
}
