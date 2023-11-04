using System;
using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
	public event Action OnAttackFinished;
	public event Action OnCanComboAttack;
	public event Action OnCanIncreaseComboCounter;

	public void OnAttackAnimFinished()
	{
		OnAttackFinished();
	}

	public void OnCanComboAttackAnim()
	{
		OnCanComboAttack();
	}

	public void CanIncreaseAttackComboCounter()
	{
		OnCanIncreaseComboCounter();
	}
}
