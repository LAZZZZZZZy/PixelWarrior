using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchWeaponUI : MonoBehaviour
{
    public EventTrigger et;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        et.GetComponent<JoyStickAttack>().Weapon = weapon;
    }

    public void switchWeapon()
    {
        et.GetComponent<JoyStickAttack>().Weapon = weapon;
    }
}
