using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void PlayGameButton(string gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }
    public void StopApplication()
    {
        Application.Quit();
    }
}
