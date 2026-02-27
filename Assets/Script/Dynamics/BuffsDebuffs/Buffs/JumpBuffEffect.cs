using UnityEngine;

public class JumpBuffEffect : Effect
{
    private PlayerMovement player;
    private float originalJumpForce;
    private float multiplier;

    public JumpBuffEffect(PlayerMovement player, float multiplier, float duration) : base(duration)
    {
        this.player = player;
        this.multiplier = multiplier;
    }

    public override void Apply()
    {
        if (player != null)
        {
            originalJumpForce = player.JumpForce;

            player.SetJumpForce(originalJumpForce * multiplier);
            Debug.Log($"JumpBoost activado: multiplicador {multiplier}x por {duration}s");
        }
    }

    public override void Revert()
    {
        if (player != null)
        {
            player.SetJumpForce(originalJumpForce);
            Debug.Log("JumpBoost terminado, fuerza restaurada");
        }
    }
}