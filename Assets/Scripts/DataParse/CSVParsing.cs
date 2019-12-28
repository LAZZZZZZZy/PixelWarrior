using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParsing : MonoBehaviour
{
    public TextAsset fequip; // Reference of CSV file
    public TextAsset faffix; // Reference of CSV file
    private List<Affix> affixes;
    private List<Equipment> equipment;
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    public List<Affix> Affixes { get => affixes; set => affixes = value; }
    public List<Equipment> Equipment { get => equipment; set => equipment = value; }

    void Start()
    {
        affixes = new List<Affix>();
        equipment = new List<Equipment>();
    }
    // Read data from CSV file
    public void readData()
    {
        //read Affix list
        string[] records = faffix.text.Split(lineSeperater);
        for (int i = 1; i < records.Length; i++)
        {
            string[] words = records[i].Split(fieldSeperator);
            int id = int.Parse(words[0]);
            string name = words[1];
            Affix.AffixPos pos;
            switch (words[2])
            {
                case "Surffix":
                    pos = Affix.AffixPos.Surffix;
                    break;
                case "Perffix":
                    pos = Affix.AffixPos.Perffix;
                    break;
                default:
                    pos = Affix.AffixPos.NULL;
                    break;
            }

            Affix.Attribute attribute;
            //affix attribute
            switch (words[3])
            {
                case "PhysicalAttack":
                    attribute = Affix.Attribute.PhysicalAttack;
                    break;
                case "MagicAttack":
                    attribute = Affix.Attribute.MagicAttack;
                    break;
                case "PhysicalArmor":
                    attribute = Affix.Attribute.PhysicalArmor;
                    break;
                case "MagicArmor":
                    attribute = Affix.Attribute.MagicArmor;
                    break;
                case "Evasion":
                    attribute = Affix.Attribute.Evasion;
                    break;
                default:
                    attribute = Affix.Attribute.NULL;
                    break;
            }
            int min = int.Parse(words[4]);
            int max = int.Parse(words[5]);
            Affixes.Add(new Affix(id, name, pos, attribute, min, max));
        }

        //read Equipment lists
        records = fequip.text.Split(lineSeperater);
        for (int i = 1; i < records.Length; i++)
        {
            string[] words = records[i].Split(fieldSeperator);
            int id = int.Parse(words[0]);
            string name = words[1];

            Equipment.Type type;
            switch (words[2])
            {
                case "Weapon":
                    type = global::Equipment.Type.Weapon;
                    break;
                case "Offweapon":
                    type = global::Equipment.Type.OffWeapon;
                    break;
                case "boot":
                    type = global::Equipment.Type.Boot;
                    break;
                case "head":
                    type = global::Equipment.Type.Head;
                    break;
                case "Body":
                    type = global::Equipment.Type.Body;
                    break;
                case "Pants":
                    type = global::Equipment.Type.Pants;
                    break;
                case "Ring":
                    type = global::Equipment.Type.Ring;
                    break;
                case "Necklet":
                    type = global::Equipment.Type.Necklet;
                    break;
                default:
                    type = global::Equipment.Type.NULL;
                    break;
            }

            int rare = int.Parse(words[3]);
            Affix surffix = Affixes[int.Parse(words[4])];
            Affix perffix = Affixes[int.Parse(words[5])];

            Equipment.ValueType valueType;
            //value type
            switch (words[6])
            {
                case "Physical":
                    valueType = global::Equipment.ValueType.Physical;
                    break;
                case "Magic":
                    valueType = global::Equipment.ValueType.Magic;
                    break;
                case "Evasion":
                    valueType = global::Equipment.ValueType.Evasion;
                    break;
                case "Armor":
                    valueType = global::Equipment.ValueType.Armor;
                    break;
                default:
                    valueType = global::Equipment.ValueType.NULL;
                    break;
            }
            int min = int.Parse(words[7]);
            int max = int.Parse(words[8]);
            Equipment.Add(new Equipment(id, name, type, rare, surffix, perffix, valueType, min, max));
        }

        Debug();
    }

    public void Debug()
    {
        foreach (Affix affix in affixes)
        {
            print(affix.ToString());
        }

        foreach (Equipment eq in equipment)
        {
            print(eq.ToString());
        }
    }
}
