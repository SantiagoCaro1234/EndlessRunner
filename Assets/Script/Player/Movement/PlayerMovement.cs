using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistanceThreshold;
    [SerializeField] private float jumpTime = 0.3f;

    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private Vector2 slideColliderSize = new Vector2(1, .5f);
    [SerializeField] private Vector2 slideColliderOffset = new Vector2(0, -.25f);

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isSliding;
    private float jumpTimer;
    private float slideTimer;

    private IInputProvider jumpInput;
    private ISlideInputProvider slideInput;

    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    public bool IsGrounded => isGrounded;

    public void Initialize(IInputProvider jumpProvider, ISlideInputProvider slideProvider)
    {
        jumpInput = jumpProvider;
        slideInput = slideProvider;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();

        // guardo tamanio y offset originales del collider
        if (boxCollider != null)
        {
            originalColliderSize = boxCollider.size;
            originalColliderOffset = boxCollider.offset;
        }
    }

    private void Update()
    {
        // detectar suelo
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistanceThreshold, groundLayer);

        // --- logica de salto ---

        if (jumpInput != null)
        {
            if (isGrounded && jumpInput.wasJumpPressedThisFrame)
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (isJumping && jumpInput.isJumpPressed)
            {
                if (jumpTimer < jumpTime)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimer += Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (jumpInput.wasJumpReleasedThisFrame)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }

        // --- logica de slide ---
        if (slideInput != null)
        {
            // activo el slide solo si estoy en suelo, no estoy ya deslizando y se detecto el gesto
            if (isGrounded && !isSliding && slideInput.wasSlideDownThisFrame)
            {
                Slide();
            }

            // mientras estoy deslizando, controlo el temporizador
            if (isSliding)
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0f)
                {
                    // termino el slide: restauro el collider original
                    EndSlide();
                }
            }
        }
    }

    private void Slide()
    {
        // aca modifico el collider para achicarlo verticalmente
        if (boxCollider != null)
        {
            boxCollider.size = slideColliderSize;
            boxCollider.offset = slideColliderOffset;
        }

        isSliding = true;
        slideTimer = slideDuration;

        // aqui se podria activar una animacion de slide
        Debug.Log("slide iniciado");
    }

    private void EndSlide()
    {
        // restauro el collider a su tamanio y offset originales
        if (boxCollider != null)
        {
            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
        }

        isSliding = false;

        Debug.Log("slide terminado");
    }
}