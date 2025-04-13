using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShakeCamera : Fake_Knight_Action
{
    [Header("�𶯲���")]
    public SharedFloat shakeStrength = 1f;

    [BehaviorDesigner.Runtime.Tasks.Tooltip("0 = Vertical�����£���1 = Horizontal�����ң�")]
    public SharedInt shakeDirection = 0;

    public override TaskStatus OnUpdate()
    {
        var shake = boss.GetComponent<CameraShake>();
        if (shake != null)
        {
            // �� SharedInt תΪ enum
            CameraShake.ShakeDirection dir = (CameraShake.ShakeDirection)shakeDirection.Value;

            shake.Shake(shakeStrength.Value, dir);
        }
        else
        {
            Debug.LogWarning("CameraShake ���δ���� Boss ���ϣ�");
        }

        return TaskStatus.Success;
    }
}
