using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // add this line make unity to show in inspector
    [System.Serializable]
    public class MaxSocre {
        public string name;
        public int maxScore;
    }

    public int LineCount = 6;
    public Brick BrickPrefab;
    public Rigidbody Ball;

    public Text ScoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject GameOverText;

    private bool m_GameOver = false;
    private bool m_Started = false;
    private int m_Points;
    private float initSpeed;
    private string playerName;
    private MaxSocre currentMaxScore;

    // Start is called before the first frame update
    void Start()
    {
        initSpeed = GameManager.Instance.InitSpeed;
        playerName = GameManager.Instance.PlayerName;
        // default value assign if varible is null
        playerName ??= "undefined";
        currentMaxScore = LoadMaxScoreInfo();
        Debug.Log(currentMaxScore);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * initSpeed, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // 如果当次分数比最高记录高
        if (currentMaxScore == null || m_Points > currentMaxScore.maxScore) {
            currentMaxScore = SaveMaxScoreInfo();
        }

        ShowMaxScoreInfo();
    }

    public MaxSocre LoadMaxScoreInfo() {
        // important
        string path = Application.persistentDataPath + "\\maxScore.json";
        if (File.Exists(path)) {
            string jsonStr = File.ReadAllText(path);

            return JsonUtility.FromJson<MaxSocre>(jsonStr);
        }
        return null;
    }

    public MaxSocre SaveMaxScoreInfo() {
        MaxSocre maxSocre = new MaxSocre();
        maxSocre.maxScore = m_Points;
        maxSocre.name = playerName;

        string jsonStr = JsonUtility.ToJson(maxSocre);
        // async.
        File.WriteAllTextAsync(Application.persistentDataPath + "\\maxScore.json", jsonStr);
        return maxSocre;
    }

    public void ShowMaxScoreInfo() {
        bestScoreText.text = $"Best Score:{currentMaxScore.name} : {currentMaxScore.maxScore}";
    }
}
