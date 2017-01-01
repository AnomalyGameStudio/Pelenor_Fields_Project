using UnityEngine;
using System.Collections;

public class MainMenuEventTrigger : MonoBehaviour 
{
	public void StartOnClick()
	{
		EventManager.TriggerEvent("NewGame");
	}

	public void ExitButton()
	{
		EventManager.TriggerEvent("Exit");
	}

    public void LoadLevel()
    {
        EventManager.TriggerEvent("LoadLevel");
    }

    public void TestThings(string wtf)
    {
        Debug.Log(wtf);
    }
}
