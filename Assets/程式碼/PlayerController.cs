using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 開啟重力
        rb.useGravity = true;

        // 只鎖定旋轉，不鎖定位置
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        Vector3 next = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(next);
    }

}
