using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("怪物移動速度")]
    public float moveSpeed = 2f;

    [Header("左右跟隨速度")]
    public float followSpeed = 2f;

    [Header("地板偵測")]
    public LayerMask groundLayer;
    public float groundCheckHeight = 1.5f;
    public float groundCheckDistance = 3f;
    public float edgeCheckDistance = 0.2f;

    private Transform player;
    private Rigidbody rb;

    // 是否已經抵達懸崖邊緣
    private bool atEdge = false;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
        }

        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

       if (!atEdge)
        {
            // 第一階段：沿 Z 軸追蹤玩家
            ChasePlayer();
        }
        else
        {
            // 第二階段：沿 X 軸跟隨玩家
            FollowPlayerSideways();
        }
    }

    // 第一階段：追蹤玩家
    void ChasePlayer()
    {
        float directionZ = Mathf.Sign(player.position.z - rb.position.z);

        Vector3 direction = new Vector3(0f, 0f, directionZ);

        Vector3 nextPosition = rb.position +
            direction * moveSpeed * Time.fixedDeltaTime;

        // 提前檢查前方是否有地板
        Vector3 checkPosition =
            nextPosition + direction * edgeCheckDistance;

        if (HasGround(checkPosition))
        {
            rb.MovePosition(nextPosition);
        }
        else
        {
            // 前方沒有地板，進入邊緣跟隨模式
            atEdge = true;
        }
    }

    // 第二階段：左右跟隨玩家
    void FollowPlayerSideways()
    {
        float targetX = player.position.x;

        float nextX = Mathf.MoveTowards(
            rb.position.x,
            targetX,
            followSpeed * Time.fixedDeltaTime
        );

        Vector3 nextPosition = new Vector3(
            nextX,
            rb.position.y,
            rb.position.z
        );

        // 檢查左右移動後是否仍有地板
        if (HasGround(nextPosition))
        {
            rb.MovePosition(nextPosition);
        }
    }

    // 偵測指定位置下方是否有地板
    bool HasGround(Vector3 position)
    {
        Vector3 rayOrigin =
            position + Vector3.up * groundCheckHeight;

        return Physics.Raycast(
            rayOrigin,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );
    }
}