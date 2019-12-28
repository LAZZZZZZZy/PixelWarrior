using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMapManagement : MonoBehaviour
{
    public Vector2 size;
    public int num;
    public GameObject BackGround;
    public GameObject monster;
    public GameObject HP_slider;
    public Canvas canvas;
    public GameObject item;

    public Vector2[] place;
    // Start is called before the first frame update
    void Start()
    {
        //initial the map
        place = new Vector2[num];
        for (int i = 0; i < num; i++)
        {
            float offset = Random.Range(-0.5f, 0.5f);
            float x = Random.Range(BackGround.transform.position.x - size.x+ offset, BackGround.transform.position.x + size.x+ offset);
            float y = Random.Range(BackGround.transform.position.y - size.y+ offset, BackGround.transform.position.y + size.y+ offset);
            place[i] = new Vector2(x, y);
        }
        //fix the map
        for (int i = 0; i < 3; i++)
        {
            checkPlace();
        }
        //create obj
        bool swi = false;
        foreach (Vector2 v in place)
        {
            if (swi)
            {
                GameObject m = GameObject.Instantiate(monster, v, Quaternion.identity, null);
                GameObject hp = GameObject.Instantiate(HP_slider, v, Quaternion.identity, canvas.transform);
                m.GetComponent<MonsterBehavior>().HP_slider = hp;
                swi = false;
            }
                
            else
            {
                GameObject.Instantiate(item, v, Quaternion.identity, null);
                swi = true;
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
