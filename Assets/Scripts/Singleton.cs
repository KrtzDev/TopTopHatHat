﻿using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T instance;

	protected virtual void Awake()
	{
		if (!instance)
		{
			instance = this as T;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(this);
		}
	}
}