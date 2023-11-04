using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public TopHatInput TopHatInput 
	{ 
		get 
		{
			if (_topHatInput == null)
				_topHatInput = new TopHatInput();
			return _topHatInput; 
		} 
	}

	[SerializeField]
	private TopHatInput _topHatInput;

	[field: SerializeField]
	public TopHatCharacter TopHatCharacter { get; private set; }
}
