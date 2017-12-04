using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "title":
                    QuitGame();
                    break;
                case "worldmap":
                    SceneManager.LoadScene("title");
                    break;
                default:
                    SceneManager.LoadScene("worldmap");
                    break;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
