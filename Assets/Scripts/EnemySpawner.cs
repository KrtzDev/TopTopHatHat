using System;
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

	private List<Actor> _aliveEnemies = new List<Actor>();

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

			Actor spawnedEnemy = Instantiate(enemy, _spawnPositions[i].position, Quaternion.identity, _enemyParent);
			if(GiveAbilities.instance.moveSpeed0)
            {
				spawnedEnemy.GetComponent<NavMeshAgent>().speed += 0.7f;
            }

			if (GiveAbilities.instance.moveSpeed1)
			{
				spawnedEnemy.GetComponent<NavMeshAgent>().speed += 0.7f;
			}

			if (GiveAbilities.instance.moveSpeed2)
			{
				spawnedEnemy.GetComponent<NavMeshAgent>().speed += 1.5f;
			}

			if(TakeAbilities.instance.decreaseEnemyMovementspeed)
            {
				spawnedEnemy.GetComponent<NavMeshAgent>().speed *= 0.675f;
            }

			if(GiveAbilities.instance.extraHat1)
            {
				spawnedEnemy.GetComponent<Health>().IncreaseMaxHealth(1, true);
            }

			if (GiveAbilities.instance.extraHat2)
			{
				spawnedEnemy.GetComponent<Health>().IncreaseMaxHealth(1, true);
			}

			if(GiveAbilities.instance.noDamageFor5)
            {
				spawnedEnemy.GetComponent<Health>().TakeNoDamageForTime(5);
            }

			actorsToRemove.Add(_enemiesToSpawn[i]);
			spawnedEnemy.OnActorDied += RemoveFromAlivelist;
			_aliveEnemies.Add(spawnedEnemy);
		}
		foreach (var actor in actorsToRemove)
		{
			_enemiesToSpawn.Remove(actor);
		}
	}

	public void AddRandomEnemyToSpawn()
    {
		_enemiesToSpawn.Add(StatsTracker.instance.enemyList[UnityEngine.Random.Range(0, StatsTracker.instance.enemyList.Count)]);
    }

	private void RemoveFromAlivelist(Actor enemyToRemove)
	{
		enemyToRemove.OnActorDied -= RemoveFromAlivelist;

		_aliveEnemies.Remove(enemyToRemove);
		if (_aliveEnemies.Count <= 0 && _enemiesToSpawn.Count <= 0)
			GameManager.instance.LevelWon.Invoke();
	}
}
