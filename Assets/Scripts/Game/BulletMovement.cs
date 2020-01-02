using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private Vector2 direction;

    public float Speed { get => speed; set => speed = value; }
    public Vector2 Direction { get => direction; set => direction = value; }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // move forward
        GetComponent<Rigidbody2D>().velocity = Direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            Debug.Log("des");
            collision.gameObject.GetComponent<MonsterBehavior>().ReduceHp(10);
            Destroy(this.gameObject);
        }
    }
}
