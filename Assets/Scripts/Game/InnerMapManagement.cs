using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMapManagement : MonoBehaviour
{
    private List<Monster> monsters;
    public int num;
    public GameObject bullet;
    public GameObject BackGround;
    public GameObject monster;
    public GameObject HP_slider;
    public Canvas canvas;
    public GameObject item;
    public BoxCollider2D boundbox;
    public Vector3 bound_min;
    public Vector3 bound_max;

    public Vector2[] place;
    // Start is called before the first frame update
    void Start()
    {
        monsters = ProjectManager.Instance.monsters;
        //initial the map
        place = new Vector2[num];
        bound_min = boundbox.bounds.min;
        bound_max = boundbox.bounds.max;
        for (int i = 0; i < num; i++)
        {
            float x = Random.Range(bound_min.x, bound_max.x);
            float y = Random.Range(bound_min.y , bound_max.y);
            Debug.Log("x: " + x+ "y " + y);
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
            MonsterBehavior mb = obj.GetComponent<MonsterBehavior>();
            mb.HP_slider = hp;
            mb.monster = m_temp;
            mb.bullet = bullet;
        }
    }

    void checkPlace()
    {
        for (int i = 0; i < num; i++)
        {
            for(int j = i; j<num;j++)
            {

                if ((place[i]-place[j]).sqrMagnitude < 1f)
                {
                    float x = Random.Range(bound_min.x, bound_max.x);
                    float y = Random.Range(bound_min.y, bound_max.y);
                    place[j] = new Vector2(x, y);
                }
            }
        }

        for (int i = 0; i < num; i++)
        {
            if ((place[i] - new Vector2(0, 0)).sqrMagnitude < 5f)
            {
                float x = Random.Range(bound_min.x, bound_max.x);
                float y = Random.Range(bound_min.y, bound_max.y);
                place[i] = new Vector2(x, y);
            }
        }
    }

}
