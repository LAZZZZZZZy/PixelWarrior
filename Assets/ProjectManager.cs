using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectManager : MonoBehaviour
{
    public List<Monster> monsters;
    public Player player;

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

    private void Awake()
    {
        monsters = new List<Monster>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //TODO: Test, change to read data file
        player = new Player(100,10,2,3,10,10,10,Vector2.one,null,"player.jpg");
        Monster m1 = new Monster(50, 10, 0, 3, Vector2.one, "monsterMelee.jpg", null, Monster.MonsterType.Melee, 1);
        Monster m2 = new Monster(50, 5, 0, 2, Vector2.one, "monsterRange.jpg", null, Monster.MonsterType.Range, 1);
        monsters.Add(m1);
        monsters.Add(m2);

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Battle");
    }
}
