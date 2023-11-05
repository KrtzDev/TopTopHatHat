using System;

using UnityEngine;

public class SlimeEnemyAnimEvents : MonoBehaviour
{
	public event Action OnAttackFinishedAnimEvent;
	public event Action OnActivateDamageZoneAnimEvent;
	public event Action OnDeactivateDamageZoneAnimEvent;

	public void AttackFinished()
	{
		OnAttackFinishedAnimEvent();
	}

	public void ActivateDamageZone()
	{
		OnActivateDamageZoneAnimEvent();
	}

	public void DeactivateDamageZone()
	{
		OnDeactivateDamageZoneAnimEvent();
	}
}
