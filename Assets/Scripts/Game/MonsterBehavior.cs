using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rigid;
    public GameObject HP_slider;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // hp条跟随
        HP_slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        // 追踪player
        if (Vector2.Distance(transform.position, target.transform.position) < 3f)
        {
            rigid.velocity = new Vector2(0, 0);
            // rigid.velocity = (target.transform.position-transform.position ) * speed;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
            rigid.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rigid.velocity = new Vector2(0, 0);
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
        Destroy(HP_slider);
    }
}
