using UnityEngine;

public class HeartEffect : IEffect
{
    private PlayerStats playerStats;
    private int healthAmount;

    public HeartEffect(PlayerStats playerStats, int healthAmount)
    {
        this.playerStats = playerStats;
        this.healthAmount = healthAmount;
    }

    public void Apply()
    {
        if (playerStats != null)
        {
            playerStats.AddHealth(healthAmount);
            Debug.Log("Heart effect: vida extra");
        }
    }

    public void Revert() { }
    public void Update(float deltaTime) { }
    public bool IsExpired => true;
}