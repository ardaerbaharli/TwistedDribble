using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{
    private enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    [SerializeField] private Transform pivot;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private RotationDirection rotationDirection;
    [SerializeField] private bool isRotating;
    [SerializeField] private bool isDragging;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private AudioSource ballHit;

    private float pivotBound;
    private float distance;
    private RectTransform ballRect;
    private RectTransform pivotRect;


    private void Awake()
    {
        isRotating = true;
        rotationDirection = RotationDirection.Clockwise;
        ballRect = GetComponent<RectTransform>();
        pivotRect = pivot.GetComponent<RectTransform>();

        var screenWidth = GameManager.instance.canvasWidth;
        distance = Mathf.Abs(ballRect.position.x - pivotRect.position.x);
        var ballWidth = ballRect.sizeDelta.x;
        pivotBound = screenWidth / 2 - distance - ballWidth;
    }

    private void Start()
    {
        inputHandler.onEndDrag_ += OnEndDrag;
        inputHandler.onBeginDrag_ += OnBeginDrag;
        inputHandler.onDrag_ += OnDrag;
        inputHandler.onPointerUp_ += OnPointerUp;
        GameManager.instance.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Destroy(gameObject);
    }


    private void OnBeginDrag()
    {
        isDragging = true;
    }

    private void OnDrag(PointerEventData data)
    {
        if (!isDragging) return;

        var deltaP = new Vector2(data.delta.x, 0);
        var pivotPos = pivotRect.anchoredPosition + deltaP;
        pivotPos.x = Mathf.Clamp(pivotPos.x, -pivotBound, pivotBound);
        pivotRect.anchoredPosition = pivotPos;
    }

    private void OnEndDrag()
    {
        isDragging = false;
    }

    private void OnPointerUp()
    {
        ChangeRotation();
    }


    private void ChangeRotation()
    {
        if (isDragging) return;
        if (!isRotating) return;
        rotationDirection = rotationDirection == RotationDirection.Clockwise
            ? RotationDirection.CounterClockwise
            : RotationDirection.Clockwise;
    }


    private void Update()
    {
        if (!isRotating) return;

        var pivPos = pivot.position;
        transform.RotateAround(pivPos,
            rotationDirection == RotationDirection.Clockwise ? Vector3.forward : Vector3.back,
            speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        inputHandler.onEndDrag_ -= OnEndDrag;
        inputHandler.onBeginDrag_ -= OnBeginDrag;
        inputHandler.onDrag_ -= OnDrag;
        inputHandler.onPointerUp_ -= OnPointerUp;
        GameManager.instance.onGameOver -= OnGameOver;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Silhouette")) return;
        GameManager.instance.OnLoseLife();
        col.gameObject.SetActive(false);
        ballHit.Play();
    }
}