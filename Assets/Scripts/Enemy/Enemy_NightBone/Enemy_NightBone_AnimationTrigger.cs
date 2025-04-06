using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey_NightBone_AnimationTrigger : MonoBehaviour
{
    private Enemy_NightBone enemy => GetComponentInParent<Enemy_NightBone>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
                hit.GetComponent<CharacterStats>().TakeDamage(enemy.stats.damage);
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
