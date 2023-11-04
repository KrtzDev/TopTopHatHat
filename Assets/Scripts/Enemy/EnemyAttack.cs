using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
	public event Action OnAttackFinished;

	private NavMeshAgent _navmeshAgent;

	public override void InitState(TopHatEnemy topHatEnemy)
	{
		base.InitState(topHatEnemy);

		_navmeshAgent = GetComponent<NavMeshAgent>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Attack");
	}

	public override void OnExit()
	{
		Debug.Log("Exit Attack");
	}

	public override void OnUpdate()
	{
		
	}

	public void AttackFinished()
	{
		OnAttackFinished();
	}
}
