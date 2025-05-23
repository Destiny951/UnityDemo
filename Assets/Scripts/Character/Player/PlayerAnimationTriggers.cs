using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    [SerializeField]private UI_MainScene_Menu ui_main_scene_menu;
    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);
            }
        }
    }
    private void DieTrigger()
    {
        player.AnimationTrigger();
        player.gameObject.SetActive(false);
        ui_main_scene_menu.YouDie.SetActive(true);
    }
}
