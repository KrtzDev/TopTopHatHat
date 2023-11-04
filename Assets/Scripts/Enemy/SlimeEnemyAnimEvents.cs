using System;

using UnityEngine;

public class SlimeEnemyAnimEvents : MonoBehaviour
{
	public event Action OnAttackFinishedAnimEvent;

 public void AttackFinished()
	{
		OnAttackFinishedAnimEvent();
	}
}
