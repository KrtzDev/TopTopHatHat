using System;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : CharacterState
{
	public event Action OnAttackFinished;

	private Animator _animator;
	private CharacterAnimEvents _characterAnimEvents;

	private TopHatInput _topHatInput;

	[SerializeField]
	private TrailRenderer _weaponTrail;

	[SerializeField]
	private DamageZone _stabDamageZone;
	[SerializeField]
	private DamageZone _slashDamageZone;
	[SerializeField]
	private DamageZone _topSlashDamageZone;

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

		_characterAnimEvents.OnActivateWeaponTrail += ActivateWeaponTrail;
		_characterAnimEvents.OnDeactivateWeaponTrail += DeactivateWeaponTrail;
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Attack");
		_topHatInput.Character.Attack.performed += Attack_performed;

		_animator.SetTrigger("Attack1");
		ActivateDamageZone(_stabDamageZone);
		_attackCounter = 0;
		SFXManager.instance.PlaySound("PlayerAttack1");
	}

	private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		if (_canIncreaseAttackCounter && _attackCounter <= 2)
		{
			_canIncreaseAttackCounter = false;
			_attackCounter++;
		}
		if (_attackCounter > 2)
        {
			_attackCounter = 0;
        }
	}

	public override void OnExit()
	{
		Debug.Log("Exit Attack");
		_topHatInput.Character.Attack.performed -= Attack_performed;

		//_characterAnimEvents.OnAttackFinished -= OnAttackAnimFinished;
		//_characterAnimEvents.OnCanComboAttack -= CanAttack;
		//_characterAnimEvents.OnCanIncreaseComboCounter -= CanIncreaseComboCounter;

		//_characterAnimEvents.OnActivateWeaponTrail -= ActivateWeaponTrail;
		//_characterAnimEvents.OnDeactivateWeaponTrail -= ActivateWeaponTrail;

		if (GiveAbilities.instance.noDamageFor0_5AfterAttacking)
		{
			EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
			for (int i = 0; i < spawner._enemyParent.childCount; i++)
			{
				spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().TakeNoDamageForTime(0.5f);
			}
		}

		_attackCounter = 0;
		DeactivateDamageZones();
		DeactivateWeaponTrail();
	}

	public override void OnUpdate()
	{
		if (_attackCounter <= 2 && _attackCounter > 0 && _canAttack)
		{
			if (_attackCounter == 1)
			{
				_animator.SetTrigger("Attack2");
				SFXManager.instance.PlaySound("PlayerAttack2");
				ActivateDamageZone(_slashDamageZone);
			}
			if (_attackCounter == 2)
			{
				_animator.SetTrigger("Attack3");
				SFXManager.instance.PlaySound("PlayerAttack3");
				ActivateDamageZone(_topSlashDamageZone);
			}

			_canAttack = false;
		}
	}

	public void OnAttackAnimFinished()
	{
		_attackCounter = 0;
		OnAttackFinished();
		DeactivateDamageZones();
	}

	public void CanAttack()
	{
		_canAttack = true;
	}

	private void CanIncreaseComboCounter()
	{
		_canIncreaseAttackCounter = true;
	}

	private void DeactivateDamageZones()
	{
		_stabDamageZone.gameObject.SetActive(false);
		_slashDamageZone.gameObject.SetActive(false);
		_topSlashDamageZone.gameObject.SetActive(false);
	}

	private void ActivateDamageZone(DamageZone damageZone)
	{
		DeactivateDamageZones();

		damageZone.gameObject.SetActive(true);
	}

	private void ActivateWeaponTrail() => _weaponTrail.emitting = true;

	private void DeactivateWeaponTrail() => _weaponTrail.emitting = false;
}