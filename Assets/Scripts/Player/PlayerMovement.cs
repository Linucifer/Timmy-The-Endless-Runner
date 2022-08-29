using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 玩家出生点
    public Vector3 spownPosition;

    // 玩家向前奔跑速度
    [SerializeField]
    private float runForwardSpeed = 3f;

    // 玩家左右移动速度
    [SerializeField]
    private float moveHorizonSpeed = 2f;

    [SerializeField]
    private float jumpHeight = 2f;          // 玩家跳跃高度(只允许一段跳)

    // 玩家的移动状态
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jumpable = false;
    private bool isGrounded = true; // 玩家是否接触地面
    private Vector3 jumpVelocity;   // 记录玩家在垂直方向上的速度(起跳速度)

    private Rigidbody m_rigidbody;


    // 初始化
    private void Awake()
    {
        transform.position = spownPosition; // 初始化玩家出生点
        jumpVelocity = Vector3.zero;        // 初始化玩家的起跳速度
        m_rigidbody = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        jumpVelocity = new Vector3(0, Mathf.Sqrt(2f * 9.81f * jumpHeight));
        SectionController.GenerateSection();
    }

    private void Update()
    {
        MovementStateReset();

        HandleVerticalMovementInput();
        HandlePlayerVerticalMovement();

        HandleHorizontalMovementInput();
        HandlePlayerHorizontalMovement();
    }


    // 处理玩家的垂直移动输入（跳跃）
    private void HandleVerticalMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                jumpable = true;
            }
        }
    }
    // 玩家跳跃（垂直移动）
    private void Jump()
    {
        m_rigidbody.velocity = jumpVelocity;
    }
    // 处理玩家的垂直移动（跳跃）
    private void HandlePlayerVerticalMovement()
    {
        if (jumpable)
            Jump();
    }


    // 处理玩家的水平移动输入
    private void HandleHorizontalMovementInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveLeft = true;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveRight = true;

    }
    // 玩家的左右移动
    private void HorizontalMove(int direction)
    {
        // 获取玩家当前的x坐标
        float playerXPos = transform.position.x;

        // 水平左移
        if (direction < 0)
        {   
            // 判断水平移动是否左越界
            if (playerXPos > SectionController.leftSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }
        // 水平右移
        else
        {
            // 判断水平移动是否右越界
            if (playerXPos < SectionController.rightSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }

        
    }
    // 玩家向前跑
    private void RunForward()
    {
        transform.Translate(Vector3.forward * runForwardSpeed * Time.deltaTime, Space.World);
    }
    // 处理玩家水平移动
    private void HandlePlayerHorizontalMovement()
    {
        ResetPlayerRotation();

        RunForward();

        if (moveLeft)
            HorizontalMove(-1);
        if (moveRight)
            HorizontalMove(1);

    }
    
    // 重置玩家的移动状态
    private void MovementStateReset()
    {
        if (Mathf.Abs(transform.position.y) <= 0.51f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        moveRight = false;
        moveLeft = false;
        jumpable = false;
    }

    // 使玩家始终面向前方
    private void ResetPlayerRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

}
