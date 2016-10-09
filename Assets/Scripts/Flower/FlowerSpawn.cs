using UnityEngine;
using System.Collections;

public class FlowerSpawn : MonoBehaviour {

    [Tooltip("Temps minimum séparant l'apparition de 2 fleurs (en sec)")]
    [Range(1f, 10f)]
    public float freqSpawnMin = 1f;

    [Tooltip("Temps maximum séparant l'apparition de 2 fleurs (en sec)")]
    [Range(1f, 10f)]
    public float freqSpawnMax = 10f;

    [Tooltip("Points de spawn des fleurs")]
    public FlowerSpawnPoint[] spawnPoints;

    [Tooltip("Préfabs de fleurs")]
    public GameObject[] prefabsFlower;

    [Tooltip("Sons de spawn")]
    public AudioClip[] spawnSounds;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnLoop());
	}

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float rand = freqSpawnMin < freqSpawnMax ? Random.Range(freqSpawnMin, freqSpawnMax) : Random.Range(freqSpawnMax, freqSpawnMin);
            yield return new WaitForSeconds(rand);

            foreach (FlowerSpawnPoint point in spawnPoints)
            {
                if (!point.HaveFlower)
                {
                    Spawn();
                    break;
                }
            }
        }
    }

    void Spawn()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        while (spawnPoints[rand].HaveFlower)
        {
            rand = Random.Range(0, spawnPoints.Length);
        }
        spawnPoints[rand].Spawn(prefabsFlower[Random.Range(0, prefabsFlower.Length)], spawnSounds[Random.Range(0, spawnSounds.Length)]);
    }
}
