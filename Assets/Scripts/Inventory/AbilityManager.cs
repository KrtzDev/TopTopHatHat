using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : Singleton<AbilityManager>
{
	[SerializeField] private List<TaskAndGiveGenerator.TakeByID> _takeByIDs = new();
	[SerializeField] private List<TaskAndGiveGenerator.GiveByID> _giveByIDs = new();

	private List<int> _takeIDs = new();
	private List<int> _giveIDs = new();

	[SerializeField] private List<bool> _takeTaskBoolList = new();
	[SerializeField] private List<bool> _giveTaskBoolList = new();

	protected override void Awake()
	{
		base.Awake();

		SceneLoader.instance.OnSceneLoadCompleted += OnSceneLoaded;
	}

	private void OnSceneLoaded()
	{
		if (SceneLoader.instance.currentScene == "SCENE_Main_Menu")
			return;

		SetupTakeAndGiveLists();
	}

	private void SetupTakeAndGiveLists() // Load scene! Do each time a level is started.
	{
		ConvertToIDList(_takeByIDs);
		ConvertToIDList(_giveByIDs);

		_takeTaskBoolList = new();
		_giveTaskBoolList = new();

		for (int i = 0; i < 20; i++)
		{

			_takeTaskBoolList.Add(false);
		}

		for (int i = 0; i < 24; i++)
		{
			_giveTaskBoolList.Add(false);
		}

		//for (int i = 0; i < 20; i++)
		//{
		//	if (ListContainsIndex(_takeIDs, _takeByIDs[i].id))
		//		_takeTaskBoolList[_takeByIDs[i].id] = true;
		//}

		for (int i = 0; i < _takeTaskBoolList.Count; i++)
		{
			for (int j = 0; j < _takeByIDs.Count; j++)
			{
				if (i == _takeByIDs[j].id)
					_takeTaskBoolList[i] = true;
			}
		}

		//for (int i = 0; i < 24; i++)
		//{
		//	if (ListContainsIndex(_giveIDs, _giveByIDs[i].id))
		//		_giveTaskBoolList[_giveByIDs[i].id] = true;
		//}

		for (int i = 0; i < _giveTaskBoolList.Count; i++)
		{
			for (int j = 0; j < _giveByIDs.Count; j++)
			{
				if (i == _giveByIDs[j].id)
					_giveTaskBoolList[i] = true;
			}
		}

		if (_takeByIDs.Count > 0)
			ActivateTakeAbilities();
		if (_giveByIDs.Count > 0)
			ActivateGiveAbilities();
	}

	public void ActivateTakeAbilities()
	{
		int i = 0;

		if (_takeTaskBoolList[i]) // 0
		{
			TakeAbilities.instance.Ability0();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability1();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability2();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability3();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability4();
		}

		i++;

		if (_takeTaskBoolList[i]) // 5
		{
			TakeAbilities.instance.Ability5();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability6();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability7();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability8();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability9();
		}

		i++;

		if (_takeTaskBoolList[i]) // 10
		{
			TakeAbilities.instance.Ability10();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability11();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability12();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability13();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability14();
		}

		i++;

		if (_takeTaskBoolList[i]) // 15
		{
			TakeAbilities.instance.Ability15();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability16();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability17();
		}

		i++;

		if (_takeTaskBoolList[i])
		{
			TakeAbilities.instance.Ability18();
		}

		i++;

		if (_takeTaskBoolList[i]) // 19
		{
			TakeAbilities.instance.Ability19();
		}
	}

	public void ActivateGiveAbilities()
	{
		int i = 0;

		if (_giveTaskBoolList[i]) // 0
		{
			GiveAbilities.instance.Ability0();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability1();
		}

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability2();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability3();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability4();
		}

		i++;

		if (_giveTaskBoolList[i]) // 5
		{
			GiveAbilities.instance.Ability5();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability6();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability7();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability8();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability9();
		}

		i++;

		if (_giveTaskBoolList[i]) // 10
		{
			GiveAbilities.instance.Ability10();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability11();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability12();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability13();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability14();
		}

		i++;

		if (_giveTaskBoolList[i]) // 15
		{
			GiveAbilities.instance.Ability15();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability16();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability17();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability18();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability19();
		}

		i++;

		if (_giveTaskBoolList[i]) // 20
		{
			GiveAbilities.instance.Ability20();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability21();
		}

		i++;

		if (_giveTaskBoolList[i])
		{
			GiveAbilities.instance.Ability22();
		}

		i++;

		if (_giveTaskBoolList[i]) // 23
		{
			GiveAbilities.instance.Ability23();
		}
	}

	public void AddToTakeList(TaskAndGiveGenerator.TakeByID task)
	{
		_takeByIDs.Add(task);
	}

	public void AddToGiveList(TaskAndGiveGenerator.GiveByID task)
	{
		_giveByIDs.Add(task);
	}

	private void ConvertToIDList(List<TaskAndGiveGenerator.TakeByID> takeByIDs)
	{
		_takeIDs = new();

		for (int i = 0; i < takeByIDs.Count; i++)
		{
			_takeIDs.Add(_takeByIDs[i].id);
		}
	}

	private void ConvertToIDList(List<TaskAndGiveGenerator.GiveByID> giveByIDs)
	{
		_giveIDs = new();

		for (int i = 0; i < giveByIDs.Count; i++)
		{
			_giveIDs.Add(_giveByIDs[i].id);
		}
	}

	private bool ListContainsIndex(List<int> IDs, int index)
	{
		if (IDs.Contains(index))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
