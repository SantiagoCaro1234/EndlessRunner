using UnityEngine;

// proveedor de salto con retardo para distinguir entre tap y swipe
public class DelayedJumpInputProvider : IInputProvider
{
    // estado interno
    private bool jumpPressed;
    private bool jumpPressedThisFrame;
    private bool jumpReleasedThisFrame;

    private float touchStartTime;
    private Vector2 touchStartPos;
    private bool touchActive;
    private bool jumpActivated;      // indica si ya se confirmo el salto para este toque
    private bool movementDetected;   // true si el dedo se movio demasiado durante el retardo

    // configuracion (puedes hacerlos publicos para ajustarlos desde InputManager)
    private float tapDelay = 0.1f;           // tiempo antes de activar el salto (en segundos)
    private float maxMoveBeforeCancel = 20f; // pixels de movimiento que cancelan el salto

    // propiedades de interfaz
    public bool isJumpPressed => jumpPressed;
    public bool wasJumpPressedThisFrame => jumpPressedThisFrame;
    public bool wasJumpReleasedThisFrame => jumpReleasedThisFrame;

    // metodo para actualizar cada frame (llamado desde InputManager)
    public void Update()
    {
        // reinicia flags de frame
        jumpPressedThisFrame = false;
        jumpReleasedThisFrame = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // comienza un nuevo toque
                    touchStartTime = Time.time;
                    touchStartPos = touch.position;
                    touchActive = true;
                    jumpActivated = false;
                    movementDetected = false;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (touchActive && !jumpActivated)
                    {
                        // verifica si el dedo se movio mas de lo permitido
                        float moveDistance = Vector2.Distance(touch.position, touchStartPos);
                        if (moveDistance > maxMoveBeforeCancel)
                        {
                            movementDetected = true;
                        }

                        // si paso el tiempo de retardo sin movimiento, activa el salto ahora
                        if (!movementDetected && (Time.time - touchStartTime) >= tapDelay)
                        {
                            // activa el salto en este frame
                            jumpPressed = true;
                            jumpPressedThisFrame = true;
                            jumpActivated = true;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touchActive)
                    {
                        if (!movementDetected && !jumpActivated)
                        {
                            // fue un tap rapido sin movimiento: activa salto al soltar
                            jumpPressed = true;
                            jumpPressedThisFrame = true;
                            jumpActivated = true;
                            // como el toque termina, inmediatamente lo suelta
                            jumpPressed = false;
                            jumpReleasedThisFrame = true;
                        }
                        else if (jumpActivated)
                        {
                            // el salto ya estaba activo, al soltar lo desactiva
                            jumpPressed = false;
                            jumpReleasedThisFrame = true;
                        }
                        // si hubo movimiento y no se activo salto, no se hace nada (fue un swipe)
                    }
                    touchActive = false;
                    break;
            }

            // mantiene el estado de presionado mientras el toque esta activo y el salto fue activado
            if (touchActive && jumpActivated)
            {
                jumpPressed = true;
            }
            else if (!touchActive)
            {
                jumpPressed = false;
            }
        }
        else
        {
            // no hay toques: resetea todo
            if (touchActive)
            {
                touchActive = false;
                if (jumpActivated)
                {
                    jumpPressed = false;
                    jumpReleasedThisFrame = true;
                }
            }
            jumpActivated = false;
        }
    }

    // metodo para cancelar un salto pendiente (lo llama InputManager cuando empieza un swipe)
    public void CancelPendingJump()
    {
        if (touchActive && !jumpActivated)
        {
            movementDetected = true; // evita que se active el salto para este toque
        }
    }
}