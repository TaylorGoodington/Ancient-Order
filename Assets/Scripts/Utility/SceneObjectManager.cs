using UnityEngine;

public class SceneObjectManager : MonoBehaviour
{
    public bool realWorldObject;
    public Material[] transparentMaterials;

    private float minWidthForTransparency = 3;
    private MeshRenderer meshRenderer;
    private BoxCollider physicalCollider;
    private Material[] normalMaterials = null;
    private bool canBecomeTransparent = false;
    private bool isInDigitalWorldTrigger = false;

    void Start ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        physicalCollider = GetComponent<BoxCollider>();
        normalMaterials = meshRenderer.materials;
        canBecomeTransparent = (transparentMaterials.Length > 0 && transform.localScale.x > minWidthForTransparency) ? true : false;
        SetRendererAndColliderForWorld();
    }

    //Works only if the first collider on an object is its physical collider.
    private void SetRendererAndColliderForWorld()
    {
        if (realWorldObject)
        {
            meshRenderer.enabled = true;
            physicalCollider.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
            physicalCollider.enabled = false;
        }
    }

    private void AdjustForDigitalWorld ()
    {
        if (isInDigitalWorldTrigger)
        {
            isInDigitalWorldTrigger = false;
            SetRendererAndColliderForWorld();
        }
    }

    private void BecomeTransparent ()
    {
        if (canBecomeTransparent)
        {
            meshRenderer.materials = transparentMaterials;
        }
    }

    private void BecomeOpaque ()
    {
        if (canBecomeTransparent)
        {
            meshRenderer.materials = normalMaterials;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("MainCamera"))
        {
            BecomeTransparent();
        }
        else if (collider.tag.Equals("DigitalWorldTrigger"))
        {
            isInDigitalWorldTrigger = true;
            SetRendererAndColliderForWorld();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("MainCamera"))
        {
            BecomeOpaque();
        }
    }
}