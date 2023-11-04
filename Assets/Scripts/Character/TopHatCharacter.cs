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

	private CharacterState _currentState;

	private TopHatInput _topHatInput;
	private bool _isDashing;
	private bool _isDashOnCooldown;

	private bool CanDash => !_isDashing && !_isDashOnCooldown;

	private void Awake()
	{
		_topHatInput = GameManager.instance.TopHatInput;

		Dash.InitState(this);
		Movement.InitState(this);

		_currentState = Movement;
	}

	private void OnEnable()
	{
		Dash.OnDashFinished += DashFinished;

		_topHatInput.Enable();

		_topHatInput.Character.Move.performed += MovePerformed;
		_topHatInput.Character.Move.canceled += MoveCanceled;

		_topHatInput.Character.Dash.performed += DashInput;
	}


	private void OnDisable()
	{
		Dash.OnDashFinished -= DashFinished;

		_topHatInput.Character.Move.performed -= MovePerformed;
		_topHatInput.Character.Move.canceled -= MoveCanceled;

		_topHatInput.Character.Dash.performed -= DashInput;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		MoveInput = context.ReadValue<Vector2>();
	}

	private void MoveCanceled(InputAction.CallbackContext context)
	{
		MoveInput = Vector2.zero;
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
		TransitionToState(Movement);
		StopCoroutine(DashCooldown());
		StartCoroutine(DashCooldown());
	}

	private IEnumerator DashCooldown()
	{
		yield return new WaitForSeconds(Dash.DashCoolDownTime);
		_isDashOnCooldown = false;
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
