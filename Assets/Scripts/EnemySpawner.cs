using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
	public Transform _enemyParent;

	[SerializeField]
	private List<Actor> _enemiesToSpawn = new List<Actor>();
	[SerializeField]
	private List<Transform> _spawnPositions = new List<Transform>();
	[SerializeField]
	private float _spawnCooldown;

	private int NumberOfEnemiesToSpawn => _spawnPositions.Count;
	private float _currentSpawnCooldown;


	private void Awake()
	{
		SpawnWave();
		_currentSpawnCooldown = _spawnCooldown;
	}

	private void Update()
	{
		_currentSpawnCooldown -= Time.deltaTime;
		if(_currentSpawnCooldown <= 0)
		{
			SpawnWave();
			_currentSpawnCooldown = _spawnCooldown;
		}
	}

	private void SpawnWave()
	{
		List<Actor> actorsToRemove = new List<Actor>();
		for (int i = 0; i < Mathf.Min(NumberOfEnemiesToSpawn, _enemiesToSpawn.Count); i++)
		{
			TopHatEnemy enemy = _enemiesToSpawn[i].GetComponent<TopHatEnemy>();

			if(GiveAbilities.instance.moveSpeed0)
            {
				enemy.GetComponent<NavMeshAgent>().speed += 0.7f;
            }

			if (GiveAbilities.instance.moveSpeed1)
			{
				enemy.GetComponent<NavMeshAgent>().speed += 0.7f;
			}

			if (GiveAbilities.instance.moveSpeed2)
			{
				enemy.GetComponent<NavMeshAgent>().speed += 1.5f;
			}

			if(GiveAbilities.instance.extraHat1)
            {
				enemy.GetComponent<Health>().IncreaseMaxHealth(1, true);
            }

			if (GiveAbilities.instance.extraHat2)
			{
				enemy.GetComponent<Health>().IncreaseMaxHealth(1, true);
			}

			if(GiveAbilities.instance.noDamageFor5)
            {
				enemy.GetComponent<Health>().GetNoDamageForTime(5);
            }

			Instantiate(enemy, _spawnPositions[i].position, Quaternion.identity, _enemyParent);
			actorsToRemove.Add(_enemiesToSpawn[i]);
		}
		foreach (var actor in actorsToRemove)
		{
			_enemiesToSpawn.Remove(actor);
		}
	}

	public void AddRandomEnemyToSpawn()
    {
		_enemiesToSpawn.Add(StatsTracker.instance.enemyList[Random.Range(0, StatsTracker.instance.enemyList.Count)]);
    }
}
