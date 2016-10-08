using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public RectTransform mainMenu;
    public RectTransform creditsMenu;

    private RectTransform currentMenu;

	// Use this for initialization
	void Start () {
        currentMenu = mainMenu;
	}

    public void ChangeTo(RectTransform rect)
    {
        if (rect == null)
        {
            return;
        }
        rect.gameObject.SetActive(true);
        if (currentMenu != null)
        {
            currentMenu.gameObject.SetActive(false);
        }
        currentMenu = rect;
    }

    public void GO()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
