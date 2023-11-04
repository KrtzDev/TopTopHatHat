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
		_navMeshAgent.SetDestination(GameManager.instance.TopHatCharacter.transform.position);
	}
}
