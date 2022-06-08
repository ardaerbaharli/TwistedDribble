using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ToggleSwitch soundToggle;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject instructionsPanel;


    private void Awake()
    {
        soundToggle.valueChanged += SoundToggleValueChanged;
        var soundValue = PlayerPrefs.GetInt("Sound", 1) == 1;
        soundToggle.Toggle(soundValue);
    }

    private void Start()
    {
        var bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"Best score: {bestScore}";
    }

    private void SoundToggleValueChanged(bool value)
    {
        SoundManager.instance.SetSound(value);
    }

    private void OnDestroy()
    {
        soundToggle.valueChanged -= SoundToggleValueChanged;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }

    public void OnInstructionsButtonClick()
    {
        gameObject.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }
}