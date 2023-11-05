using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsTracker : Singleton<StatsTracker>
{
    public List<Actor> enemyList;

	public bool _playerHasFirstKill = false;
	public bool _playerHasMitigatedFirstDamage = false;
	public bool _playerDealDoubleDamage = false;
	public int _killedEnemies = 0;
	public int _dashes = 0;

	protected void Start()
	{
		SceneLoader.instance.OnSceneLoadCompleted += OnSceneLoaded;
	}

	private void OnSceneLoaded()
	{
		if (SceneManager.GetActiveScene().name.Contains("Level"))
		{
			ResetStatsPerStage();
		}
	}

	private void ResetStatsPerStage()
    {
		_playerHasFirstKill = false;
		_playerHasMitigatedFirstDamage = false;
		_playerDealDoubleDamage = false;
		_killedEnemies = 0;
		_dashes = 0;
    }
}
