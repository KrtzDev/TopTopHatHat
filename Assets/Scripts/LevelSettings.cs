using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] Vector3 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GameManager.instance.TopHatCharacter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.TopHatCharacter.transform.position = _playerPos;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeLevel("prev");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeLevel("next");
        }
    }

    private void ChangeLevel(string _dir)
    {
        int _levelIndex = SceneManager.GetActiveScene().buildIndex;

        if (_dir == "prev" && _levelIndex > 1)
        {
            SceneLoader.instance.LoadScene(_levelIndex-1);
        }
        else if (_dir == "next" && _levelIndex < 10)
        {
            SceneLoader.instance.LoadScene(_levelIndex+1);
        }
    }
}
