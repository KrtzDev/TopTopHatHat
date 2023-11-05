using System;
using System.Collections;
using UnityEngine;

public class Dash : CharacterState
{
	public event Action OnDashFinished;

	private Rigidbody _rigidbody;
	private Animator _animator;

	[SerializeField]
	private GameObject _closedUmbrella;
	[SerializeField]
	private GameObject _openedUmbrella;

	[SerializeField]
	private float _dashDuration;
	[SerializeField]
	private float _dashForce;

	private float _currentDashTime;

	[field: SerializeField]
	public float DashCoolDownTime { get; private set; }

	public override void InitState(TopHatCharacter topHatCharacter)
	{
		base.InitState(topHatCharacter);
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
	}

	public override void OnEnter()
	{
		Debug.Log("Enter Dash");

		_currentDashTime = _dashDuration;

		Vector3 movedir = new Vector3(_topHatCharacter.MoveInput.x, 0, _topHatCharacter.MoveInput.y);
		_rigidbody.AddForce(movedir * _dashForce, ForceMode.Impulse);

		_animator.SetBool("IsDashing", true);
		SFXManager.instance.PlaySound("PlayerDash");
		OpenUmbrella();
	}

	private void OpenUmbrella()
	{
		_closedUmbrella.SetActive(false);
		_openedUmbrella.SetActive(true);
	}

	public override void OnExit()
	{
		Debug.Log("Exit Dash");
		_rigidbody.velocity = Vector3.zero;

		_animator.SetBool("IsDashing", false);

		if(TakeAbilities.instance.moveSpeedOnDash)
        {
			FindObjectOfType<TopHatCharacter>().Movement.IncreaseMoveSpeed(1);
			StartCoroutine(ResetMoveSpeedAfterTime(1.5f));
        }

		if(TakeAbilities.instance.takeNoDamageFor0_5AfterDash)
        {
			GameManager.instance.TopHatCharacter.GetComponent<Health>().TakeNoDamageForTime(0.5f);
        }

		if(TakeAbilities.instance.dealDoubleDamageOnNextAttack)
        {
			StatsTracker.instance._playerDealDoubleDamage = true;
        }

		StatsTracker.instance._dashes++;

		if(TakeAbilities.instance.gainTopHatOn10Dashes)
        {
			if(StatsTracker.instance._dashes == 10)
            {
				GameManager.instance.TopHatCharacter.GetComponent<Health>().IncreaseMaxHealth(1, false);
				GameManager.instance.TopHatCharacter.GetComponent<Health>().Heal(1);
            }
        }


		if(GiveAbilities.instance.noDamageFor1AfterPlayDash)
        {
			EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
			for (int i = 0; i < spawner._enemyParent.childCount; i++)
			{
				spawner._enemyParent.GetChild(i).gameObject.GetComponent<Health>().TakeNoDamageForTime(1);
			}
		}

		CloseUmbrella();
	}

	private void CloseUmbrella()
	{
		_closedUmbrella.SetActive(true);
		_openedUmbrella.SetActive(false);
	}

	public override void OnUpdate()
	{
		_currentDashTime -= Time.deltaTime;
		if(_currentDashTime <= 0)
		{
			_currentDashTime = _dashDuration;
			OnDashFinished();
		}
	}

	private IEnumerator ResetMoveSpeedAfterTime(float time)
    {
		yield return new WaitForSeconds(time);

		FindObjectOfType<TopHatCharacter>().Movement.DecreaseMoveSpeed(1);
	}

	public void DecreaseDashCooldown(float value)
    {
		DashCoolDownTime -= value;
    }

	public void IncreaseDashDistance(float value)
    {
		_dashForce += value;
    }
}
