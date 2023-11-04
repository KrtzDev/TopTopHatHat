using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopHatCharacter : MonoBehaviour
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

	private CharacterState _currentState;

	private TopHatInput _topHatInput;
	private bool _isDashing;
	private bool _isDashOnCooldown;

	private bool CanDash => !_isDashing && !_isDashOnCooldown;

	private void Awake()
	{
		_topHatInput = GameManager.instance.TopHatInput;

		Idle.InitState(this);
		Movement.InitState(this);
		Dash.InitState(this);
		Attack.InitState(this);

		_currentState = Idle;
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

	public void TransitionToState(CharacterState characterState)
	{
		if (_currentState == characterState)
			return;

		_currentState.OnExit();
		_currentState = characterState;
		_currentState.OnEnter();
	}

	private void FixedUpdate()
	{
		_currentState?.OnUpdate();
	}
}
