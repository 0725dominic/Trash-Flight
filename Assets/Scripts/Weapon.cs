using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour 
{
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float shootInterval = 0.1f; // ✅ 발사 간격
    public float ShootInterval => shootInterval; // ✅ 외부에서 읽기 가능

    public float damage = 1f;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    //Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}