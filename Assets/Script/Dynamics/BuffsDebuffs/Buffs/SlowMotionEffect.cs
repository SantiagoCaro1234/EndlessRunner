using UnityEngine;

public class SlowMotionEffect : Effect
{
    private float originalTimeScale;
    private float originalFixedDeltaTime;
    private float factor;

    public SlowMotionEffect(float factor, float duration) : base(duration)
    {
        this.factor = Mathf.Clamp(factor, 0.1f, 1f); // no menos de 0.1 para no congelar
    }

    public override void Apply()
    {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = factor;
        Time.fixedDeltaTime = originalFixedDeltaTime * factor;
        Debug.Log($"SlowMotion activado: factor {factor}x por {duration}s");
    }

    public override void Revert()
    {
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
        Debug.Log("SlowMotion terminado, tiempo restaurado");
    }
}