using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCards : MonoBehaviour
{
    [SerializeField] private List<CardSelection> _cards = new();

    private void Start()
    {
        if(_cards.Count <= TaskAndGiveGenerator.instance.TakeByIDsAmount() && _cards.Count <= TaskAndGiveGenerator.instance.GiveeByIDsAmount())
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].SetCardNameIcon(RandomNameGenerator.instance.CombinedNameAndIcon());
                _cards[i].SetTake(TaskAndGiveGenerator.instance.GetRandomTakeTask());
                _cards[i].SetGive(TaskAndGiveGenerator.instance.GetRandomGiveTask());
            }
        }
        else
        {
            Debug.LogError("NOT ENOUGH TAKE OR GIVE TASKS FOR AMOUNT OF CARDS");
        }
    }

}
