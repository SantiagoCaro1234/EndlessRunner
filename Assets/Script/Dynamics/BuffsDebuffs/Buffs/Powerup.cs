using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PowerUpData data;
    private PowerUpPool pool;

    public void Init(PowerUpData data, PowerUpPool owner)
    {
        this.data = data;
        this.pool = owner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect();
            pool?.Return(this);
        }
    }

    private void ApplyEffect()
    {
        if (data == null) return;

        IEffect effect = null;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        PlayerStats stats = FindObjectOfType<PlayerStats>();

        switch (data.effectType)
        {
            case EffectType.JumpBoost:
                if (player != null)
                    effect = new JumpBuffEffect(player, data.effectValue, data.effectDuration);
                break;
            case EffectType.SlowMotion:
                effect = new SlowMotionEffect(data.effectValue, data.effectDuration);
                break;
            case EffectType.Heart:
                if (stats != null)
                    effect = new HeartEffect(stats, (int)data.effectValue);
                break;
            case EffectType.Bomb:
                if (player != null)
                    effect = new BombEffect(player, data.effectValue);
                break;
        }

        if (effect != null)
            EffectManager.Instance.AddEffect(effect);
    }

    private void OnBecameInvisible()
    {
        pool?.Return(this);
    }
}