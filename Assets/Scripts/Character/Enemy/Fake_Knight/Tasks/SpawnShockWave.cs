using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SpawnShockWave : Fake_Knight_Action
{
    public string objectTag = "ShockWave";
    public LayerMask whatisGround;

    public override void OnStart()
    {
        Generate();
    }

    private void Generate()
    {
        GameObject shockWave = ObjectManager.instance.GetObjectItem(objectTag);

        if (shockWave == null)
        {
            Debug.LogWarning("[SpawnShockWave] �޷��Ӷ�����л�ȡ ShockWave");
            return;
        }

        Vector2 spawnPos = boss.attackCheck.position;

        // ʹ�� Raycast ��ȷ��������
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 10f, whatisGround);
        if (hit.collider != null)
        {
            spawnPos = new Vector2(spawnPos.x, hit.point.y);
        }

        // ���� ShockWave
        ShockWave wave = shockWave.GetComponent<ShockWave>();
        wave.Activate(spawnPos, boss.facingDirection);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        // ��ѡ���������ʱ�����߼�
    }
}
