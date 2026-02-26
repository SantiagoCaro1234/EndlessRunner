using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleConfig", menuName = "Game/Obstacle Config")]
public class ObstacleConfigSO : ScriptableObject
{
    public float yOffset;
    public bool repeatable;
}