using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAbilities : Singleton<GiveAbilities>
{
    public bool moveSpeed0 = false;

    public void Ability0() // Enemies movement speed is slightly increased
    {
        moveSpeed0 = true;
    }

    public bool moveSpeed1 = false;

    public void Ability1() // Enemies movement speed is slightly increased
    {
        moveSpeed1 = true;
    }

    public bool moveSpeed2 = false;

    public void Ability2() // Enemies movement speed is heavily increased
    {
        moveSpeed2 = true;
    }

    public bool extraHat1 = false;

    public void Ability3() // enemies need an additional hit to be slain
    {
        extraHat1 = true;
    }

    public bool extraHat2 = false;

    public void Ability4() // The first hit an enemy receives deals no damage
    {
        extraHat2 = true;
    }

    public bool noDamageFor5 = false;

    public void Ability5() // for the first 5 seconds of a stage enemies do not take damage
    {
        noDamageFor5 = true;
    }


    public bool noDamageFor2AfterEnemyDeath = false;

    public void Ability6() // after an enemy is killed other enemies do not take damage for 2 seconds
    {
        noDamageFor2AfterEnemyDeath = true;
    }

    public void Ability7() // each stage spawn an additional enemy
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability8() // each stage spawn an additional enemy
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability9() // each stage spawn an additional enemy
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability10() // each stage spawn an additional enemy
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability11() // each stage spawn two additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability12() // each stage spawn two additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability13() // each stage spawn two additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability14() // each stage spawn three additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability15() // each stage spawn three additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public void Ability16() // each stage spawn five additional enemies
    {
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
        FindAnyObjectByType<EnemySpawner>().AddRandomEnemyToSpawn();
    }

    public bool firstHitDealsDoubleDamage = false;

    public void Ability17() // the first enemy that hits you each stage deals double damage
    {
        firstHitDealsDoubleDamage = true;
    }

    public bool receiveDoubleDamageAt7HP = false;

    public void Ability18() // if you have 7 or more top hats enemies deal double damage
    {
        receiveDoubleDamageAt7HP = true;
    }

    public bool noDamageFor0_5AfterAttacking = false;

    public void Ability19() // you deal no damage 0.5 seconds after attacking
    {
        noDamageFor0_5AfterAttacking = true;
    }

    public bool noDamageFor1AfterPlayDash = false;

    public void Ability20() // you deal no damage for 1 seconds after having dashed
    {
        noDamageFor1AfterPlayDash = true;
    }

    public bool noDamageFor1AfterEnemyDeath = false;

    public void Ability21() // you deal no damage for 1 seconds after slaying an enemy
    {
        noDamageFor1AfterEnemyDeath = true;
    }

    public bool noMoveFor0_5AfterEnemyDeath = false;

    public void Ability22() // you cannot move for 1 second after slaying an enemy
    {
        noMoveFor0_5AfterEnemyDeath = true;
    }

    public void Ability23() // for the first 3 seconds of each stage you cannot move
    {
        GameManager.instance.TopHatCharacter.Movement.SetMoveSpeed0();
        StartCoroutine(ResetMoveSpeedAfterTime(GameManager.instance.TopHatCharacter, 2.5f));
    }

    public IEnumerator ResetMoveSpeedAfterTime(TopHatCharacter player, float time)
    {
        yield return new WaitForSeconds(time);

        player.Movement.ResetMoveSpeed();
    }
}
