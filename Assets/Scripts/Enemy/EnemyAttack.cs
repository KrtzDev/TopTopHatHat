using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
	public event Action OnAttackFinished;

	[field: SerializeField]
	public float AttackCooldown {  get; private set; }
	[field: SerializeField]
	public float AttackRange {  get; private set; }

	[SerializeField]
	private DamageZone _slimeAttackDamageZone;

	private NavMeshAgent _navmeshAgent;
	private Animator _animator;
	private SlimeEnemyAnimEvents _animEvents;

	private float _attackCooldown;

	public override void InitState(TopHatEnemy topHatEnemy)
	{
		base.InitState(topHatEnemy);

		_navmeshAgent = GetComponent<NavMeshAgent>();
		_animator = GetComponentInChildren<Animator>();
		_animEvents = GetComponentInChildren<SlimeEnemyAnimEvents>();

		_attackCooldown = AttackCooldown;
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Attack");

		_navmeshAgent.isStopped = true;

		_animEvents.OnAttackFinishedAnimEvent += AttackFinished;
		_animator.SetTrigger("Attack");

		_slimeAttackDamageZone.gameObject.SetActive(true);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Attack");

		_navmeshAgent.isStopped = false;

		AttackCooldown = _attackCooldown;

		_slimeAttackDamageZone.gameObject.SetActive(false);
	}

	public override void OnUpdate()
	{
		
	}

	public void AttackFinished()
	{
		OnAttackFinished();
	}
	public void TickCooldown(float deltaTime)
	{
		AttackCooldown -= deltaTime;
	}
}
