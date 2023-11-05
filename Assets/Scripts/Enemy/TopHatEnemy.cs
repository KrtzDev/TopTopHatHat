using System;
using UnityEngine;

public class TopHatEnemy : Actor
{
	public string _sfxNameAttack;
	public string _sfxNameGetHit;
	public string _sfxNameDeath;

	private EnemyState _currentState;

	[field: SerializeField]
	public EnemyMoveTowardsPlayer EnemyMoveTowardsPlayer {  get; private set; }
	[field: SerializeField]
	public EnemyAttack EnemyAttack { get; private set; }

	private Health _health;
	private bool _isInAttackRange;
	private bool _hasAttackCooldown;
	public bool CanAttack => _isInAttackRange && !_hasAttackCooldown;

	private void Awake()
	{
		_health = GetComponent<Health>();

		EnemyMoveTowardsPlayer.InitState(this);
		EnemyAttack.InitState(this);
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
		float distanceToPlayer = (GameManager.instance.TopHatCharacter.transform.position - transform.position).magnitude;

		_isInAttackRange = distanceToPlayer <= EnemyAttack.AttackRange;
		EnemyAttack.TickCooldown(Time.deltaTime);
		_hasAttackCooldown = EnemyAttack.AttackCooldown >= 0;

		if(CanAttack)
        {
			SFXManager.instance.PlaySound(_sfxNameAttack);
			TransitionToState(EnemyAttack);
		}
	}

	private void TransitionToState(EnemyState enemyState)
	{
		if (_currentState == enemyState)
			return;

		_currentState.OnExit();
		_currentState = enemyState;
		_currentState.OnEnter();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out TopHatEnemy topHatEnemy))
			return;

		if (other.TryGetComponent(out DamageZone damageComponent))
		{
			_health.TakeDamage(damageComponent.DamageAmount);
		}
	}

	public override void OnActorDeath()
	{
		base.OnActorDeath();
	}
}
