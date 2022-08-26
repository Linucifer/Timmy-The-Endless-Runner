using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRotate : MonoBehaviour
{
    public float rotateSpeed = 360f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }
}
