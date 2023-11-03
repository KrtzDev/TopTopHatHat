using System;
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

	private void DashFinished()
	{
		TransitionToState(Movement);
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		MoveInput = context.ReadValue<Vector2>();
		TransitionToState(Movement);
	}

	private void MoveCanceled(InputAction.CallbackContext context)
	{
		MoveInput = Vector2.zero;
	}

	private void DashInput(InputAction.CallbackContext context)
	{
		TransitionToState(Dash);
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
