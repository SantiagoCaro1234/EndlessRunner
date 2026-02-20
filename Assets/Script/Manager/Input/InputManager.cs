using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    [Header("ajustes de salto")]
    [SerializeField] private float tapDelay = 0.1f;
    [SerializeField] private float maxMoveBeforeCancel = 20f;

    [Header("ajustes de swipe")]
    [SerializeField] private float minSwipeDistance = 150f;
    [SerializeField] private float maxSwipeTime = 0.5f;

    private DelayedJumpInputProvider jumpInput;
    private SwipeDownDetector slideInput;

    private void Awake()
    {
        // crea los proveedores
        jumpInput = new DelayedJumpInputProvider();
        slideInput = new SwipeDownDetector();

        // suscribe el evento de inicio de swipe para cancelar el salto pendiente
        slideInput.OnSwipeDownStarted += OnSwipeDownStarted;

        // inyecta los proveedores en el player
        if (player != null)
            player.Initialize(jumpInput, slideInput);
    }

    private void OnSwipeDownStarted()
    {
        // cuando el swipe comienza, cancela cualquier salto que este en periodo de retardo
        jumpInput?.CancelPendingJump();
    }

    private void Update()
    {
        // actualiza ambos proveedores cada frame
        jumpInput?.Update();
        slideInput?.Update();
    }

    private void OnDestroy()
    {
        // limpia la suscripcion
        if (slideInput != null)
            slideInput.OnSwipeDownStarted -= OnSwipeDownStarted;
    }
}