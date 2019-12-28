using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float hp;
    private float attack;
    private float defense;
    private float speed;
    private float strength;
    private float agility;
    private float intelligence;
    private Vector2 size;//scale
    private List<Skill> skills;
    private string sprite;

    public Player(float hp, float attack, float defense, float speed, float strength, float agility, float intelligence, Vector2 size, List<Skill> skills, string sprite)
    {
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.strength = strength;
        this.agility = agility;
        this.intelligence = intelligence;
        this.size = size;
        this.skills = skills;
        this.sprite = sprite;
    }

    public float Hp { get => hp; set => hp = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defense { get => defense; set => defense = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }
    public float Speed { get => speed; set => speed = value; }
    public Vector2 Size { get => size; set => size = value; }
    public string Sprite { get => sprite; set => sprite = value; }
    public float Strength { get => strength; set => strength = value; }
    public float Agility { get => agility; set => agility = value; }
    public float Intelligence { get => intelligence; set => intelligence = value; }
}
