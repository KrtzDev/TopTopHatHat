using System;
using UnityEngine;

public class Attack : CharacterState
{
	public event Action OnAttackFinished;

	private Animator _animator;
	private CharacterAnimEvents _characterAnimEvents;

	private TopHatInput _topHatInput;

	private int _attackCounter = 0;
	private bool _canAttack;
	private bool _canIncreaseAttackCounter;

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);

		_animator = GetComponentInChildren<Animator>();
		_characterAnimEvents = GetComponentInChildren<CharacterAnimEvents>();

		_topHatInput = GameManager.instance.TopHatInput;

		_characterAnimEvents.OnAttackFinished += OnAttackAnimFinished;
		_characterAnimEvents.OnCanComboAttack += CanAttack;
		_characterAnimEvents.OnCanIncreaseComboCounter += CanIncreaseComboCounter;
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Attack");
		_topHatInput.Character.Attack.performed += Attack_performed;

		_animator.SetTrigger("Attack1");
		_attackCounter = 0;
	}

	private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		Debug.Log(_attackCounter);

		if (_canIncreaseAttackCounter && _attackCounter <= 2)
		{
			_canIncreaseAttackCounter = false;
			_attackCounter++;
		}
		if (_attackCounter > 2)
			_attackCounter = 0;
	}

	public override void OnExit()
	{
		Debug.Log("Exit Attack");
		_topHatInput.Character.Attack.performed -= Attack_performed;
		_attackCounter = 0;
	}

	public override void OnUpdate()
	{
		if (_attackCounter <= 2 && _attackCounter > 0 && _canAttack)
		{
			if (_attackCounter == 1)
				_animator.SetTrigger("Attack2");
			if (_attackCounter == 2)
				_animator.SetTrigger("Attack3");

			_canAttack = false;
		}
	}

	public void OnAttackAnimFinished()
	{
		_attackCounter = 0;
		OnAttackFinished();
	}

	public void CanAttack()
	{
		_canAttack = true;
	}

	private void CanIncreaseComboCounter()
	{
		_canIncreaseAttackCounter = true;
	}
}