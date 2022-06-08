using System;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private GameObject deadImage;
    [SerializeField] private GameObject aliveImage;

    public bool IsAlive;

    private void Awake()
    {
        IsAlive = true;
    }

    public void Toggle(bool isAlive)
    {
        IsAlive = isAlive;
        deadImage.SetActive(!isAlive);
        aliveImage.SetActive(isAlive);
    }
}