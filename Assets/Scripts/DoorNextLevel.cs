using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{
    [SerializeField] private string _nextLevel;

	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();

		GameManager.instance.LevelWon += OpenDoorToNextLevel;
	}


	private void OnDisable()
	{
		GameManager.instance.LevelWon -= OpenDoorToNextLevel;
	}

	private void OpenDoorToNextLevel()
	{
		_animator.SetTrigger("Exit_Open");
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TopHatCharacter>() != null)
        {
			SceneLoader.instance.SceneToLoad = _nextLevel;
			GameManager.instance.OpenGiveAndTakeUI();
        }
    }
}
