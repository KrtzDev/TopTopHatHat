using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
	protected TopHatCharacter topHatCharacter;

	public virtual void InitState(TopHatCharacter topHatCharacter)
	{
		this.topHatCharacter = topHatCharacter; 
	}

	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
}