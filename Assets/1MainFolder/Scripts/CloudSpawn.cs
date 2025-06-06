using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Clouds
{
    public GameObject cloud;
    public GameObject Cloud => cloud;
}
public class CloudSpawn : MonoBehaviour
{
    [SerializeField] List<Clouds> clouds = new List<Clouds>();
    [SerializeField] List<GameObject> CloudSpawners = new List<GameObject>();

    private GameObject cloudObj;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float durationTime;
    void Update()
    {
        SpawnCloud();
    }

    private void SpawnCloud()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= durationTime)
        {
            int RandomSpawnPoint = UnityEngine.Random.Range(0, 3);
            int randomIndex = UnityEngine.Random.Range(0, clouds.Count);
            GameObject cloudObject = clouds[randomIndex].Cloud;
            cloudObj = Instantiate(cloudObject, CloudSpawners[RandomSpawnPoint].transform.position, Quaternion.identity);
            elapsedTime = 0.0f;
        }
    }
}
