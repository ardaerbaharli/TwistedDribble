using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class SilhouetteSpawner : MonoBehaviour
{
    [SerializeField] private float interval;
    [SerializeField] private RectTransform biggestSilhouette;
    [SerializeField] private float spawnY;
    private float _timePassed;
    private float _bound;

    private void Awake()
    {
        var w = GameManager.instance.canvasWidth;
        _bound = w / 2 - biggestSilhouette.sizeDelta.x * biggestSilhouette.localScale.x / 2;
    }

    private void Start()
    {
        CreateSilhouette();
        GameManager.instance.onScore += SpeedUp;
    }

    private void SpeedUp(int score)
    {
        if (interval > 1f)
            interval -= 0.05f;
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed <= interval) return;
        _timePassed = 0.0f;

        CreateSilhouette();
    }

    private void CreateSilhouette()
    {
        var silhouette = ObjectPool.instance.GetPooledObject("Silhouette");
        silhouette.rectTransform.anchoredPosition = GetRandomPosition();
        silhouette.rectTransform.rotation = GetRandomRotation();
        silhouette.image.sprite = GetRandomImage();
        silhouette.gameObject.SetActive(true);
    }


    private Sprite GetRandomImage()
    {
        var index = Range(0, 7);
        return Resources.Load<Sprite>("Silhouettes/" + index);
    }

    private Quaternion GetRandomRotation()
    {
        var random = Range(0, 2);
        return Quaternion.Euler(0, random == 1 ? 180 : 0, 0);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Range(-_bound, _bound), spawnY, 0);
    }
}