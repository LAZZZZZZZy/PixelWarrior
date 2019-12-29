using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    private int id;
    private string name;
    private float hp;
    private float attack;
    private float defense;
    private float speed;
    private MonsterType monster_type;
    private float attackSpeed;
    private float size;//scale
    private List<Item> drops;
    private string sprite;

    public Monster(int id, string name,float hp, float attack, float defense, float speed, float size, List<Item> drops, MonsterType monster_type, float attackSpeed, string sprite)
    {
        Hp = hp;
        Attack = attack;
        Defense = defense;
        Speed = speed;
        Size = size;
        Sprite = sprite;
        Drops = drops;
        Monster_type = monster_type;
        AttackSpeed = attackSpeed;
        Id = id;
        Name = name;
    }

    public float Hp { get => hp; set => hp = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defense { get => defense; set => defense = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Size { get => size; set => size = value; }
    public string Sprite { get => sprite; set => sprite = value; }
    public List<Item> Drops { get => drops; set => drops = value; }
    public MonsterType Monster_type { get => monster_type; set => monster_type = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }

    public enum MonsterType
    {
        Melee, Range, NULL
    }

}

