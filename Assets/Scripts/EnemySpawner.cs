using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private Transform _enemyParent;

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
			Instantiate(_enemiesToSpawn[i], _spawnPositions[i].position, Quaternion.identity, _enemyParent);
			actorsToRemove.Add(_enemiesToSpawn[i]);
		}
		foreach (var actor in actorsToRemove)
		{
			_enemiesToSpawn.Remove(actor);
		}
	}
}
