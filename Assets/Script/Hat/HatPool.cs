using UnityEngine;
using System.Collections.Generic;
using System;

public class HatPool : MonoBehaviour
{
    public static HatPool Instance { get; private set; }

    [SerializeField] private HatData[] allHats; // ordenado por ID
    private List<int> ownedHats = new List<int>();
    private int equippedHat = -1;

    public event Action<int> OnOwnedChanged; // opcional, para actualizar UI

    private const string OwnedKey = "OwnedHats";
    private const string EquippedKey = "EquippedHat";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadData()
    {
        string saved = PlayerPrefs.GetString(OwnedKey, "");
        if (!string.IsNullOrEmpty(saved))
        {
            string[] ids = saved.Split(',');
            foreach (string id in ids)
                if (int.TryParse(id, out int i)) ownedHats.Add(i);
        }
        equippedHat = PlayerPrefs.GetInt(EquippedKey, -1);
    }

    private void SaveData()
    {
        List<string> ids = new List<string>();
        foreach (int i in ownedHats) ids.Add(i.ToString());
        PlayerPrefs.SetString(OwnedKey, string.Join(",", ids));
        PlayerPrefs.SetInt(EquippedKey, equippedHat);
        PlayerPrefs.Save();
    }

    public bool IsOwned(int hatId) => ownedHats.Contains(hatId);
    public bool IsOwned(HatData hat) => ownedHats.Contains(GetHatId(hat));

    public void AddOwned(int hatId)
    {
        if (!ownedHats.Contains(hatId))
        {
            ownedHats.Add(hatId);
            SaveData();
            OnOwnedChanged?.Invoke(hatId);
        }
    }

    public void Equip(int hatId)
    {
        if (IsOwned(hatId))
        {
            equippedHat = hatId;
            SaveData();
        }
    }

    public int GetEquippedHat() => equippedHat;

    public int GetHatId(HatData hat)
    {
        for (int i = 0; i < allHats.Length; i++)
            if (allHats[i] == hat) return i;
        return -1;
    }

    public HatData GetHatData(int id)
    {
        if (id >= 0 && id < allHats.Length) return allHats[id];
        return null;
    }
}