using UnityEngine;

public abstract class Effect : IEffect
{
    protected float duration;
    protected float timer;

    public bool IsExpired => timer <= 0f;

    public Effect(float duration)
    {
        this.duration = duration;
        this.timer = duration;
    }

    public virtual void Apply() { }
    public virtual void Revert() { }

    public void Update(float deltaTime)
    {
        if (timer > 0f)
        {
            timer -= deltaTime;
            if (timer <= 0f)
            {
                Revert();
            }
        }
    }
}