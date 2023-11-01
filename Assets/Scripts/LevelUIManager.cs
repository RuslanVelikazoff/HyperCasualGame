using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    public Button closeLevelButton;

    public Button restartLevelButton;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (closeLevelButton != null)
        {
            closeLevelButton.onClick.RemoveAllListeners();
            closeLevelButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }

        if (restartLevelButton != null)
        {
            restartLevelButton.onClick.RemoveAllListeners();
            restartLevelButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
    }
}
