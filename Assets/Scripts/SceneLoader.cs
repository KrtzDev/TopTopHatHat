using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
	public event Action OnSceneLoadCompleted;

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName).completed += SceneLoadCompleted;
	}

	private void SceneLoadCompleted(AsyncOperation operation)
	{
		OnSceneLoadCompleted();
	}
}
