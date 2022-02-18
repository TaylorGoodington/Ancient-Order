using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraManager : MonoBehaviour
{
    private void LateUpdate()
    {
        AdjustPosition();
    }

    private void AdjustPosition()
    {
        
    }

    public void ActivateCamera(Vector3 setPosition)
    {
        transform.position = setPosition;
    }
}