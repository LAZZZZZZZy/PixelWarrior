using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    private float hp;
    private float attack;
    private float defense;
    private float speed;
    private Vector2 size;//scale
    private List<Skill> skills;
    private List<Item> drops;
    private string sprite;
    private string party;

    public float Hp { get => hp; set => hp = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defense { get => defense; set => defense = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }
    public float Speed { get => speed; set => speed = value; }
    public Vector2 Size { get => size; set => size = value; }
    public string Sprite { get => sprite; set => sprite = value; }
    public List<Item> Drops { get => drops; set => drops = value; }

    public Monster(float hp, float attack, float defense, float speed, Vector2 size, List<Skill> skills, string sprite, List<Item> drops)
    {
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.size = size;
        this.skills = skills;
        this.drops = drops;
        this.sprite = sprite;
    }


}

