using UnityEngine;

// maneja las animaciones especificas del personaje
public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private AnimationManager animManager; // referencia al manager generico
    [SerializeField] private PlayerMovement playerMovement; // referencia al movimiento del jugador

    // nombres de parametros del animator (constantes para evitar errores)
    private const string PARAM_IS_GROUNDED = "isGrounded";
    private const string PARAM_IS_JUMPING = "isJumping";
    private const string PARAM_IS_SLIDING = "isSliding";
    private const string PARAM_SPEED = "speed";

    private void Start()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (animManager == null || playerMovement == null) return;

        // actualiza parametros segun el estado del jugador
        animManager.SetBool(PARAM_IS_GROUNDED, playerMovement.IsGrounded);
        animManager.SetBool(PARAM_IS_JUMPING, playerMovement.IsJumping);
        animManager.SetBool(PARAM_IS_SLIDING, playerMovement.IsSliding);
    }

    // metodos publicos para eventos externos (por ejemplo, desde el player al saltar)
    public void TriggerJump()
    {
        animManager.SetTrigger("jump");
    }

    public void TriggerSlide()
    {
        animManager.SetTrigger("slide");
    }
}