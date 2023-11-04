using UnityEngine;

public class Actor : MonoBehaviour
{
	public virtual void OnActorDeath()
	{
		Destroy(gameObject);
	}
}