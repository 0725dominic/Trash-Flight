using UnityEngine;

public static class SaveManager
{
    public static void SaveUpgradeData(float attackPower, int weaponIndex)
    {
        PlayerPrefs.SetFloat("AttackPower", attackPower);
        PlayerPrefs.SetInt("WeaponIndex", weaponIndex);
        PlayerPrefs.Save();
    }

    public static float LoadAttackPower()
    {
        return PlayerPrefs.GetFloat("AttackPower", 1f); // 기본값 1
    }

    public static int LoadWeaponIndex()
    {
        return PlayerPrefs.GetInt("WeaponIndex", 0); // 기본값 0
    }
}
