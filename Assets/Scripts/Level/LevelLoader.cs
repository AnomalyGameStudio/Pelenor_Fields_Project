using UnityEngine;
using System.Collections;
using System.Xml;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader instance;
    public XmlDocument xmlLevel;
    public string level = "level1";

    // Another Singleton Implementation. Seems better than the Singleton.cs interface
    void Awake()
    {
        if(instance == null)
        {
            // Prevents this object to be destroy
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if ( instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel()
    {
        Debug.Log("Level " + level + " loaded");

        if(xmlLevel != null)
        {
            Debug.Log(xmlLevel.GetElementById("name").InnerXml);
        }

    }
}
