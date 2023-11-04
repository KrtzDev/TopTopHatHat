using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAndGiveGenerator : Singleton<TaskAndGiveGenerator>
{
    [System.Serializable]
    public class TakeByID
    {
        public string text;
        public int id;

        public TakeByID(string _text, int _id)
        {
            text = _text;
            id = _id;
        }
    }

    [System.Serializable]
    public class GiveByID
    {
        public string text;
        public int id;

        public GiveByID(string _text, int _id)
        {
            text = _text;
            id = _id;
        }
    }

    [SerializeField] private List<TakeByID> _takeByIDs = new();
    [SerializeField] private List<GiveByID> _giveByIDs = new();

    public TakeByID GetRandomTakeTask()
    {
        int random = Random.Range(0, _takeByIDs.Count);
        TakeByID current = _takeByIDs[random];
        _takeByIDs.RemoveAt(random);

        return current;
    }

    public GiveByID GetRandomGiveTask()
    {
        int random = Random.Range(0, _takeByIDs.Count);
        GiveByID current = _giveByIDs[random];
        _giveByIDs.RemoveAt(random);

        return current;
    }
}
