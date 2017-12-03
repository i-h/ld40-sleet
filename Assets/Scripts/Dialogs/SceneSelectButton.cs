using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSelectButton : MonoBehaviour {
    public string Target = "";
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Change);
    }
    public void Change()
    {
        SceneManager.LoadScene(Target);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
