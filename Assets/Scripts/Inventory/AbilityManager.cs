using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : Singleton<AbilityManager>
{
    [SerializeField] private List<TaskAndGiveGenerator.TakeByID> _takeByIDs = new();
    [SerializeField] private List<TaskAndGiveGenerator.GiveByID> _giveByIDs = new();

    private void Start() // Load scene? Do each time a level is started.
    {
        
    }

    public void AddToTakeList(TaskAndGiveGenerator.TakeByID task)
    {
        _takeByIDs.Add(task);
    }

    public void AddToGiveList(TaskAndGiveGenerator.GiveByID task)
    {
        _giveByIDs.Add(task);
    }
}
