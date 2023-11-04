using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNameGenerator : Singleton<RandomNameGenerator>
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
        WeaponIconByName returnValue = new WeaponIconByName(current.name + " of " + RandomItemDescription(), current.icon);

        return returnValue;
    }
}
