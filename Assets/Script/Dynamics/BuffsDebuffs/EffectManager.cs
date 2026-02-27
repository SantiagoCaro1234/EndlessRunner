using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    private List<IEffect> activeEffects = new List<IEffect>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        List<IEffect> effectsCopy = new List<IEffect>(activeEffects);
        foreach (var effect in effectsCopy)
        {
            effect.Update(Time.unscaledDeltaTime); // usamos unscaled para que la duración no sea afectada por slow motion
            if (effect.IsExpired)
            {
                activeEffects.Remove(effect);
            }
        }
    }

    public void AddEffect(IEffect effect)
    {
        effect.Apply();
        activeEffects.Add(effect);
    }

    public void RemoveEffect(IEffect effect)
    {
        if (activeEffects.Contains(effect))
        {
            effect.Revert();
            activeEffects.Remove(effect);
        }
    }

    public void ClearAllEffects()
    {
        foreach (var effect in activeEffects)
        {
            effect.Revert();
        }
        activeEffects.Clear();
    }
}