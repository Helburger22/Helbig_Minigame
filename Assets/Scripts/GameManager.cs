using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levels;
    public float speed;
    public int sharkPast;
    public int sharksRequirement;
    public SharkSpawnManager spawner;
    bool leveling;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SharkSpawnManager").GetComponent<SharkSpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SharkCount()
    {
        sharkPast++;
        if (sharkPast > sharksRequirement&& !leveling)
        {
            StartCoroutine(LevelUp());
        }
    }

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
        
        sharksRequirement = sharkPast + sharksRequirement + levels * 5;
        speed = speed + 5;
        
        
        yield return new WaitForSeconds(2);
        leveling = false;
        spawner.StartSpawning();

    }
}
