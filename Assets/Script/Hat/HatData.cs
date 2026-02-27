using UnityEngine;

[CreateAssetMenu(fileName = "NewHat", menuName = "Game/Hat Data")]
public class HatData : ScriptableObject
{
    public string hatName;
    public int price;
    public Sprite hatSprite;
}