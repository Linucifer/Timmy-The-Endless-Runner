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



    private void Awake()
    {
        transform.position = spownPosition;
    }

    private void Update()
    {
        RunForward();

        HandleMovementInput();
    }

    // 处理玩家的移动输入
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            int direction = -1;
            HorizontalMove(direction);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            int direction = 1;
            HorizontalMove(direction);
        }
    }

    // 处理玩家的水平移动
    private void HorizontalMove(int direction)
    {
        // 获取玩家当前的x坐标
        float playerXPos = transform.position.x;

        // 水平左移
        if (direction < 0)
        {   
            // 判断水平移动是否左越界
            if (playerXPos > Street.leftSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }
        // 水平右移
        else
        {
            // 判断水平移动是否右越界
            if (playerXPos < Street.rightSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }

        
    }

    // 玩家向前跑
    private void RunForward()
    {
        transform.Translate(Vector3.forward * runForwardSpeed * Time.deltaTime, Space.World);
    }

}
