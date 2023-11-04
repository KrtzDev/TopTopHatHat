using UnityEngine;
using UnityEngine.TextCore.Text;

public class Movement : CharacterState
{
	const float SPEED_MULTIPLIER = 100;

	[SerializeField]
	private float _currentMoveSpeed;
	private float _baseMoveSpeed;
	private float _oldMoveSpeed;

	private Rigidbody _rigidbody;
	private Animator _animator;

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
		_baseMoveSpeed = _currentMoveSpeed;
		_oldMoveSpeed = _currentMoveSpeed;
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Move");
		_animator.SetBool("IsWalking", true);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Move");
		_rigidbody.velocity = Vector3.zero;
		_animator.SetBool("IsWalking", false);
	}

	public override void OnUpdate()
	{
		Vector3 movedirInput = new Vector3(_topHatCharacter.MoveInput.x, 0, _topHatCharacter.MoveInput.y);

		_rigidbody.velocity = _currentMoveSpeed * SPEED_MULTIPLIER * Time.fixedDeltaTime * movedirInput;

		if (movedirInput != Vector3.zero)
		{
			Vector3 lookdir = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
			_topHatCharacter.transform.rotation =
			   Quaternion.RotateTowards(_topHatCharacter.transform.rotation,
			   Quaternion.LookRotation(movedirInput, Vector3.up),
			   1200f * Time.deltaTime);
		}
	}
	
	public void IncreaseMoveSpeed(float amount)
    {
		_currentMoveSpeed += amount;
    }

	public void DecreaseMoveSpeed(float amount)
    {
		_currentMoveSpeed -= amount;
	}

	public void ResetMoveSpeed()
    {
		_currentMoveSpeed = _oldMoveSpeed;
    }

	public void SetMoveSpeed0()
    {
		_oldMoveSpeed = _currentMoveSpeed;
		_currentMoveSpeed = 0;
    }
}
