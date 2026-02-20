using System;
using UnityEngine;

// este proveedor detecta deslizamiento vertical hacia abajo
public class SwipeDownDetector : ISlideInputProvider
{
    private Vector2 startTouchPosition;
    private float startTouchTime;
    private bool slideDownThisFrame;
    private bool swipeStarted; // true cuando el movimiento ya supero el umbral

    private float minSwipeDistance = 150f;  // distancia minima en pixeles
    private float maxSwipeTime = 0.5f;      // tiempo maximo para completar el swipe

    public event Action OnSwipeDownStarted;

    public bool wasSlideDownThisFrame => slideDownThisFrame;

    // se llama una vez por frame desde el input manager
    public void Update()
    {
        slideDownThisFrame = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    startTouchTime = Time.time;
                    swipeStarted = false;
                    break;

                case TouchPhase.Moved:
                    if (!swipeStarted)
                    {
                        float deltaY = touch.position.y - startTouchPosition.y;
                        float deltaX = touch.position.x - startTouchPosition.x;
                        float timeDelta = Time.time - startTouchTime;

                        // condiciones para considerar que ha comenzado un swipe hacia abajo
                        if (deltaY < -minSwipeDistance && timeDelta < maxSwipeTime && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
                        {
                            swipeStarted = true;
                            OnSwipeDownStarted?.Invoke(); // avisa al InputManager
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (swipeStarted)
                    {
                        slideDownThisFrame = true; // notifica que el swipe termino
                    }
                    swipeStarted = false;
                    break;
            }
        }
    }
}