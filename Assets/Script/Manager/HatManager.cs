using UnityEngine;

public class HatManager : MonoBehaviour
{
    [SerializeField] private Transform hatAnchor; // padres de los GameObjects de sombreros

    private void Start()
    {
        foreach (Transform child in hatAnchor)
            child.gameObject.SetActive(false);

        int equippedId = HatPool.Instance.GetEquippedHat();
        if (equippedId >= 0 && equippedId < hatAnchor.childCount)
            hatAnchor.GetChild(equippedId).gameObject.SetActive(true);
    }
}