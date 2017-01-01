using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelList : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject parent;

	
	void Start ()
    {
        // Instantiate the Button prefab
        GameObject buttonGO = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        // Sets the Parent as the Grid in order for it to be scrollable
        buttonGO.transform.parent = parent.transform;

        // Gets the button component and Adds to the Listener the LoadLevel method
        Button button = buttonGO.GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);

        // Change the Text Label of the Button
        button.GetComponentInChildren<Text>().text = "Level 3";
    }
	
    public void LoadLevel()
    {
        // Can use this to pass the values I need to the level loader in the scene
        LevelLoader.instance.level = "Level3";
        // Use the Event Manager to Load the new Level
        EventManager.TriggerEvent("LoadLevel");
    }

}
