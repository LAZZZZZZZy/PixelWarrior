using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private SkillType type;
    private float baseValue;
    private Coefficient coefficient;
    private float range;
    private string sprite;

    public Skill(SkillType type, float baseValue, Coefficient coefficient, float range, string sprite)
    {
        this.type = type;
        this.baseValue = baseValue;
        this.coefficient = coefficient;
        this.range = range;
        this.sprite = sprite;
    }

    public float BaseValue { get => baseValue; set => baseValue = value; }
    public SkillType Type { get => type; set => type = value; }
    public Coefficient Coefficient1 { get => coefficient; set => coefficient = value; }
    public float Range { get => range; set => range = value; }
    public string Sprite { get => sprite; set => sprite = value; }

    public struct Coefficient
    {
        private float strength;
        private float agility;
        private float intelligence;

        public Coefficient(float strength, float agility, float intelligence)
        {
            this.strength = strength;
            this.agility = agility;
            this.intelligence = intelligence;
        }

        public float Strength { get => strength; set => strength = value; }
        public float Agility { get => agility; set => agility = value; }
        public float Intelligence { get => intelligence; set => intelligence = value; }
    }

    public enum SkillType
    {
        Passive,Active,Trigger
    }


}
