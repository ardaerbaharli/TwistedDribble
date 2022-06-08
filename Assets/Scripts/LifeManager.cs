using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private List<Life> lives;

    public int remainingLives;

    private void Awake()
    {
        GameManager.instance.loseLife += LoseLife;
    }

    private void Start()
    {
        remainingLives = lives.Count;
    }

    private void LoseLife()
    {
        remainingLives--;
        lives.First(x => x.IsAlive).Toggle(false);
        if (remainingLives <= 0)
            GameManager.instance.GameOver();
    }

    private void OnDestroy()
    {
        GameManager.instance.loseLife -= LoseLife;
    }
}