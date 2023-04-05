using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{
    public string difficulty;
    public GameManager gameManager;

    private Button difficultySelectBtn;

    void Start()
    {
        difficultySelectBtn = GetComponent<Button>();
        difficultySelectBtn.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty() {
        // it is no convenience to push new event
        EventSystem.SetDifficultyEvent(difficulty);
    }
}
