using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Game/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public string powerUpName;
    public GameObject prefab;
    public EffectType effectType;
    public float effectValue;
    public float effectDuration;
    public Sprite icon;
}

public enum EffectType
{
    JumpBoost,
    SlowMotion,
    Heart,
    Bomb
}