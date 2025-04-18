using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header ("Major stats")]
    public Stat strength;


    public Stat damage;
    public Stat maxHP;

    private int currentHP;

    public System.Action onHealthChanged;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHP = maxHP.GetValue();
    }


    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = strength.GetValue() + damage.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    // Update is called once per frame
    public virtual void TakeDamage(int _damage)
    {
        currentHP -= _damage;
        onHealthChanged?.Invoke();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Rebirth()
    {
        currentHP = maxHP.GetValue();
        onHealthChanged?.Invoke();
    }

    public virtual void IncreaseMaxHP(int _amount)
    {
        maxHP.AddModifier(_amount);
        currentHP += _amount;
        onHealthChanged?.Invoke();
    }

    public virtual int GetCurrentHP()
    {
        return currentHP;
    }

    public virtual void SetCurrentHP(int _amount)
    {
        currentHP = _amount;
        onHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
    }
}
