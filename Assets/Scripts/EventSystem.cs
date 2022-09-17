
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Slider;

public class EventSystem : MonoBehaviour {

    public static UnityAction GameStartEvent;
    public static UnityAction GameRestartEvent;
    public static UnityAction GamePauseEvent;
    public static UnityAction GamePauseOverEvent;
    public static UnityAction GameOverEvent;
    public static UnityAction<int> HpUpdateEvent;
    public static UnityAction<float> ScoreUpdateEvent;
    public static UnityAction<float> VolumnAdjustEvent;
    public static UnityAction<string> SetDifficultyEvent;
    public static UnityAction<string> SetPlayNameEvent;

    private GameManager gameManager;
    private UIManager uiManager;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        GameStartEvent = uiManager.OnGameStart;
        GameRestartEvent = gameManager.OnGameRestartBtnClick;
        GameOverEvent = uiManager.OnGameOver;
        HpUpdateEvent = uiManager.UpdateLives;
        ScoreUpdateEvent = uiManager.UpdateScore;
        VolumnAdjustEvent = gameManager.AdjustVolumn;
        SetDifficultyEvent = gameManager.OnSetDifficultyBtnClick;
        GamePauseEvent = uiManager.GamePause;
        GamePauseOverEvent = uiManager.GamePauseOver;
        SetPlayNameEvent = gameManager.SetPlayName;
    }
}
