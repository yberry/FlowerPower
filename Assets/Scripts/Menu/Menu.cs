using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour {

    [Tooltip("Menu principal")]
    public RectTransform mainMenu;
    [Tooltip("Menu tuto")]
    public RectTransform tutoMenu;

    private RectTransform currentMenu;

    private AudioSource source;

    private bool tuto = false;

    [Tooltip("Son de déplacement")]
    public AudioClip moveSound;
    [Tooltip("Son de sélection")]
    public AudioClip selectSound;

	// Use this for initialization
	void Start () {
        currentMenu = mainMenu;
        source = GetComponent<AudioSource>();
	}

    void Update()
    {
        if (tuto && Input.anyKeyDown)
        {
            GO();
        }
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

    public void Tuto()
    {
        source.PlayOneShot(selectSound);
        ChangeTo(tutoMenu);
        tuto = true;
    }

    void GO()
    {
        source.PlayOneShot(selectSound);
        SceneManager.LoadScene("MasterLevel3");
    }

    public void Exit()
    {
        source.PlayOneShot(selectSound);
        Application.Quit();
    }
}
