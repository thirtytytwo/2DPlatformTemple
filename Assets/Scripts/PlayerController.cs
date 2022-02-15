using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //组件
    private Rigidbody2D playerRig;
    private PlayerInput inputController;

    [Header("动作参数")]
    //移动
    public float moveSpeed = 100f;
    //跳跃
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpForce;

    [Header("信号")]
    public bool isOnGround;

    [Header("信号检测参数")]
    public LayerMask groundLayer;
    public Vector2 groundCheckOffset;
    public float groundCheckRadius;

    private void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        inputController = GetComponent<PlayerInput>();

    }

    private void Update()
    {
        #region 信号检测
        isOnGround = Physics2D.OverlapCircle((Vector2)transform.position + groundCheckOffset, groundCheckRadius, groundLayer);
        #endregion
        #region 更新角色方向
        if (inputController.forwardAndBackward > 0.1f)
        {
            transform.right = new Vector3(1, 0, 0);
        }
        else if(inputController.forwardAndBackward < -0.1f)
        {
            transform.right = new Vector3(-1, 0, 0);
        }
        #endregion
        //角色跳跃
        Jump();
    }

    private void FixedUpdate()
    {
        //角色移动
        GroundMovement();
        print(playerRig.velocity.y);
    }

    private void GroundMovement()
    {
        if(inputController.forwardAndBackward > 0.1f || inputController.forwardAndBackward < -0.1f)
        {
            playerRig.velocity = new Vector2(moveSpeed * inputController.forwardAndBackward * Time.fixedDeltaTime, playerRig.velocity.y);
        }
    }
    private void Jump()
    {
        //检测下落和短跳，对跳跃进行优化
        if(playerRig.velocity.y < 0)
        {
            playerRig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(playerRig.velocity.y > 0 && !inputController.jumpHold)
        {
            playerRig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //跳跃
        if(inputController.jumpPress && isOnGround)
        {
            playerRig.velocity += Vector2.up * jumpForce;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckOffset, groundCheckRadius);
    }
}
