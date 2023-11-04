using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
	protected TopHatEnemy _topHatEnemy;

	public virtual void InitState(TopHatEnemy topHatEnemy)
	{
		_topHatEnemy = topHatEnemy;
	}
	public abstract void OnEnter();
	public abstract void OnExit();
	public abstract void OnUpdate();
}
