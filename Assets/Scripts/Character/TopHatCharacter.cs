using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopHatCharacter : Actor
{
	public Vector2 MoveInput { get; private set; }

	[field: SerializeField]
	public Dash Dash {  get; private set; }
	[field: SerializeField]
	public Movement Movement {  get; private set; }
	[field: SerializeField]
	public Idle Idle { get; private set; }
	[field: SerializeField]
	public Attack Attack { get; private set; }
	[field: SerializeField]
	public Death Death { get; private set; }

	private CharacterState _currentState;
	private Health _health;

	private TopHatInput _topHatInput;
	private bool _isDashing;
	private bool _isDashOnCooldown;

	public bool canTakeDamage = true;
	public bool canDoDamage = true;

	public AdditionalTopHats_Player AdditionalTopHats { get; private set; }

	private bool CanDash => !_isDashing && !_isDashOnCooldown;

	private void Awake()
	{
		_topHatInput = GameManager.instance.TopHatInput;
		_health = GetComponent<Health>();

		_health.SetHealth(StatsTracker.instance.playerHP);

		this.AdditionalTopHats = gameObject.GetComponentInChildren<AdditionalTopHats_Player>();

		Idle.InitState(this);
		Movement.InitState(this);
		Dash.InitState(this);
		Attack.InitState(this);
		Death.InitState(this);

		_currentState = Idle;

		if(TakeAbilities.instance.gainTopHatOnStage1)
        {
			_health.IncreaseMaxHealth(1, false);
			_health.Heal(1);
        }

		if (TakeAbilities.instance.gainTopHatOnStage2)
		{
			_health.IncreaseMaxHealth(1, false);
			_health.Heal(1);
		}
	}

	private void OnEnable()
	{
		Dash.OnDashFinished += DashFinished;
		Attack.OnAttackFinished += AttackFinished;

		_topHatInput.Enable();

		_topHatInput.Character.Move.performed += MovePerformed;
		_topHatInput.Character.Move.canceled += MoveCanceled;

		_topHatInput.Character.Dash.performed += DashInput;

		_topHatInput.Character.Attack.performed += AttackInput;

		_topHatInput.Character.Pause.performed += PauseInput;
	}


	private void OnDisable()
	{
		Dash.OnDashFinished -= DashFinished;
		Attack.OnAttackFinished -= AttackFinished;

		_topHatInput.Character.Move.performed -= MovePerformed;
		_topHatInput.Character.Move.canceled -= MoveCanceled;

		_topHatInput.Character.Dash.performed -= DashInput;

		_topHatInput.Character.Attack.performed -= AttackInput;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		MoveInput = context.ReadValue<Vector2>();
		if(_currentState != Dash && _currentState != Attack)
			TransitionToState(Movement);

		if (MoveInput == Vector2.zero && _currentState != Dash)
			TransitionToState(Idle);
	}

	private void MoveCanceled(InputAction.CallbackContext context)
	{
		MoveInput = Vector2.zero;
		if(_currentState != Dash)
			TransitionToState(Idle);
	}

	private void DashInput(InputAction.CallbackContext context)
	{
		if (!CanDash)
			return;

		_isDashOnCooldown = true;
		_isDashing = true;
		TransitionToState(Dash);
	}

	private void DashFinished()
	{
		_isDashing = false;
		StopCoroutine(DashCooldown());
		StartCoroutine(DashCooldown());

		if(MoveInput != Vector2.zero)
			TransitionToState(Movement);
		else 
			TransitionToState(Idle);
	}

	private IEnumerator DashCooldown()
	{
		yield return new WaitForSeconds(Dash.DashCoolDownTime);
		_isDashOnCooldown = false;
	}

	private void AttackInput(InputAction.CallbackContext context)
	{
		if(_currentState != Dash && _currentState != Attack)
		TransitionToState(Attack);
	}

	private void AttackFinished()
	{
		if (MoveInput != Vector2.zero)
			TransitionToState(Movement);
		else
			TransitionToState(Idle);
	}

	private void PauseInput(InputAction.CallbackContext context)
    {
		Time.timeScale = 0;
		GameManager.instance.PauseMenu.gameObject.SetActive(true);
    }

	public void TransitionToState(CharacterState characterState)
	{
		if (_currentState == characterState || _currentState == Death)
			return;

		_currentState.OnExit();
		_currentState = characterState;
		_currentState.OnEnter();
	}

	private void FixedUpdate()
	{
		_currentState?.OnUpdate();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out DamageZone damageComponent))
		{
			if (!canTakeDamage)
				return;

			_health.TakeDamage(damageComponent.DamageAmount);
		}
	}

	public override void OnActorDeath()
	{
		TransitionToState(Death);
	}

	public int GetCurrentHealth()
    {
		return _health.ReturnCurrentHealth();
    }
}
