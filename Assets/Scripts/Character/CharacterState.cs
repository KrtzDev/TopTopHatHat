using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
	protected TopHatCharacter _topHatCharacter;

	public virtual void InitState(TopHatCharacter topHatCharacter)
	{
		_topHatCharacter = topHatCharacter; 
	}

	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
}