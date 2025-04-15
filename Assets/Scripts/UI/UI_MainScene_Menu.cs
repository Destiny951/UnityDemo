using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainScene_Menu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainMenu";
    [SerializeField] private GameObject Character;
    [SerializeField] public GameObject YouDie;
    private bool isPaused = false;
    // Start is called before the first frame update

    private void Start()
    {
        SessionFlags.isLoadingFromSave = true; //�����ã���ʽ��ɾ��

        SaveManager.instance.LoadGame();
        SessionFlags.isLoadingFromSave = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void SaveGameBtn()
    {
        SaveManager.instance.SaveGame();
        TogglePause();
        Debug.Log("Game Saved");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ��ͣ��Ϸ
            Character.SetActive(true); // ��ʾ�˵�
        }
        else
        {
            Time.timeScale = 1f; // �ָ���Ϸ
            Character.SetActive(false); // ���ز˵�
        }
    }


    public void RestartGameButton()
    {
        GameManager.instance.RestartScene();
    }
}
