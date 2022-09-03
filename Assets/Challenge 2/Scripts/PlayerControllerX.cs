using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool dogSent = false;
    private float dogDelay = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog and set delay
        if (Input.GetKeyDown(KeyCode.Space) && !dogSent)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            dogSent = true;
            Invoke("DogReturned", dogDelay);
        }
    }

    // Reset delay of sending dog
    void DogReturned()
    {
        dogSent = false;
    }
}
