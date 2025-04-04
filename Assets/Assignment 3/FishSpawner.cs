using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    //add fish prefab here
    public GameObject prefab;

    //spawner varables
    public float minFishAmount = 8;
    public float spawnAmount = 3;
    public float minSize = 0.5f;
    public float maxSize = 3;

    //array list to hold all the fish prefabs
    public List<GameObject> fishList;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            //spawn fish when the fish is below the minium fish amount
            if (fishList.Count < minFishAmount)
            {
                //spawn fish for the set amount
                for (int i = 0; i < spawnAmount; i++)
                {
                    //create random vector for test
                    Vector3 spawnPos = Random.insideUnitCircle * 6;
                    GameObject newSpawnedFish = Instantiate(prefab, spawnPos, Quaternion.identity);

                    //set fish random size
                    float size = Random.Range(minSize, maxSize);
                    newSpawnedFish.transform.localScale = Vector3.one * size;

                    //add the new spawned fish to the list
                    fishList.Add(newSpawnedFish);
                }
            }
            yield return null;
        }
    }
}
