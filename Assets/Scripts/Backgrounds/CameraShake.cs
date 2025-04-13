using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    public enum ShakeDirection
    {
        Vertical,
        Horizontal
    }

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// �𶯾�ͷ��ָ��ǿ�Ⱥͷ���
    /// </summary>
    public void Shake(float strength = 1f, ShakeDirection direction = ShakeDirection.Vertical)
    {
        if (impulseSource == null)
        {
            Debug.LogWarning("CinemachineImpulseSource δ�ҵ���");
            return;
        }

        Vector3 shakeVector = direction switch
        {
            ShakeDirection.Vertical => Vector3.down * strength,
            ShakeDirection.Horizontal => Vector3.left * strength,
            _ => Vector3.zero
        };

        impulseSource.GenerateImpulse(shakeVector);
    }
}
