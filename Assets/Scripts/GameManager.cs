using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] private float startSpeed;
    [SerializeField] private RectTransform canvas;
    public float canvasWidth;
private float canvasHeight;

public static GameManager instance;
    public float speed;

    public delegate void LoseLife();

    public event LoseLife loseLife;

    public delegate void OnScore(int score);

    public event OnScore onScore;

    public delegate void OnGameOver();

    public event OnGameOver onGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // fix fps and vsync
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;

        score = 0;
        speed = startSpeed;
    }

    public void OnLoseLife()
    {
        loseLife?.Invoke();
    }

    public void Score()
    {
        speed += 1f;
        score++;
        onScore?.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        var bestScore = PlayerPrefs.GetInt("BestScore");
        if (score > bestScore)
            PlayerPrefs.SetInt("BestScore", score);
        onGameOver?.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        canvasWidth = canvas.rect.width;
        canvasHeight = canvas.rect.height;
    }

    public void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Main");
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}