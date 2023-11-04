using UnityEngine;

public class Death : CharacterState
{
	private Animator _animator;

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_animator = GetComponentInChildren<Animator>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Death");

		GameManager.instance.TopHatInput.Character.Disable();
		_animator.SetBool("IsDead", true);
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
		
	}
}