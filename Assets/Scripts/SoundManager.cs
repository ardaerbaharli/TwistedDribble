using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource gameBackgroundSound;


    public static SoundManager instance;

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
    }

    private void Start()
    {
        GameManager.instance.onGameOver += OnGameOver;
        SetSound(PlayerPrefs.GetInt("Sound", 1) == 1);
        gameBackgroundSound.Play();
    }

    private void OnGameOver()
    {
        gameOverSound.Play();
    }

    public void SetSound(bool value)
    {
        print(value);
        PlayerPrefs.SetInt("Sound", value ? 1 : 0);
        mixer.SetFloat("Master", value ? 0 : -80);
    }
}