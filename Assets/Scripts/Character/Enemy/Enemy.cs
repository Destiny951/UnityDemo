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

    //˫������ҷ���
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, playerCheckDistance, whatisPlayer);

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void OpenCounterAttackWindow()
    {
        // ʹ���˿��Ա�����
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        // ʹ���˲��ܱ�����
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanbeStunned()
    {
        // �������Ƿ���Ա�����
        if (canBeStunned)
        {
            return true;
        }
        return false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistance * facingDirection, wallCheck.position.y));
    }

    public virtual void AnimationFinishTrigger() =>
        stateMachine.currentState.AnimationFinishTrigger();


}
