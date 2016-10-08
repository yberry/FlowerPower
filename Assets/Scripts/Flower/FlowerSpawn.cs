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

    [Tooltip("Préfab de fleur")]
    public GameObject prefabFlower;

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
            bool occupe = true;
            foreach (FlowerSpawnPoint point in spawnPoints)
            {
                occupe &= point.HaveFlower;
            }
            if (!occupe)
            {
                Spawn();
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
        spawnPoints[rand].Spawn(prefabFlower);
    }
}
