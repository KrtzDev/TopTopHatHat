using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopHatCharacter : MonoBehaviour
{
	const float SPEED_MULTIPLIER = 100;

	[SerializeField]
	private float _moveSpeed;

	private Rigidbody _rigidbody;

	private TopHatInput _topHatInput;

	private Vector2 _moveInput;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();

		_topHatInput = GameManager.instance.TopHatInput;
	}

	private void OnEnable()
	{
		_topHatInput.Enable();

		_topHatInput.Character.Move.performed += MoveInput;
		_topHatInput.Character.Move.canceled += MoveCanceled;
	}

	private void OnDisable()
	{
		_topHatInput.Character.Move.performed -= MoveInput;
		_topHatInput.Character.Move.canceled -= MoveCanceled;
	}

	private void MoveCanceled(InputAction.CallbackContext context)
	{
		_moveInput = Vector2.zero;
	}

	private void MoveInput(InputAction.CallbackContext context)
	{
		_moveInput = context.ReadValue<Vector2>();
	}

	private void FixedUpdate()
	{
		Vector3 movedir = new Vector3(_moveInput.x, 0 ,_moveInput.y);
		_rigidbody.velocity = _moveSpeed * SPEED_MULTIPLIER * Time.fixedDeltaTime * movedir;
	}
}
