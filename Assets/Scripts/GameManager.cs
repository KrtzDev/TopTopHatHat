using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

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

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(this);
		}
	}
}
