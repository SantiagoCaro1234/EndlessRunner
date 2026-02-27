using UnityEngine;

public class BombEffect : IEffect
{
    private PlayerMovement player;
    private float boostForce;

    public BombEffect(PlayerMovement player, float boostForce)
    {
        this.player = player;
        this.boostForce = boostForce;
    }

    public void Apply()
    {
        if (player != null)
        {
            // Aplicar un impulso vertical hacia arriba
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, boostForce);
            }
            Debug.Log("Bomb effect: salto impulsado");
        }
    }

    public void Revert()
    {
        // No hay reversión, es instantáneo
    }

    public void Update(float deltaTime)
    {
        // No hace nada, es instantáneo
    }

    public bool IsExpired => true; // Se considera expirado inmediatamente
}