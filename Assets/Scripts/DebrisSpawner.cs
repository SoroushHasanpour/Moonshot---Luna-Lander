using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public GameObject moon;
    public GameObject ship;

    private Vector3 spawnPath;
    public GameObject debrisPrefab;
    public int numberOfDebris;

    // Start is called before the first frame update
    void Start()
    {
        spawnPath = moon.transform.position - ship.transform.position;
        //Debug.Log(spawnPath);

        for(int i = 0; i < numberOfDebris; i++)
        {
            Vector3 pointOnThePath = new Vector3(Random.Range(0, spawnPath.x), Random.Range(0, spawnPath.y), Random.Range(0, spawnPath.z));
            Vector3 randomizer = new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000));
            Vector3 newPosition = pointOnThePath + randomizer;
            Instantiate(debrisPrefab, newPosition, Quaternion.identity);
        }
    }
}
