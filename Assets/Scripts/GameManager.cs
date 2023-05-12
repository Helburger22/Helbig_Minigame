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

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SharkSpawnManager").GetComponent<SharkSpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevels();
        playerController.UpdateLives();
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
}
