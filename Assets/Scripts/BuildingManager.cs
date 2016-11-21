using UnityEngine;
using System.Collections;

public class BuildingManager : Singleton<BuildingManager>
{
    public GameObject selectedTower;

    protected BuildingManager() {}
    
    public void SelectTowerType(GameObject prefab)
    {
        Debug.Log("Prefab: " + prefab.name);
        selectedTower = prefab;
    }
}
