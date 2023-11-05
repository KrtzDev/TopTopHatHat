using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _levelName;
    [SerializeField] private GameObject _mainMenuButtons;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private Persistent _bootsTrapper; 

    private void Awake()
    {
        Destroy(FindAnyObjectByType<Persistent>().gameObject);

        Instantiate(_bootsTrapper);
    }

    public void StartGame()
    {
        SceneLoader.instance.LoadScene(_levelName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void EnableSettings()
    {
        _mainMenuButtons.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void DisableSettings()
    {
        _settingsMenu.SetActive(false);
        _mainMenuButtons.SetActive(true);
    }

    public void EnableControls()
    {
        _controlsMenu.SetActive(true);
        _mainMenuButtons.SetActive(false);
    }

    public void DisableControls()
    {
        _controlsMenu.SetActive(false);
        _mainMenuButtons.SetActive(true);
    }
}
