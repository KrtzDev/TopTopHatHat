using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
	public event Action OnAttackFinished;
	public string _sfxNameAttack;


	[field: SerializeField]
	public float AttackCooldown {  get; private set; }
	[field: SerializeField]
	public float AttackRange {  get; private set; }

	[SerializeField]
	private DamageZone _slimeAttackDamageZone;
	[SerializeField]
	private ParticleSystem _slimeAttackParticles;

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
		_animEvents.OnActivateDamageZoneAnimEvent += ActivateDamageZone;
		_animEvents.OnDeactivateDamageZoneAnimEvent += DeactivateDamageZone;

		_animator.SetTrigger("Attack");
	}

	public override void OnExit()
	{
		Debug.Log("Exit Attack");

		_navmeshAgent.isStopped = false;

		AttackCooldown = _attackCooldown;
		DeactivateDamageZone();
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

	private void ActivateDamageZone()
	{
		_slimeAttackDamageZone.gameObject.SetActive(true);
		_slimeAttackParticles.Play(true);
		SFXManager.instance.PlaySound(_sfxNameAttack);
	}

	private void DeactivateDamageZone()
	{
		_slimeAttackDamageZone.gameObject.SetActive(false);
		_slimeAttackParticles.Stop(true);
	}
}
