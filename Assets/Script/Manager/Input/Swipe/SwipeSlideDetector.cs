using UnityEngine;
using System;

public class SwipeSlideDetector : ISlideInputProvider
{
    private Vector2 startTouchPosition;
    private bool slideDownThisFrame;

    public bool wasSlideDownThisFrame => slideDownThisFrame;

    private float minSlideDistance = 100f;
    private float minVerticalRatio;

    public SwipeSlideDetector(float minDistance = 100f, float verticalRatio = 0.7f)
    {
        minSlideDistance = minDistance;
        minVerticalRatio = verticalRatio;
        if (SwipeManager.Instance != null)
        {
            SwipeManager.Instance.OnSwipeEnd += OnSwipeEnd;
        }
        else
        {
            Debug.LogError("swipemanager instance not found");
        }
    }

    private void OnSwipeEnd(SwipeData data)
    {
        if (data.points.Count < 2) return;

        Vector2 first = data.points[0];
        Vector2 last = data.points[data.points.Count - 1];
        Vector2 delta = last - first;

        // deslizamiento hacia abajo: delta.y negativo y predominio vertical
        if (delta.y < -minSlideDistance && Mathf.Abs(delta.y) > Mathf.Abs(delta.x) * minVerticalRatio)
        {
            slideDownThisFrame = true;
        }
    }

    public void Update()
    {
        slideDownThisFrame = false; // se reinicia cada frame

        // aca detecto el inicio del toque
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        // aca detecto cuando el toque termina
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 endTouchPosition = Input.GetTouch(0).position;

            // calculo la diferencia en el eje y
            float deltaY = endTouchPosition.y - startTouchPosition.y;

            // considero un deslizamiento hacia abajo si la y final es menor que la inicial
            // (con una tolerancia para evitar movimientos horizontales puros)
            if (deltaY < 0 && Mathf.Abs(deltaY) > Mathf.Abs(endTouchPosition.x - startTouchPosition.x))
            {
                // es un swipe hacia abajo
                slideDownThisFrame = true;
            }
        }
    }

    public void ResetFrameFlags()
    {
        slideDownThisFrame = false;
    }
}