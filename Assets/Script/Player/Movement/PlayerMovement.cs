using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private SpriteRenderer srenderer;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistanceThreshold;
    [SerializeField] private float jumpTime = 0.3f;

    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float slideCooldown= 0.2f;
    [SerializeField] private Vector2 slideColliderSize = new Vector2(1, .5f);
    [SerializeField] private Vector2 slideColliderOffset = new Vector2(0, -.25f);
    [SerializeField] private bool isProlongingSlide;
    [SerializeField] private bool slideOnCooldown;

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

    public float JumpForce => jumpForce;

    public void SetJumpForce(float newForce)
    {
        jumpForce = newForce;
    }

    public bool IsGrounded => isGrounded;
    public bool IsJumping => isJumping;
    public bool IsSliding => isSliding;

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
            if (isGrounded && jumpInput.wasJumpPressedThisFrame && !isSliding)
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                Debug.Log("Jumping");
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

            if (isGrounded && !jumpInput.isJumpPressed)
            {
                isJumping = false;
            }
        }

        // --- logica de slide ---
        if (slideInput != null)
        {
            // activo el slide solo si estoy en suelo, no estoy ya deslizando y se detecto el gesto
            if (isGrounded && !isSliding && !slideOnCooldown && slideInput.wasSlideDownThisFrame)
            {
                Slide();
            }

            if (isSliding)
            {
                if (!isProlongingSlide)
                {
                    slideTimer -= Time.deltaTime;
                    if (slideTimer <= 0f)
                        EndSlide();
                }
                // si isProlongingSlide es true, el timer no disminuye
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

        srenderer.sortingOrder = 1;
        srenderer.sortingLayerName = "Middleground";
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
        srenderer.sortingOrder = 0;
        srenderer.sortingLayerName = "Entity";

        StartCoroutine(SlideCooldownRoutine());
    }

    private IEnumerator SlideCooldownRoutine()
    {
        slideOnCooldown = true;
        yield return new WaitForSeconds(slideCooldown);
        slideOnCooldown = false;
    }

    public void SetSlideProlongation(bool prolonging)
    {
        isProlongingSlide = prolonging;
    }
}