using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int levels;
    public float speed;
    public int sharkPast;
    public int sharksRequirement;
    public SharkSpawnManager spawner;
    bool leveling;
    private PlayerController playerController;
    public TextMeshProUGUI levelText;
    public GameObject gameOverScreen;
    public bool isGameActive;
    public GameObject titleScreen;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
        
        spawner = GameObject.Find("SharkSpawnManager").GetComponent<SharkSpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        //gameOverScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive == false)
        {
            spawner.StopSpawning();
        }
        
        UpdateLevels();

        if (Input.anyKey && !isGameActive)
        {
            StartGame();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // or if (Input.GetButtonUp("Cancel")) {
            Application.Quit();
        }
        //if (playerController != null)
        //{
        //playerController.UpdateLives();

        //}
    }

    //controls when 
    public void SharkCount()
    {
        sharkPast++;
        if (sharkPast > sharksRequirement&& !leveling)
        {
            StartCoroutine(LevelUp());
        }
    }

    //controls levels up, and updates shark changes
     IEnumerator LevelUp()
    {
        leveling = true;
        spawner.StopSpawning();
        levels++;
        Debug.Log("level " + levels);
        SharkForward[] sharks = GameObject.FindObjectsOfType<SharkForward>();
        for (int i = 0; i < sharks.Length; i++)
        {
            sharks[i].Deletion();
        }
        playerController.UpdateLives();

        playerController.ResetLives();
        
        sharksRequirement = sharkPast + sharksRequirement + levels * 5;
        speed = speed + 5;
        
        
        yield return new WaitForSeconds(2);
        leveling = false;
        spawner.StartSpawning();

    }

    //updates levels UI
    public void UpdateLevels()
    {
        levelText.text = "Level: " + levels;
    }

    //sets game over canvas to active
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        spawner.StopSpawning();
        
        
    }
    //resets to the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        spawner.StartSpawning();
        titleScreen.gameObject.SetActive(false);
    }
}
