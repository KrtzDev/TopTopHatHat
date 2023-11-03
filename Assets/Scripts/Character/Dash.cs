using System;
using UnityEngine;

public class Dash : CharacterState
{
	public event Action OnDashFinished;

	private Rigidbody _rigidbody;

	[SerializeField]
	private float _dashDuration;
	[SerializeField]
	private float _dashForce;

	private float _currentDashTime;

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_rigidbody = GetComponent<Rigidbody>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Dash");

		_currentDashTime = _dashDuration;

		Vector3 movedir = new Vector3(_topHatCharacter.MoveInput.x, 0, _topHatCharacter.MoveInput.y);
		_rigidbody.AddForce(movedir * _dashForce, ForceMode.Impulse);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Dash");
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
