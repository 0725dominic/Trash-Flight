using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;
    public int Coin => coin;
    [HideInInspector]
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        LoadCoin();           // ⬅️ 저장된 코인 불러오기
        UpdateCoinUI();       // ⬅️ 코인 텍스트 갱신
        UpdateCursorState();  // 커서 설정
    }

    public void IncreaseCoin(int amount = 1)
    {
        coin += amount;
        SaveCoin();        // ⬅️ 저장
        UpdateCoinUI();    // ⬅️ 텍스트 갱신

    }

    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner enemySpawner = FindFirstObjectByType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);

        UpdateCursorState(); // 게임 오버 시 커서 보이기
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    // ✅ 커서 상태 업데이트 함수
    public void UpdateCursorState()
    {
        if (isGameOver || Time.timeScale == 0f || SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        coin = PlayerPrefs.GetInt("Coin", 0); // 저장된 게 없으면 0
    }

    void UpdateCoinUI()
    {
        text.SetText(coin.ToString());
    }
    

    public bool SpendCoin(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;
            SaveCoin();
            UpdateCoinUI();
            return true;
        }
        return false;
    }

}
