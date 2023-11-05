using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
	public event Action OnSceneLoadCompleted;
	public string currentScene;

	public string SceneToLoad { get; set; }

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName).completed += SceneLoadCompleted;
		currentScene = sceneName;
	}

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex).completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperation operation)
	{
		OnSceneLoadCompleted();
	}
}
