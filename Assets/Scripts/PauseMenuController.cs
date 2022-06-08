using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private ToggleSwitch soundToggle;

    private void Awake()
    {
        soundToggle.valueChanged += SoundToggleValueChanged;
        var soundValue = PlayerPrefs.GetInt("Sound", 1) == 1;
        soundToggle.Toggle(soundValue);

        var score = GameManager.instance.score;
        currentScoreText.text = $"{score}m";
    }

    private void SoundToggleValueChanged(bool value)
    {
        SoundManager.instance.SetSound(value);
    }

    public void OnResumeButtonClicked()
    {
        gameObject.SetActive(false);
        GameManager.instance.ResumeGame();
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager.instance.LoadMainMenu();
    }
}