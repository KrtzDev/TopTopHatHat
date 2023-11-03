using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopHatCharacter : MonoBehaviour
{
	public Vector2 MoveInput { get; private set; }

	[SerializeField]
	private Dash _dash;
	[SerializeField]
	private Movement _movement;

	private CharacterState _currentState;

	private TopHatInput _topHatInput;

	private void Awake()
	{
		_topHatInput = GameManager.instance.TopHatInput;

		_dash.InitState(this);
		_movement.InitState(this);

		_currentState = _movement;
	}

	private void OnEnable()
	{
		_topHatInput.Enable();

		_topHatInput.Character.Move.performed += MovePerformed;
		_topHatInput.Character.Move.canceled += MoveCanceled;

		_topHatInput.Character.Dash.performed += DashInput;
	}


	private void OnDisable()
	{
		_topHatInput.Character.Move.performed -= MovePerformed;
		_topHatInput.Character.Move.canceled -= MoveCanceled;

		_topHatInput.Character.Dash.performed += DashInput;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		MoveInput = context.ReadValue<Vector2>();
		TransitionToState(_movement);
	}

	private void MoveCanceled(InputAction.CallbackContext context)
	{
		MoveInput = Vector2.zero;
	}

	private void DashInput(InputAction.CallbackContext context)
	{
		TransitionToState(_dash);
	}

	private void TransitionToState(CharacterState characterState)
	{
		_currentState.OnExit();
		_currentState = characterState;
		_currentState.OnEnter();
	}

	private void FixedUpdate()
	{
		_currentState?.OnUpdate();
	}
}
