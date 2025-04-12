using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float shootInterval;

    private int weaponIndex = 0;
    private float lastShotTime = 0f;
    private float attackPower = 1f; // ✅ 공격력 저장용 변수

    void Start()
    {
        // ✅ 저장된 데이터 불러오기
        attackPower = SaveManager.LoadAttackPower();
        weaponIndex = SaveManager.LoadWeaponIndex();
    }

    void Update()
    {
        // ⏸️ 일시정지 또는 게임오버 시 동작 정지
        if (Time.timeScale == 0f || GameManager.instance.isGameOver)
            return;

        HandleMovement();
        Shoot();
    }

    void HandleMovement()
    {
        Vector3 inputPosition = Vector3.zero;
        bool hasInput = false;

        // 🖱️ 마우스 입력 (에디터용)
        if (Application.isEditor)
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hasInput = true;
        }
        // 📱 터치 입력 (모바일용)
        else if (Input.touchCount > 0)
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            hasInput = true;
        }

        if (hasInput)
        {
            float toX = Mathf.Clamp(inputPosition.x, -2.35f, 2.35f);
            transform.position = new Vector3(toX, transform.position.y, transform.position.z);
        }
    }

    void Shoot()
    {
        GameObject weaponPrefab = weapons[weaponIndex];
        Weapon weaponComponent = weaponPrefab.GetComponent<Weapon>();
        float currentInterval = weaponComponent.ShootInterval;

        if (Time.time - lastShotTime > currentInterval)
        {
            Instantiate(weaponPrefab, shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        attackPower += 1f;

        // 일정 공격력 이상이면 무기 인덱스 증가
        if (attackPower >= 5f && weaponIndex < weapons.Length - 1)
        {
            weaponIndex++;
        }

        // 저장
        SaveManager.SaveUpgradeData(attackPower, weaponIndex);
    }
}
