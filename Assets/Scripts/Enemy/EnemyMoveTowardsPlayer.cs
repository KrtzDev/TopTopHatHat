using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveTowardsPlayer : EnemyState
{
	private NavMeshAgent _navMeshAgent;

	public override void InitState(TopHatEnemy topHatEnemy)
	{
		base.InitState(topHatEnemy);
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	public override void OnEnter()
	{
		_navMeshAgent.isStopped = false;
	}

	public override void OnExit()
	{
		_navMeshAgent.isStopped = true;
	}

	public override void OnUpdate()
	{
		Vector3 dirToPlayer = (transform.position - GameManager.instance.TopHatCharacter.transform.position).normalized;

		_navMeshAgent.SetDestination(GameManager.instance.TopHatCharacter.transform.position - dirToPlayer * _navMeshAgent.stoppingDistance);
	}
}
