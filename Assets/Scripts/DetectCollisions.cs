using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public int scoreValue;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Animal")
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Border")
            {
                gameManager.damageTaken();
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Food")
            {
                if (gameObject.GetComponent<HealthBar>().damageTaken() == 0)
                {
                    gameManager.addScore(scoreValue);
                    Destroy(gameObject);
                }
                other.gameObject.SetActive(false);
            }
        }
    }
}
