using System.Collections.Generic;
using UnityEngine;

public class ModelDataController : MonoBehaviour
{
    public static ModelDataController Instance;

    private List<ModelData> modelDatabase;
    private List<int> checkedOutModelInstanceIds;

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

    private void Start()
    {
        modelDatabase = new List<ModelData>();
        checkedOutModelInstanceIds = new List<int>();

        foreach (Transform child in transform)
        {
            modelDatabase.Add(new ModelData(child.gameObject.GetInstanceID(), child.GetComponent<ModelId>().modelId, child.gameObject, child.position));
        }
    }

    public List<GameObject> RetrieveTargetModels(List<int> modelIds)
    {
        List<GameObject> modelObjects = new List<GameObject>();

        for (int i = 0; i < modelIds.Count; i++)
        {
            ModelData modelData = modelDatabase.Find(x => x.modelId == modelIds[i] && !checkedOutModelInstanceIds.Contains(x.unityInstanceId));
            modelObjects.Add(modelData.modelObject);
            checkedOutModelInstanceIds.Add(modelData.unityInstanceId);
        }

        return modelObjects;
    }

    public void CheckModelsIn(List<GameObject> modelObjects)
    {
        foreach (GameObject model in modelObjects)
        {
            int instanceId = model.GetInstanceID();
            model.transform.parent = transform;
            model.transform.position = modelDatabase.Find(x => x.unityInstanceId == instanceId).homeLocation;
            checkedOutModelInstanceIds.Remove(instanceId);
        }
    }
}

public struct ModelData
{
    public int unityInstanceId;
    public int modelId;
    public GameObject modelObject;
    public Vector3 homeLocation;

    public ModelData(int unityInstanceId, int modelId, GameObject modelObject, Vector3 homeLocation)
    {
        this.unityInstanceId = unityInstanceId;
        this.modelId = modelId;
        this.modelObject = modelObject;
        this.homeLocation = homeLocation;
    }
}