using UnityEngine;

public class TouchInputProvider : IInputProvider
{
    private bool jumpPressed;
    private bool jumpPressedThisFrame;
    private bool jumpReleasedThisFrame;

    public bool isJumpPressed => jumpPressed;
    public bool wasJumpPressedThisFrame => jumpPressedThisFrame;
    public bool wasJumpReleasedThisFrame => jumpReleasedThisFrame;

    public void Update()
    {
        // reiniciamos flags de frame
        jumpPressedThisFrame = false;
        jumpReleasedThisFrame = false;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        jumpPressed = true;
                        jumpPressedThisFrame = true;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        jumpPressed = false;
                        jumpReleasedThisFrame = true;
                        break;
                        // moved y stationary no cambian el estado
                }
            }
        }
        else
        {
            // si no hay toques, aseguramos que no este presionado
            // esto evita que se quede trabado si el toque termina fuera de la app
            if (jumpPressed)
            {
                jumpPressed = false;
                jumpReleasedThisFrame = true;
            }
        }
    }
}