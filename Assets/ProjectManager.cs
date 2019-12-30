using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectManager : MonoBehaviour
{
    private static ProjectManager instance;
    public static ProjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("ProjectManagement").GetComponent<ProjectManager>();
            }
            return instance;
        }
    }
    //csv parse
    public TextAsset fPlayer; // Reference of CSV file
    public TextAsset fMonster; // Reference of CSV file
    public List<Monster> monsters;
    public Player player;
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    private void Awake()
    {
        monsters = new List<Monster>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        parseMonster();
        parsePlayer();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseMonster()
    {
        //read monster list
        string[] records = fMonster.text.Split(lineSeperater);
        for (int i = 1; i < records.Length; i++)
        {
            string[] words = records[i].Split(fieldSeperator);
            int id = int.Parse(words[0]);
            string name = words[1];
            float hp =float.Parse(words[2]);
            float atk = float.Parse(words[3]);
            float def = float.Parse(words[4]);
            float spd = float.Parse(words[5]);
            float size = float.Parse(words[6]);
            string drop = words[7];
            Monster.MonsterType type = Monster.MonsterType.NULL;
            switch (words[8])
            {
                case "Melee":
                    type = Monster.MonsterType.Melee;
                    break;
                case "Range":
                    type = Monster.MonsterType.Range;
                    break;
            }

            float atkSpd = float.Parse(words[9]);
            string sprite = words[10];

            monsters.Add(new Monster(id, name, hp, atk, def, spd, size, null, type, atkSpd, sprite));
        }
    }

    private void parsePlayer()
    {
        player = new Player(100, 10, 2, 3, 10, 10, 10, Vector2.one, null, "player.jpg");
    }

    public void LoadScene()
    {
        SceneName.nextSceneName = "Battle";
        SceneManager.LoadScene("Loading");
    }
}
