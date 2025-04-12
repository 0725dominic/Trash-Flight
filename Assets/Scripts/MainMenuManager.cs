using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // 게임 씬 이름
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료"); // 테스트용 로그
    }
}