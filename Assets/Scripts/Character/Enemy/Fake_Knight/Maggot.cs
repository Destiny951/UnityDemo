using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maggot : Enemy
{
    private Fake_Knight boss;
    private bool flipOnce = false;
    public bool stateTrigger = false;
    protected override void Start()
    {
        base.Start();
        boss = GetComponentInParent<Fake_Knight>();
    }


    protected override void Update()
    {
        FlipUI();
    }

    public override void Die()
    {
        base.Die();
        boss.stats.Rebirth();
        boss.stateTrigger = false;

        stateTrigger = true;
        flipOnce = false;
        stats.Rebirth();
        gameObject.SetActive(false);
    }

    private void FlipUI()
    {
        if (flipOnce) return;
        if (boss.facingDirection != 1 && !flipOnce)
        {
            flipOnce = true;
            onFlipped?.Invoke();
        }
    }

}
