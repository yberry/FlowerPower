using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour {

    public RectTransform mainMenu;
    public RectTransform creditsMenu;

    private RectTransform currentMenu;

    private AudioSource source;

    [Tooltip("Son de déplacement")]
    public AudioClip moveSound;
    [Tooltip("Son de sélection")]
    public AudioClip selectSound;
    [Tooltip("Scène de jeu")]
    public Scene game;

	// Use this for initialization
	void Start () {
        currentMenu = mainMenu;
        source = GetComponent<AudioSource>();
	}

    public void ChangeTo(RectTransform rect)
    {
        source.PlayOneShot(selectSound);
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

    public void Move()
    {
        source.PlayOneShot(moveSound);
    }

    public void GO()
    {
        source.PlayOneShot(selectSound);
        SceneManager.LoadScene(game.name);
    }

    public void Exit()
    {
        source.PlayOneShot(selectSound);
        Application.Quit();
    }
}
