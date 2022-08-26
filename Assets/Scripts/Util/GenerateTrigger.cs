using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Character"))
        {
            SectionController.GenerateSection();
        }
    }

}
