using UnityEngine;
using UnityEngine.SceneManagement;

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

	protected void Start()
	{
		if (SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("Test"))
		{
			TopHatCharacter = FindObjectOfType<TopHatCharacter>();
		}

		SceneLoader.instance.OnSceneLoadCompleted += OnSceneLoaded;
	}

	private void OnSceneLoaded()
	{
        if (SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("Test"))
		{
			TopHatCharacter = FindObjectOfType<TopHatCharacter>();
        }

	}
}
