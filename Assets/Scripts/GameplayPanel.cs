using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private Text currentScore;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverPanel;


    private void Awake()
    {
        GameManager.instance.onScore += OnScoreChanged;
        GameManager.instance.onGameOver += OnGameOver;
        currentScore.text = "0m";
    }

    private void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        Destroy(gameObject);
    }

    private void OnScoreChanged(int score)
    {
        currentScore.text = $"{score}m";
    }

    public void OnPauseButtonClicked()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.PauseGame();
    }

    private void OnDestroy()
    {
        GameManager.instance.onScore -= OnScoreChanged;
        GameManager.instance.onGameOver -= OnGameOver;
    }
}