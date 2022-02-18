using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private OverworldCameraManager overworldCamera;
    [SerializeField] private CombatCameraManager combatCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void ActivateOverWorldCamera()
    {
        overworldCamera.gameObject.SetActive(true);
        overworldCamera.ActivateCamera();
        combatCamera.gameObject.SetActive(false);
    }

    public void ActivateCombatCamera(Vector3 setPosition)
    {
        combatCamera.gameObject.SetActive(true);
        combatCamera.ActivateCamera(setPosition);
        overworldCamera.gameObject.SetActive(false);
    }
}