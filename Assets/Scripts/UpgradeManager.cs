using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI upgradeCostText;

    private int coin;
    private float attackPower;
    private int weaponIndex;

    private int baseCost = 10;             // 기본 업그레이드 비용
    private int upgradeLevel = 0;          // 현재 업그레이드 단계

    void Start()
    {
        LoadData();
        UpdateUI();
    }

    void LoadData()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        attackPower = SaveManager.LoadAttackPower();
        weaponIndex = SaveManager.LoadWeaponIndex();
        upgradeLevel = PlayerPrefs.GetInt("UpgradeLevel", 0); // 저장된 업그레이드 단계 불러오기
    }

    void UpdateUI()
    {
        coinText.text = $"Coin: {coin}";
        attackPowerText.text = $"Damage: {attackPower}";

        int cost = baseCost + (upgradeLevel / 2); // ✅ 2레벨마다 비용 1 증가
        upgradeCostText.text = $"Upgrade Cost: {cost}";
    }

    public void OnUpgradeButton()
    {
        int cost = baseCost + (upgradeLevel / 2); // ✅ 같은 계산 사용

        if (coin >= cost)
        {
            coin -= cost;
            attackPower += 1f;

            // 일정 공격력 이상이면 무기 교체
            if (attackPower >= 5f && weaponIndex < 2)
            {
                weaponIndex++;
            }

            upgradeLevel++; // 업그레이드 단계 증가

            // 저장
            SaveManager.SaveUpgradeData(attackPower, weaponIndex);
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("UpgradeLevel", upgradeLevel);
            PlayerPrefs.Save();

            UpdateUI();
        }
        else
        {
            Debug.Log($"Need cost : {cost}");
        }
    }

    public void OnResetButton()  // 디버깅용 리셋 버튼
    {
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("UpgradeLevel", 0);
        SaveManager.SaveUpgradeData(0f, 0);

        coin = 0;
        attackPower = 0f;
        weaponIndex = 0;
        upgradeLevel = 0;

        UpdateUI();
    }
}
