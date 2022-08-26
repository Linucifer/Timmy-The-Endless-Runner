using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (transform.position.z < mainCam.transform.position.z - SectionController.sectionLenght)
        {
            gameObject.SetActive(false);
        }
    }

    
}
