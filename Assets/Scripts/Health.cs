using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _maxHealth;

	private int _currentHealth;

	private Actor _owningActor;

	private void Awake()
	{
		_owningActor = GetComponent<Actor>();

		_currentHealth = _maxHealth;
	}

	public void TakeDamage(int amount)
	{
		Debug.Log($"{transform.gameObject.name} was hit with {amount} Damage");

		_currentHealth -= amount;

		for(int i = 0; i < amount; i++)
        {
			if(gameObject.GetComponent<TopHatCharacter>() != null)
            {
				GameManager.instance.TopHatCharacter.AdditionalTopHats.RemoveTopHat(GameManager.instance.TopHatCharacter.AdditionalTopHats.GetLastTopHatPosition());
            }
			else if (gameObject.GetComponent<TopHatEnemy>() != null)
            {
				gameObject.GetComponent<AdditionalTopHats_Enemy>().RemoveTopHat(gameObject.GetComponent<AdditionalTopHats_Enemy>().GetLastTopHatPosition());
            }
        }

		if (_currentHealth <= 0)
			Death();
	}

	public void Heal(int amount)
	{
		_currentHealth += amount;
		_currentHealth = Mathf.Clamp(_currentHealth,0,_maxHealth);

		if (gameObject.GetComponent<TopHatCharacter>() != null)
		{
			GameManager.instance.TopHatCharacter.AdditionalTopHats.AddTopHat(GameManager.instance.TopHatCharacter.AdditionalTopHats.GetLastTopHatPosition() + 1);
		}
		else if (gameObject.GetComponent<TopHatEnemy>() != null)
		{
			gameObject.GetComponent<AdditionalTopHats_Enemy>().AddTopHat(gameObject.GetComponent<AdditionalTopHats_Enemy>().GetLastTopHatPosition() + 1);
		}
	}

	private void Death()
	{
		_owningActor.OnActorDeath();
	}

	public int ReturnCurrentHealth()
    {
		return _currentHealth;
    }
}
