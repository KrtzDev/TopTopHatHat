using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNameGenerator : MonoBehaviour
{
    [System.Serializable]
    public class WeaponIconByName
    {
        public string name;
        public Sprite icon;

        public WeaponIconByName(string _name, Sprite _icon)
        {
            name = _name;
            icon = _icon;
        }
    }

    private void Start()
    {
        CombinedNameAndIcon();
    }

    [SerializeField] private List<WeaponIconByName> _weaponIconsByNames = new();
    [SerializeField] private List<string> _weaponDescriptions = new();

    private WeaponIconByName RandomItemType()
    {
        return _weaponIconsByNames[Random.Range(0, _weaponIconsByNames.Count)];
    }

    private string RandomItemDescription() // Second Name
    {
        return _weaponDescriptions[Random.Range(0, _weaponDescriptions.Count)];
    }

    public WeaponIconByName CombinedNameAndIcon()
    {
        WeaponIconByName current = RandomItemType();
        current.name = current.name + " of " + RandomItemDescription();

        Debug.Log(current.name);

        return current;
    }
}
