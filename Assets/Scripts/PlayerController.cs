using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 8.0f;
    public float xRange = 10;
    public float zRange = 5;
    public int health = 3;
    public int maxHealth = 3;
    public Renderer hitscan;
    public TextMeshProUGUI healthText;
    private GameManager gameManager;
    public bool powerup;
    public Color colorG;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
       
        if (other.CompareTag("shark"))
        {
            
            if (!powerup)
            {
                //controls health
                //health -= 1;
                UpdateLives();
                StartCoroutine(HealthBlink());
                Destroy(other.gameObject);
                if (health < 1)
                {


                    Destroy(gameObject);

                    if (gameManager != null)
                    {
                        gameManager.GameOver();
                    }


                }
            }
            else
            {
                other.gameObject.GetComponentInChildren<Renderer>().material.color = colorG;
                
            }
        }
        else if (other.CompareTag("powerUp"))
        {
            if (powerup == false)
            {
                StartCoroutine(PowerBlink());
            }
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

    IEnumerator PowerBlink()
    {
        //Change to new material
        hitscan.material.color = colorG;

        powerup = true;
        speed += 5;
        //wait for seconds
        yield return new WaitForSeconds(4.0f);

        //change back to og material
        hitscan.material.color = Color.white;
        powerup = false;
        speed -= 5;
    }

    public void UpdateLives()
    {
        health -= 1;
        healthText.text = "Health: " + health;
    }

    public void ResetLives()
    {
        health = maxHealth;
    }

}
