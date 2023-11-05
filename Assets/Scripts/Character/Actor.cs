using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
	public event Action<Actor> OnActorDied;

	public virtual void OnActorDeath()
	{
		OnActorDied.Invoke(this);
		Destroy(gameObject);
	}
}