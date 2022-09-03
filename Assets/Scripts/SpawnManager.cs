using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float[,] animalsDirections = new float[4, 6]; //[direction][xRangeMin/xRangeMax/zRangeMin/zRangeMax/rotation/HProtation]
    private float spawnRangeX = 20;
    private float spawnRangeZ = 8;
    private float spawnPosX = 22;
    private float spawnPosZ = 13;

    private float minSpawnInterval = 0.5f;
    public float spawnInterval = 2f;
    public int[] spawnChance = new int[3]; //[cow/dog/horse]
    private int accumulatedProbability; // Sum of all values in spawnChance


    // Start is called before the first frame update
    void Start()
    {
        SetAcumulatedProbability();

        // Animals coming from top
        animalsDirections[0, 0] = -spawnRangeX;
        animalsDirections[0, 1] = spawnRangeX;
        animalsDirections[0, 2] = spawnPosZ;
        animalsDirections[0, 3] = spawnPosZ;
        animalsDirections[0, 4] = 180;
        animalsDirections[0, 5] = 0;
        // Animals coming from bottom
        animalsDirections[1, 0] = -spawnRangeX;
        animalsDirections[1, 1] = spawnRangeX;
        animalsDirections[1, 2] = -spawnPosZ;
        animalsDirections[1, 3] = -spawnPosZ;
        animalsDirections[1, 4] = 0;
        animalsDirections[1, 5] = 0;
        // Animals coming from right
        animalsDirections[2, 0] = spawnPosX;
        animalsDirections[2, 1] = spawnPosX;
        animalsDirections[2, 2] = -spawnRangeZ;
        animalsDirections[2, 3] = spawnRangeZ;
        animalsDirections[2, 4] = 270;
        animalsDirections[2, 5] = 90;
        // Animals coming from left
        animalsDirections[3, 0] = -spawnPosX;
        animalsDirections[3, 1] = -spawnPosX;
        animalsDirections[3, 2] = -spawnRangeZ;
        animalsDirections[3, 3] = spawnRangeZ;
        animalsDirections[3, 4] = 90;
        animalsDirections[3, 5] = 270;

        StartCoroutine(SpawnRandomAnimal());
    }

    IEnumerator SpawnRandomAnimal()
    {
        yield return new WaitForSeconds(spawnInterval);

        int animalDirectionIndex = Random.Range(0, 4);
        float xPos = Random.Range(animalsDirections[animalDirectionIndex, 0], animalsDirections[animalDirectionIndex, 1]);
        float zPos = Random.Range(animalsDirections[animalDirectionIndex, 2], animalsDirections[animalDirectionIndex, 3]);
        Vector3 spawnPos = new Vector3(xPos, 0, zPos);
        int animalIndex = GetRandomAnimalIndex();
        GameObject animal = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        animal.transform.Rotate(0, animalsDirections[animalDirectionIndex, 4], 0);

        // Turn Health bar
        Transform child = animal.transform.GetChild(1);
        child.transform.eulerAngles = new Vector3(90, animalsDirections[animalDirectionIndex, 5], 0);

        StartCoroutine(SpawnRandomAnimal());
    }

    private int GetRandomAnimalIndex()
    {
        int randomNumber = Random.Range(0, accumulatedProbability);
        int addedChance = 0;
        for (int i = 0; i < spawnChance.Length; i++)
        {
            if (randomNumber < spawnChance[i] + addedChance)
            {
                return i;
            }
            addedChance += spawnChance[i];
        }

        return -1; // Shouldn't reach this return
    }

    public void SetAcumulatedProbability()
    {
        int newProbability = 0;
        for (int i = 0; i < spawnChance.Length; i++)
        {
            newProbability += spawnChance[i];
        }
        accumulatedProbability = newProbability;
    }

    public void IncreaseSpawnChances()
    {
        spawnChance[0] += 1;
        spawnChance[1] += 3;
        spawnChance[2] += 5;
        SetAcumulatedProbability();
    }

    public void IncreaseAnimalsSpeed()
    {
        for(int i = 0; i < animalPrefabs.Length; i++)
        {
            animalPrefabs[i].GetComponent<MoveForward>().speed += 1;
        }
    }

    public void DecreaseSpawnInterval()
    {
        if(spawnInterval > minSpawnInterval)
        {
            spawnInterval -= 0.1f;
        }
    }
}
