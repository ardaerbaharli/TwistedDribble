using System;
using UnityEngine;

public class Silhouette : MonoBehaviour
{
    [SerializeField] private float speed;

    private void OnEnable()
    {
        speed = GameManager.instance.speed;
    }

    private void Update()
    {
        // Go down at speed
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("BottomTrigger"))
        {
            GameManager.instance.Score();
            gameObject.SetActive(false);
        }
    }
}