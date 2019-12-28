﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMeleeBehavior : MonoBehaviour
{
    public GameObject target;
    public Monster monster;
    private Rigidbody2D rigid;
    public GameObject HP_slider;
    private Slider hp;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private float hp_multiple;
    // Start is called before the first frame update

    void Start()
    {
        hp_multiple = 100 / monster.Hp;
        target = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        hp = HP_slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // hp条跟随
        HP_slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        // 追踪player
        if (Vector2.Distance(transform.position, target.transform.position) < 3.5f)
        {
            rigid.velocity = new Vector2(0, 0);
            // rigid.velocity = (target.transform.position-transform.position ) * speed;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, monster.Speed * Time.deltaTime);
        }
        else
            rigid.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rigid.velocity = new Vector2(0, 0);
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
        Destroy(HP_slider);
    }

    public void ReduceHp(int val)
    {
        Debug.Log(monster.Hp);
        Debug.Log(hp.value);
        monster.Hp -= val;
        hp.value = (monster.Hp * hp_multiple) / 100;

        if (monster.Hp == 0)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        // HP_slider.GetComponentInChildren<Text>().text = monster.Hp.ToString();
    }
}
