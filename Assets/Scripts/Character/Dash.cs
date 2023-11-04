using System;
using UnityEngine;

public class Dash : CharacterState
{
	public event Action OnDashFinished;

	private Rigidbody _rigidbody;
	private Animator _animator;

	[SerializeField]
	private float _dashDuration;
	[SerializeField]
	private float _dashForce;

	private float _currentDashTime;

	[field: SerializeField]
	public float DashCoolDownTime { get; private set; }

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Dash");

		_currentDashTime = _dashDuration;

		Vector3 movedir = new Vector3(_topHatCharacter.MoveInput.x, 0, _topHatCharacter.MoveInput.y);
		_rigidbody.AddForce(movedir * _dashForce, ForceMode.Impulse);

		_animator.SetBool("IsDashing", true);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Dash");
		_rigidbody.velocity = Vector3.zero;

		_animator.SetBool("IsDashing", false);
	}

	public override void OnUpdate()
	{
		_currentDashTime -= Time.deltaTime;
		if(_currentDashTime <= 0)
		{
			_currentDashTime = _dashDuration;
			OnDashFinished();
		}
	}
}
