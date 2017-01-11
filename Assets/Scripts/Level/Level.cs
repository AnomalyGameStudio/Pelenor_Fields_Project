using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelLoader.instance.LoadLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
