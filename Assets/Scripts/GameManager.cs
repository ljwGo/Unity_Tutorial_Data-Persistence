using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float InitSpeed { set; get; }
    public string PlayerName { set; get; }

    public static GameManager Instance { get; private set; }
    private AudioSource bgm;

    // It is no clear to know the meaning of value.
    private Dictionary<string, float> levelDifficultMap = new Dictionary<string, float> {
        {"easy", 2f },
        {"medium", 2.5f },
        {"hard", 3f },
    };

    private void Awake() {
        // Single mode
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }

    public void OnGameRestartBtnClick() {
        // hard code
        SceneManager.LoadScene(1);
    }

    public void AdjustVolumn(float volumn) {
        bgm.volume = volumn;
    }

    public void OnSetDifficultyBtnClick(string difficulty) {
        // em... Other script to obtain this InitSpeed
        InitSpeed = levelDifficultMap[difficulty];
        GameStart();
    }

    public void SetPlayName(string playName) {
        PlayerName = playName;
    }

    public void GameStart() {
        SceneManager.LoadScene(1);
    }
}
