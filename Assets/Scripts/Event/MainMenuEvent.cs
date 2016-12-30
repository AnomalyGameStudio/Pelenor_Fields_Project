using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuEvent : MonoBehaviour 
{
	// Starts listening on the OnEnable
	void OnEnable()
	{
		EventManager.StartListening("NewGame", ButtonNewGame);
		EventManager.StartListening("Exit", ButtonExit);
        EventManager.StartListening("LoadLevel", ButtonLoadLevel);
	}

	// Stops listening on the OnDisable to avoid memory leaks
	void OnDisable()
	{
		EventManager.StopListening("NewGame", ButtonNewGame);
		EventManager.StopListening("Exit", ButtonExit);
	}

	//Handles the code from the New game Menu Button
	public void ButtonNewGame()
	{
		Debug.Log("New Game");
        //Application.LoadLevel("level_1-1");
        SceneManager.LoadScene("scene2");
	}

	//Handles the code from the Exit game Menu Button
	public void ButtonExit()
	{
		Debug.Log("Exit game");
	}

    public void ButtonLoadLevel()
    {
        SceneManager.LoadScene("scene2");
    }
}