using System.Collections;
using TMPro;
using Unity.VisualScripting;
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

	public void SetHealth(int value)
	{
		_currentHealth = value;
		if(_currentHealth > _maxHealth)
			_maxHealth = _currentHealth;
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

			if (_currentHealth - amount <= 1)
			{
				if (TakeAbilities.instance.mitigateFirstDamage && !StatsTracker.instance._playerHasMitigatedFirstDamage)
				{
					StatsTracker.instance._playerHasMitigatedFirstDamage = true;
					return;
				}

				if (TakeAbilities.instance.gain1TopHatsOn1HP)
				{
					if (!StatsTracker.instance._playerWasAlmostDead)
					{
						StatsTracker.instance._playerWasAlmostDead = true;
						Heal(1);
					}
				}
			}
		}

		if (gameObject.GetComponent<TopHatEnemy>() != null)
		{
			if (TakeAbilities.instance.killFirstEnemyEachStage)
			{
				if(StatsTracker.instance._killedEnemies == 0)
                {
					amount = 100;
                }
			}

			if (TakeAbilities.instance.killSecondEnemyEachStage)
			{
				if (StatsTracker.instance._killedEnemies == 1)
				{
					amount = 100;
				}
			}

			if (TakeAbilities.instance.killFifthEnemyEachStage)
			{
				if (StatsTracker.instance._killedEnemies == 4)
				{
					amount = 100;
				}
			}

			if (TakeAbilities.instance.killTenthEnemyEachStage)
			{
				if (StatsTracker.instance._killedEnemies == 9)
				{
					amount = 100;
				}
			}

			if(TakeAbilities.instance.dealDoubleDamageOnNextAttack)
            {
				if(StatsTracker.instance._playerDealDoubleDamage)
                {
					amount *= 2;
					StatsTracker.instance._playerDealDoubleDamage = false;
                }
            }
		}

		_currentHealth -= amount;
		_firstDamageReceived = true;

		if (gameObject.GetComponent<TopHatCharacter>() != null)
        {
			if(TakeAbilities.instance.takeNoDamageFor2AfterGettingDamaged)
            {
				TakeNoDamageForTime(2);
            }

			StatsTracker.instance.playerHP = _currentHealth;
        }

		if(gameObject.GetComponent<TopHatEnemy>() != null)
        {
			if(TakeAbilities.instance.takeNoDamageFor0_5AfterAttackingAnEnemy)
            {
				GameManager.instance.TopHatCharacter.GetComponent<Health>().TakeNoDamageForTime(0.5f);
            }
		}

		for (int i = 0; i < amount; i++)
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

		if (gameObject.GetComponent<TopHatCharacter>() != null)
		{
			GameManager.instance.TopHatCharacter.AdditionalTopHats.AddTopHat(GameManager.instance.TopHatCharacter.AdditionalTopHats.GetLastTopHatPosition() + 1);
			StatsTracker.instance.playerHP = _currentHealth;
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
			StatsTracker.instance._killedEnemies++;

			if (!StatsTracker.instance._playerHasFirstKill)
			{
				// stuff on first enemy kill each stage

				if(TakeAbilities.instance.gainTopHatOnFirstKill)
                {
					GameManager.instance.TopHatCharacter.GetComponent<Health>().Heal(1);
                }

				StatsTracker.instance._playerHasFirstKill = true;
			}

			if (GiveAbilities.instance.noDamageFor2AfterEnemyDeath)
            {
				EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
				for(int i = 0; i < spawner._enemyParent.childCount; i++)
                {
					spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().TakeNoDamageForTime(2);
                }
            }

			if(GiveAbilities.instance.noDamageFor1AfterEnemyDeath)
            {
				EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
				for (int i = 0; i < spawner._enemyParent.childCount; i++)
				{
					spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().TakeNoDamageForTime(1);
				}
			}

			if(GiveAbilities.instance.noMoveFor0_5AfterEnemyDeath)
            {
				GameManager.instance.TopHatCharacter.Movement.SetMoveSpeed0();
                StartCoroutine(ResetMoveSpeedAfterTimePlayer(GameManager.instance.TopHatCharacter, 0.5f));
            }

			if(TakeAbilities.instance.takeNoDamageFor2AfterKillingAnEnemy)
            {
				GameManager.instance.TopHatCharacter.GetComponent<Health>().TakeNoDamageForTime(2);

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

	public void TakeNoDamageForTime(float time)
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
