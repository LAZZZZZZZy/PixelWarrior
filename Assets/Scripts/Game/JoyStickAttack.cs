using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoyStickAttack : EventTrigger
{
    private GameObject joyStick;
    private Vector3 joyStickPosition;
    private float R;
    private GameObject weapon;
    private GameObject player;
    private Rigidbody2D player_rigid;
    private PlayerBattleController playerBattle;

    private float speed;//TODO test player object

    public GameObject Weapon { get => weapon; set => weapon = value; }

    void Start()
    {
        speed = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
        player_rigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        joyStick = this.transform.GetChild(0).gameObject;
        playerBattle = player.GetComponent<PlayerBattleController>();
        R = this.GetComponent<RectTransform>().sizeDelta.x / 2;

        joyStickPosition = joyStick.transform.position;
    }

    public override void OnDrag(PointerEventData data)
    {
        //if the drag distance not exceed the range of the sticker, it just follows the input position
        if (Vector3.Distance(data.position, this.transform.position) <= R)
        {
            joyStick.transform.position = data.position;
        }
        else
        {
            //calculate the distance between input position and joysticker
            Vector3 dir = new Vector3(data.position.x, data.position.y, joyStickPosition.z) - joyStickPosition;
            //normalized the distance that it can get the drag direction
            joyStick.transform.position = joyStickPosition + dir.normalized * R;
            //TODO move the player here, but it can be written in player script
            float angle = Vector2.SignedAngle(new Vector2(R, 0), new Vector2(data.position.x, data.position.y) - new Vector2(joyStickPosition.x, joyStickPosition.y));
            //give the joystick angle to the weapon
            playerBattle.Weapon_angle = angle;
            playerBattle.Weapon_direction = dir;
            playerBattle.Fired = true;
        }

    }

    public override void OnEndDrag(PointerEventData data)
    {
        joyStick.transform.position = joyStickPosition;
        playerBattle.Fired = false;
    }
   
}
