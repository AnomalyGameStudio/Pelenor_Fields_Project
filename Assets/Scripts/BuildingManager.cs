using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

    public GameObject selectedTower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectTowerType(GameObject prefab)
    {
        Debug.Log("Prefab: " + prefab.name);
        selectedTower = prefab;
    }
}
