using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
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
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerBattleController>().ReduceHp(10);
            Destroy(this.gameObject);
        }
    }
}
