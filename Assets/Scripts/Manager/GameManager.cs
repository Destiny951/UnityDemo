using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;


    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void RestartScene()
    {
        // Restart the current scene
        SaveManager.instance.NewGame();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void FreezeTime(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        Time.timeScale = 0f;                      // ��ͣ��Ϸʱ��
        yield return new WaitForSecondsRealtime(duration);  // �ȴ���ʵ����ʱ�䣨���� Time.timeScale Ӱ�죩
        Time.timeScale = 1f;                      // �ָ���Ϸʱ��
    }

}
