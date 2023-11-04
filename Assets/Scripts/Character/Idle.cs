using UnityEngine;

public class Idle : CharacterState
{
	private Animator _animator;

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);

		_animator = GetComponentInChildren<Animator>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Idle");
		_animator.SetBool("IsIdle", true);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Idle");
		_animator.SetBool("IsIdle", false);
	}

	public override void OnUpdate()
	{

	}
}