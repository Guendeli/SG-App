using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointSystem;


public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Header("Character Levels")]
    public GameObject[] characters;
    public PathManager path;
    private int score;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        InitGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Helper method are done here

    private void InitGame()
    {
        CreateCharacter(score);
    }

    private void CreateCharacter(int index)
    {
        GameObject character = (GameObject)Instantiate(characters[index]);
        character.GetComponent<CharacterScript>().InitCharacter(path);
    }

    // public methods
    public void NextLevel()
    {
        score++;
        if(score <= characters.Length)
        {
            CreateCharacter(score);
        }
    }
}
