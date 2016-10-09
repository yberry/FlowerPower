using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Karma : MonoBehaviour {

    private const int maxHearts = 10;

    private static float[] amount
    {
        get
        {
            return new float[]
            {
                0f,
                0.05f,
                0.13f,
                0.2f,
                0.28f,
                0.37f,
                0.5f,
                0.58f,
                0.75f,
                0.87f,
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
