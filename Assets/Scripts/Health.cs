using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _maxHealth;

	private int _currentHealth;

	private void Awake()
	{
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(int amount)
	{
		Debug.Log($"{transform.gameObject.name} was hit with {amount} Damage");

		_currentHealth -= amount;
		if (_currentHealth <= 0)
			Death();
	}

	public void Heal(int amount)
	{
		_currentHealth += amount;
		_currentHealth = Mathf.Clamp(_currentHealth,0,_maxHealth);
	}

	private void Death()
	{
		Debug.Log("Died");
		Destroy(gameObject);
	}
}
