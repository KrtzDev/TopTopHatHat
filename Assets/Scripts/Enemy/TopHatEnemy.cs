using UnityEngine;

public class TopHatEnemy : MonoBehaviour
{
	private EnemyState _currentState;

	[field: SerializeField]
	public EnemyMoveTowardsPlayer EnemyMoveTowardsPlayer {  get; private set; }
	[field: SerializeField]
	public EnemyAttack EnemyAttack { get; private set; }

	private bool _isInAttackRange;
	private bool _hasAttackCooldown;
	public bool CanAttack => _isInAttackRange && !_hasAttackCooldown;

	private void Awake()
	{
		EnemyMoveTowardsPlayer.InitState(this);
		_currentState = EnemyMoveTowardsPlayer;
	}

	private void OnEnable()
	{
		EnemyAttack.OnAttackFinished += EnemyAttack_OnAttackFinished;
	}

	private void EnemyAttack_OnAttackFinished()
	{
		TransitionToState(EnemyMoveTowardsPlayer);
	}

	private void FixedUpdate()
	{
		EvaluateStateChange();

		_currentState?.OnUpdate();
	}

	private void EvaluateStateChange()
	{
		if(CanAttack)
			TransitionToState(EnemyAttack);
	}

	private void TransitionToState(EnemyState enemyState)
	{
		if (_currentState == enemyState)
			return;

		_currentState.OnExit();
		_currentState = enemyState;
		_currentState.OnEnter();
	}
}
