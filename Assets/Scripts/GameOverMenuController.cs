using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private ToggleSwitch soundToggle;

    private void Awake()
    {
        soundToggle.valueChanged += SoundToggleValueChanged;
        var soundValue = PlayerPrefs.GetInt("Sound", 1) == 1;
        soundToggle.Toggle(soundValue);

        var score = GameManager.instance.score;
        currentScoreText.text = $"{score}m";
            
        var bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"Best score: {bestScore}m";
    }

    private void SoundToggleValueChanged(bool value)
    {
        SoundManager.instance.SetSound(value);
    }

    public void OnRestartuttonClicked()
    {
        GameManager.instance.RestartGame();
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager.instance.LoadMainMenu();
    }
}