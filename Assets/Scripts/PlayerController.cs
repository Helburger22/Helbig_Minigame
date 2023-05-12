using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 10;
    public float zRange = 5;
    public int health = 3;
    public Renderer hitscan;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //keeps the player in bound on the x axis
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        //moves the player object left or right
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //controls health
        health -= 1;
        StartCoroutine(HealthBlink());
        if (health < 1) 
        {
            

            
          Destroy(gameObject);

            
        }
        

    }
    //changes color of player during a detection of collision
    IEnumerator HealthBlink()
    {
        //Change to new material
        hitscan.material.color = Color.red;
        //wait for seconds
        yield return new WaitForSeconds(0.5f);

        //change back to og material
        hitscan.material.color = Color.white;
    }

    public void UpdateLives()
    {
        healthText.text = "Health: " + health;
        //if (health < 1)
        {
            //gameover.setactive(
        }
    }

}
