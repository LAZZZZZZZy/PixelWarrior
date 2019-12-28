using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private Vector2 destination;
    private bool touched;
    private bool isBattle;
    private Rigidbody2D myRigidbody;
    private Vector2 lastMove;
    public GameObject weapon;
    private int weapon_count;
    private int weapon_speed;
    public Image enterHint;
    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        isBattle = false;
        weapon_speed = 60;
        weapon_count = weapon_speed;
        speed = 3f;
        destination = new Vector2(0, 0);
        touched = false;
        myRigidbody = GetComponent<Rigidbody2D> ();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //点击移动
        isBattle = SceneManager.GetActiveScene().name.Equals("Battle");
        //鼠标点击开火
        if (isBattle)
        {
            if (Input.GetMouseButton(0) && weapon_count >= weapon_speed)
            {
                Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 length = new Vector2(1, 0);
                Vector3 dir = new Vector2(mouseInWorld.x, mouseInWorld.y) - new Vector2(transform.position.x, transform.position.y);
                float angle = Vector2.SignedAngle(length, dir);
                GameObject bullet = GameObject.Instantiate(weapon, transform.position + dir.normalized, Quaternion.Euler(0, 0, angle), null);
                bullet.GetComponent<BulletMovement>().Direction = dir;
                weapon_count = 0;
            }
            weapon_count++;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Debug.Log(isBattle);
                
                bool isOnBtn = EventSystem.current.IsPointerOverGameObject();
                if (!isOnBtn && !isBattle)
                {
                    destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touched = true;
                }
            }

            if (!isBattle && touched == true && Mathf.Abs(((Vector2)transform.position - destination).sqrMagnitude) > 0.001f)
            {
                //Debug.Log("??? "+ transform.position+ "Des "+destination);
                //transform.position = new Vector3(3, 3, 0);
                transform.position = Vector2.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        //战斗场景
        if (isBattle)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * movespeed * Time.deltaTime,0f,0f));
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, myRigidbody.velocity.y);
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * movespeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * Speed);
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("house"))
        {
           // Debug.Log("coll1");

            enterHint.transform.position = Camera.main.WorldToScreenPoint(new Vector3(collision.transform.position.x, collision.gameObject.GetComponent<BoxCollider2D>().bounds.max.y + 0.8f, collision.transform.position.z));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("house"))
        {
           // Debug.Log("coll1");
            
            enterHint.transform.position = Camera.main.WorldToScreenPoint(new Vector3(collision.transform.position.x, collision.gameObject.GetComponent<BoxCollider2D>().bounds.max.y + 0.8f, collision.transform.position.z));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("house"))
        {
            enterHint.transform.position = new Vector3(-2200, 200, 0);
        }
    }

}
