using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ��ҳ�����
    public Vector3 spownPosition;

    // �����ǰ�����ٶ�
    [SerializeField]
    private float runForwardSpeed = 3f;

    // ��������ƶ��ٶ�
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

    // ������ҵ��ƶ�����
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

    // ������ҵ�ˮƽ�ƶ�
    private void HorizontalMove(int direction)
    {
        // ��ȡ��ҵ�ǰ��x����
        float playerXPos = transform.position.x;

        // ˮƽ����
        if (direction < 0)
        {   
            // �ж�ˮƽ�ƶ��Ƿ���Խ��
            if (playerXPos > Street.leftSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }
        // ˮƽ����
        else
        {
            // �ж�ˮƽ�ƶ��Ƿ���Խ��
            if (playerXPos < Street.rightSide)
                transform.Translate(Vector3.right * direction * moveHorizonSpeed * Time.deltaTime, 
                    Space.World);
        }

        
    }

    // �����ǰ��
    private void RunForward()
    {
        transform.Translate(Vector3.forward * runForwardSpeed * Time.deltaTime, Space.World);
    }

}
