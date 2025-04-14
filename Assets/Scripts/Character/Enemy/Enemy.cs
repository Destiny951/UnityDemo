using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine { get; private set; }
    public string lastStateName { get; private set; }

    [SerializeField] protected LayerMask whatisPlayer;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;
    public bool isStunned = false;

    [Header("Attack info")]
    public float playerCheckDistance;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Move info")]
    public float moveSpeed;
    public float moveTime;
    public float idleTime;
    public float battleTime;
    public float jumpForce = 0f;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public virtual void AssignLastAnimName(string _stateName)
    {
        lastStateName = _stateName;
    }

    //双向检测玩家方法
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, playerCheckDistance, whatisPlayer);

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void OpenCounterAttackWindow()
    {
        // 使敌人可以被反击
        canBeStunned = true;
        if (counterImage != null)
        {
            counterImage.SetActive(true);
        }
    }

    public virtual void CloseCounterAttackWindow()
    {
        // 使敌人不能被反击
        canBeStunned = false;
        if (counterImage != null)
        {
            counterImage.SetActive(false);
        }
    }

    public virtual bool CanbeStunned()
    {
        // 检测敌人是否可以被反击
        if (canBeStunned)
        {
            return true;
        }
        return false;
    }

    // for behavior tree
    public virtual void ChangeStunned()
    {
        isStunned = !isStunned;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistance * facingDirection, wallCheck.position.y));
    }

    public virtual void AnimationFinishTrigger() =>
        stateMachine.currentState.AnimationFinishTrigger();


    public void LoadData(GameData _data)
    {
        Debug.Log(_data);
        this.stats.SetCurrentHP(_data.currentHP);
        this.transform.position = _data.position;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currentHP = this.stats.GetCurrentHP();
        _data.position = this.transform.position;
    }
}
