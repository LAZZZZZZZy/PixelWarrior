using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    private int weapon_speed;
    private Vector2 weapon_direction;
    private float weapon_angle;
    private int weapon_count;
    private bool fired;
    public GameObject weapon;

    public int Weapon_speed { get => weapon_speed; set => weapon_speed = value; }
    public Vector2 Weapon_direction { get => weapon_direction; set => weapon_direction = value; }
    public float Weapon_angle { get => weapon_angle; set => weapon_angle = value; }
    public bool Fired { get => fired; set => fired = value; }

    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        weapon_speed = 20;
        weapon_count = weapon_speed;

        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fired)
        {
            if (weapon_count >= weapon_speed)
            {
                GameObject atk = GameObject.Instantiate(weapon, (Vector2)transform.position + weapon_direction.normalized, Quaternion.Euler(0, 0, weapon_angle), null);
                atk.GetComponent < BulletMovement>().Direction = weapon_direction.normalized;
                weapon_count = 0;
            }
        }

        if (weapon_count < weapon_speed)
            weapon_count++;
    }
}
