using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkForward : MonoBehaviour
{
    //controls the game objects speed
    public float speedMod = 15.0f;
    public GameManager gameManager;
   
    //boundry for game object before despawns
    private float topBound = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //moves game object forward twoards the player
        transform.Translate(Vector3.forward * Time.deltaTime * gameManager.speed* speedMod);

        //destroys game object once reaches topbound variable
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
            gameManager.SharkCount();
        }
    }
}
