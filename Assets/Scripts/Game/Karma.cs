using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Karma : MonoBehaviour {

    private const int maxHearts = 7;

    private static float[] amount
    {
        get
        {
            return new float[]
            {
                0f,
                0.1f,
                0.2f,
                0.35f,
                0.5f,
                0.67f,
                0.85f,
                1f
            };
        }
    }

    public bool IsMax
    {
        get
        {
            return nbHearts == maxHearts;
        }
    }

    private int nbHearts = 0;

    private Image image;

    [Tooltip("Vitesse de remplissage")]
    public float fillSpeed = 1f;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        image.fillAmount = Mathf.MoveTowards(image.fillAmount, amount[nbHearts], fillSpeed * Time.deltaTime);
	}

    public void AddHeart()
    {
        nbHearts++;
    }
}
