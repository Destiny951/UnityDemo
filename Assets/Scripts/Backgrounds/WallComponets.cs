using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallComponets : MonoBehaviour
{
    [SerializeField]private bool isDeathZone = true; //�ж��Ƿ�Ϊ��������
    // Start is called before the first frame update

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if(collision.CompareTag("Player")&& isDeathZone)
        {
            Debug.Log("Player is dead");
            PlayerManager.instance.player.Die();
        }
    }
}
