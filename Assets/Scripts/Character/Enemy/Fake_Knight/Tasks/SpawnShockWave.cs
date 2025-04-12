using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class SpawnShockWave : Fake_Knight_Action
{
    public string objectTag = "ShockWave";
    public LayerMask whatisGround;
    private GameObject shockWave;
    // Start is called before the first frame update
    public override void OnStart()
    {
        Generate();
    }

    private void Generate()
    {
        shockWave = ObjectManager.instance.GetObjectItem(objectTag);

        RaycastHit2D hit = Physics2D.Raycast(boss.attackCheck.position, Vector2.down, 10f, whatisGround);

        if (hit.collider != null)
        {
            // ���ò���λ�ã�X ʹ�ù����㣬Y ʹ�õ���߶�
            shockWave.transform.position = new Vector2(boss.attackCheck.position.x, hit.point.y);
        }
        else
        {
            // ���û��⵽���棬����ԭλ��
            shockWave.transform.position = boss.attackCheck.position;
        }

        //ͨ������ע�����
        var wave = shockWave.GetComponent<ShockWave>();
        wave.SetDirection(boss.facingDirection);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
    public override void OnEnd()
    {

    }
}
