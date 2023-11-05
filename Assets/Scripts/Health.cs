using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _maxHealth;

	private int _currentHealth;

	private Actor _owningActor;

	private float _noDamageTimer;
	private bool _canGetDamage;

	private bool _firstDamageReceived;

	private void Awake()
	{
		_owningActor = GetComponent<Actor>();

		_currentHealth = _maxHealth;

		_canGetDamage = true;
		_firstDamageReceived = false;
	}

    private void Update()
    {
		if(_noDamageTimer > 0)
        {
			_noDamageTimer -= Time.deltaTime;
			_canGetDamage = false;
        }
		else
        {
			_canGetDamage = true;
		}
    }

    public void TakeDamage(int amount)
	{
		Debug.Log($"{transform.gameObject.name} was hit with {amount} Damage");

		if (!_canGetDamage)
			return;

		if (gameObject.GetComponent<TopHatCharacter>() != null)
		{
			if (GiveAbilities.instance.receiveDoubleDamageAt7HP)
			{
				amount *= 2;
			}

			if(GiveAbilities.instance.firstHitDealsDoubleDamage)
            {
				if(!_firstDamageReceived)
                {
					amount *= 2;
                }
            }
		}

		_currentHealth -= amount;
		_firstDamageReceived = true;

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
		if (gameObject.GetComponent<TopHatEnemy>() != null)
        {
			if(GiveAbilities.instance.noDamageFor2AfterEnemyDeath)
            {
				EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
				for(int i = 0; i < spawner._enemyParent.childCount; i++)
                {
					spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().GetNoDamageForTime(2);
                }
            }

			if(GiveAbilities.instance.noDamageFor1AfterEnemyDeath)
            {
				EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
				for (int i = 0; i < spawner._enemyParent.childCount; i++)
				{
					spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().GetNoDamageForTime(1);
				}
			}

			if(GiveAbilities.instance.noMoveFor0_5AfterEnemyDeath)
            {
				GameManager.instance.TopHatCharacter.Movement.SetMoveSpeed0();
                StartCoroutine(ResetMoveSpeedAfterTimePlayer(GameManager.instance.TopHatCharacter, 0.5f));
            }
        }

		_owningActor.OnActorDeath();
	}

	public int ReturnCurrentHealth()
    {
		return _currentHealth;
    }

	public void IncreaseMaxHealth(int value, bool heal)
    {
		_maxHealth += value;
		if (heal)
			_currentHealth = _maxHealth;
	}

	public void GetNoDamageForTime(float time)
    {
		if(_noDamageTimer <= 0)
        {
			_noDamageTimer = time;
		}
		else if (_noDamageTimer < time)
        {
			_noDamageTimer = time;
        }

		_canGetDamage = false;
	}

	public IEnumerator ResetMoveSpeedAfterTimePlayer(TopHatCharacter player, float time)
	{
		yield return new WaitForSeconds(time);

		player.Movement.ResetMoveSpeed();
	}
}
