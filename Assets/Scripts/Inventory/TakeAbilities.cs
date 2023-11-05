using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAbilities : Singleton<TakeAbilities>
{
    public bool gainTopHatOnFirstKill = false;

    public void Ability0() // the first time you kill an enemy each stage you gain a top hat
    {
        gainTopHatOnFirstKill = true;
    }

    public bool gainTopHatOnStage1 = false;

    public void Ability1() // at the beginning of each stage you gain a top hat
    {
        gainTopHatOnStage1 = true;
    }

    public bool gainTopHatOnStage2 = false;

    public void Ability2() // at the beginning of each stage you gain a top hat
    {
        gainTopHatOnStage2 = true;
    }

    public bool gain1TopHatsOn1HP = false;

    public void Ability3() // each stage, the first time you only have 1 top hat left you gain another
    {
        gain1TopHatsOn1HP = true;
    }

    public bool mitigateFirstDamage = false;

    public void Ability4() // the first damage you receive eachs tage is mitigated
    {
        mitigateFirstDamage = true;
    }

    public bool takeNoDamageFor2AfterGettingDamaged = false;

    public void Ability5() // you cannot take damage for 2 seconds after taking damaged
    {
        takeNoDamageFor2AfterGettingDamaged = true;
    }

    public bool takeNoDamageFor2AfterKillingAnEnemy = false;

    public void Ability6() // you cannot take damage for 2 seconds after killing an enemy
    {
        takeNoDamageFor2AfterKillingAnEnemy = true;
    }

    public bool takeNoDamageFor0_5AfterAttackingAnEnemy = false;

    public void Ability7() // you cannot take damage for 0.5 second after attacking an enemy
    {
        takeNoDamageFor0_5AfterAttackingAnEnemy = true;
    }

    public void Ability8() // your movement speed is increased
    {
        GameManager.instance.TopHatCharacter.Movement.IncreaseMoveSpeed(1);
    }

    public void Ability9() // your dash distance is increased
    {
        GameManager.instance.TopHatCharacter.Dash.IncreaseDashDistance(10f);
    }

    public void Ability10() // your dash coldown is decreased
    {
        GameManager.instance.TopHatCharacter.Dash.DecreaseDashCooldown(0.5f);
    }

    public bool decreaseEnemyMovementspeed = false;

    public void Ability11() // the enemy movement speed is decreased
    {
        decreaseEnemyMovementspeed = true;
    }

    public bool killFirstEnemyEachStage = false;

    public void Ability12() // each stage you kill the first non-boss enemy you strike instantly
    {
        killFirstEnemyEachStage = true;
    }

    public bool killSecondEnemyEachStage = false;

    public void Ability13() // each stage you kill the second non-boss enemy you strike instantly
    {
        killSecondEnemyEachStage = true;
    }

    public bool killFifthEnemyEachStage = false;

    public void Ability14() // each stage you kill the fifth non-boss enemy you strike instantly
    {
        killFifthEnemyEachStage = true;
    }

    public bool killTenthEnemyEachStage = false;

    public void Ability15() // each stage you kill the tenth non-boss enemy you strike instantly
    {
        killTenthEnemyEachStage = true;
    }

    public bool takeNoDamageFor0_5AfterDash = false;

    public void Ability16() // after you dash you take no damage for 0.5 esconds
    {
        takeNoDamageFor0_5AfterDash = true;
    }

    public bool dealDoubleDamageOnNextAttack = false;

    public void Ability17() // after you dash you deal double damage for 1.5 seconds
    {
        dealDoubleDamageOnNextAttack = true;
    }

    public bool gainTopHatOn10Dashes = false;

    public void Ability18() // after you dash 10 times in a stage you get a top hat
    {
        gainTopHatOn10Dashes = true;
    }

    public bool moveSpeedOnDash = false;

    public void Ability19() // after you dash your movement speed is increased
    {
        moveSpeedOnDash = true;
    }
}
