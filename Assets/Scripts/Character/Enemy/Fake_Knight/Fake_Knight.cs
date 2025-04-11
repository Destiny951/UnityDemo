using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Knight : Enemy
{
    [Header("Pool Tag")]
    public string poolTag = "FakeKnight";

    #region State
    public Fake_Knight_IdleState idleState { get; private set; }
    public Fake_Knight_JumpState fallState { get; private set; }
    public Fake_Knight_LandState landState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new Fake_Knight_IdleState(this, stateMachine, "idle", this);
        fallState = new Fake_Knight_JumpState(this, stateMachine, "jump", this);
        landState = new Fake_Knight_LandState(this, stateMachine, "land", this);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnSpawnedFromPool()
    {
        stateMachine.Initialize(fallState);
    }

    public override void OnReturnedToPool()
    {
        StopAllCoroutines(); // ��ֹЭ��й¶
    }
}
