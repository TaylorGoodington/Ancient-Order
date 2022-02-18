using UnityEngine;

public class OverworldCameraManager : MonoBehaviour
{
    private bool followPlayer = false;
    private Transform target = null;

    private void LateUpdate()
    {
        AdjustPosition();
    }

    private void AdjustPosition()
    {
        if (followPlayer)
        {
            //transform.position = new Vector3(target.position.x, target.position.y + yDistanceFromPlayerOnMap, target.position.z + zDistanceFromPlayerOnMap);
            transform.position = target.position;
        }
    }

    public void ChangeTaget (Transform newTarget)
    {
        target = newTarget;
    }

    public void ActivateCamera()
    {
        target = MasterControl.Instance.GetOverworldPlayer().gameObject.transform;
        followPlayer = true;
    }
}