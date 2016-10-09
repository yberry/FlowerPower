using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Inventory : MonoBehaviour
{
    private const int maxFlowers = 5;

    private static float[,] amount
    {
        get
        {
            return new float[,] {
                {
                    0f,
                    0.19f,
                    0.35f,
                    0.57f,
                    0.75f,
                    1f
                },
                {
                    0f,
                    0.25f,
                    0.45f,
                    0.65f,
                    0.8f,
                    1f
                }
            };
        }
    }

    private int nbFlowers = 0;

    private int idPlayer;

    private Image image;

    [Tooltip("Vitesse de remplissage")]
    public float fillSpeed = 1f;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Mathf.MoveTowards(image.fillAmount, amount[idPlayer, nbFlowers], fillSpeed * Time.deltaTime);
    }

    public void SetPlayer(int p)
    {
        idPlayer = p;
    }

    public void SetNbFlowers(int nb)
    {
        nbFlowers = nb;
    }
}
