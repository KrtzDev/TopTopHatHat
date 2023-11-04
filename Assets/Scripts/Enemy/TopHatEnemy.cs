using UnityEngine;

public class TopHatEnemy : MonoBehaviour
{
	private EnemyState _currentState;

	[field: SerializeField]
	public EnemyMoveTowardsPlayer EnemyMoveTowardsPlayer {  get; private set; }

	private void Awake()
	{
		EnemyMoveTowardsPlayer.InitState(this);
		_currentState = EnemyMoveTowardsPlayer;
	}

	private void FixedUpdate()
	{
		_currentState?.OnUpdate();
	}
}
