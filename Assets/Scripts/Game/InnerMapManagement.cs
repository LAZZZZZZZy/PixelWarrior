using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMapManagement : MonoBehaviour
{
    private List<Monster> monsters;
    public Vector2 size;
    public int num;
    public GameObject bullet;
    public GameObject BackGround;
    public GameObject monster;
    public GameObject HP_slider;
    public Canvas canvas;
    public GameObject item;

    public Vector2[] place;
    // Start is called before the first frame update
    void Start()
    {
        monsters = ProjectManager.Instance.monsters;
        //initial the map
        place = new Vector2[num];
        for (int i = 0; i < num; i++)
        {
            float offset = Random.Range(-0.5f, 0.5f);
            float x = Random.Range(size.x+ offset, size.x+ offset);
            float y = Random.Range(size.y+ offset, size.y+ offset);
            place[i] = new Vector2(x, y);
        }
        //fix the map
        for (int i = 0; i < 3; i++)
        {
            checkPlace();
        }
        //create obj
        foreach (Vector2 v in place)
        {
            GameObject obj = GameObject.Instantiate(monster, v, Quaternion.identity, null);
            GameObject hp = GameObject.Instantiate(HP_slider, v, Quaternion.identity, canvas.transform);
            int r = Random.Range(0, ProjectManager.Instance.monsters.Count);
            Monster m_temp = monsters[r];
            if (m_temp.Monster_type == Monster.MonsterType.Melee)
            {
                MonsterMeleeBehavior melee = obj.AddComponent<MonsterMeleeBehavior>();
                melee.monster = m_temp;
                melee.HP_slider = hp;
            }
            else if (m_temp.Monster_type == Monster.MonsterType.Range)
            {
                MonsterRangeBehavior range = obj.AddComponent<MonsterRangeBehavior>();
                range.monster = m_temp;
                range.HP_slider = hp;
                range.bullet = bullet;
            }
        }
    }

    void checkPlace()
    {
        for (int i = 0; i < num; i++)
        {
            for(int j = i;j<num;j++)
            {

                if ((place[i]-place[j]).sqrMagnitude < 1f)
                {
                    float x = Random.Range(BackGround.transform.position.x - size.x, BackGround.transform.position.x + size.x);
                    float y = Random.Range(BackGround.transform.position.y - size.y, BackGround.transform.position.y + size.y);
                    place[j] = new Vector2(x, y);
                }
            }
        }

    }

}
