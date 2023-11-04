using UnityEngine;

public class Movement : CharacterState
{
	const float SPEED_MULTIPLIER = 100;

	[SerializeField]
	private float _moveSpeed;

	private Rigidbody _rigidbody; 

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_rigidbody = GetComponent<Rigidbody>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Move");
	}

	public override void OnExit()
	{
		Debug.Log("Exit Move");
		_rigidbody.velocity = Vector3.zero;
	}

	public override void OnUpdate()
	{
		Vector3 movedirInput = new Vector3(_topHatCharacter.MoveInput.x, 0, _topHatCharacter.MoveInput.y);

		_rigidbody.velocity = _moveSpeed * SPEED_MULTIPLIER * Time.fixedDeltaTime * movedirInput;
	}
}
