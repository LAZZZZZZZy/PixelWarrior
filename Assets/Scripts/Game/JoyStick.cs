using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoyStick : EventTrigger
{
    private GameObject _joyStick;
    private Vector3 _joyStickPosition;
    private float R;
    private GameObject _player;
    private Rigidbody2D _player_rigid;

    private float speed;//TODO test player object
    void Start()
    {
        speed = 3f;
        _player = GameObject.FindGameObjectWithTag("Player");
        _player_rigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _joyStick = this.transform.GetChild(0).gameObject;
        R = this.GetComponent<RectTransform>().sizeDelta.x / 2;

        _joyStickPosition = _joyStick.transform.position;
    }

    public override void OnDrag(PointerEventData data)
    {
        //if the drag distance not exceed the range of the sticker, it just follows the input position
        if (Vector3.Distance(data.position, this.transform.position) <= R)
        {
            _joyStick.transform.position = data.position;
        }
        else
        {
            //calculate the distance between input position and joysticker
            Vector3 dir = new Vector3(data.position.x, data.position.y, _joyStickPosition.z) - _joyStickPosition;
            //normalized the distance that it can get the drag direction
            _joyStick.transform.position = _joyStickPosition + dir.normalized * R;
            //TODO move the player here, but it can be written in player script
            _player_rigid.velocity = dir.normalized * speed;
        }

    }

    public override void OnEndDrag(PointerEventData data)
    {
        _player_rigid.velocity = new Vector2(0, 0);
        _joyStick.transform.position = _joyStickPosition;
    }
}
