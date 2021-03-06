﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LevelList : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject parent;

    public string levelPath;
    public string extension;

	void Start ()
    {

        string[] levels = Directory.GetFiles(Application.streamingAssetsPath + levelPath, "*." + extension, SearchOption.AllDirectories);

        foreach (string level in levels)
        {
            string levelFile = Path.GetFileNameWithoutExtension(level);
            
            Debug.Log(levelFile);

            // Instantiate the Button prefab
            GameObject buttonGO = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Sets the Parent as the Grid in order for it to be scrollable
            buttonGO.transform.parent = parent.transform;

            // Gets the button component and Adds to the Listener the LoadLevel method
            Button button = buttonGO.GetComponent<Button>();
            button.onClick.AddListener(() => LoadLevel(levelFile));

            // Change the Text Label of the Button
            button.GetComponentInChildren<Text>().text = levelFile;
        }

        
    }
	
    public void LoadLevel(string level)
    {
        // Can use this to pass the values I need to the level loader in the scene
        LevelLoader.instance.level = level;
        // Use the Event Manager to Load the new Level
        EventManager.TriggerEvent("LoadLevel");
    }

}
