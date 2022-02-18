using System.Collections.Generic;
using UnityEngine;

//Order on the positions should be enemies 1-3, allies 1-2, player
public class CombatSetObject : MonoBehaviour
{
    public int id;
    public AudioClip music;

    private List<Transform> positions;

    private void Start()
    {
        positions = new List<Transform>();

        foreach (Transform child in transform)
        {
            positions.Add(child);
        }
    }

    public void PlaceModels(List<GameObject> models)
    {
        for (int i = 0; i < models.Count; i++)
        {
            if (models[i] == null)
            {
                Debug.Log("Missing model at position " + i);
            }
            else
            {
                models[i].transform.parent = positions[i];
                models[i].transform.localPosition = Vector3.zero;
            }
        }
    }
}