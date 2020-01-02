using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    public GameObject target;
    public Monster monster;
    private Rigidbody2D rigid;
    public GameObject HP_slider;
    private Slider hp;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private float hp_multiple;
    private float weapon_count;
    private float timer = 0;
    public GameObject bullet;
    private SpriteRenderer render;
    private bool isHit = false;
    // Start is called before the first frame update

    void Start()
    {
        hp_multiple = 100f / monster.Hp;
        target = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        hp = HP_slider.GetComponent<Slider>();
        //load image
        render = GetComponent<SpriteRenderer>();
        Debug.Log(monster.Sprite.ToString());
        Sprite spr = Resources.Load<Sprite>(monster.Sprite.Trim());
        render.sprite = spr;
    }

    // Update is called once per frame
    void Update()
    {
        // hp条跟随
        HP_slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        if (monster.Monster_type == Monster.MonsterType.Melee)
        {
            MeleeBehavior();
        }
        else if(monster.Monster_type == Monster.MonsterType.Range){
            RangeBehavior();
        }
    }

    private void RangeBehavior()
    {
        // 追踪player
        if (weapon_count > monster.AttackSpeed  && Vector3.Distance(target.transform.position, transform.position) <= 5f)
        {
            float angle = Vector2.SignedAngle(new Vector2(target.transform.position.x, 0), new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y));
            Vector2 direction = target.transform.position - transform.position;
            GameObject atk = GameObject.Instantiate(bullet, (Vector2)transform.position + direction.normalized, Quaternion.Euler(0, 0, angle), null);
            atk.GetComponent<EnemyBulletMovement>().Direction = direction.normalized;
            atk.GetComponent<EnemyBulletMovement>().damage = monster.Attack;
            weapon_count = 0;
        }
        weapon_count += Time.deltaTime;
    }

    private void MeleeBehavior()
    {
        // 追踪player
        if (Vector2.Distance(transform.position, target.transform.position) < 3.5f && !isHit)
        {
            rigid.velocity = new Vector2(0, 0);
            // rigid.velocity = (target.transform.position-transform.position ) * speed;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, monster.Speed * Time.deltaTime);
        }
        else
            rigid.velocity = new Vector2(0, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            bool hitted = collision.gameObject.GetComponent<PlayerBattleController>().ReduceHp(monster.Attack);
            if (hitted)
            {
                Debug.Log("hit player");
                Debug.Log("x:" + collision.contacts[0].normal.x + "y: "+ collision.contacts[0].normal.y);
                if (collision.contacts[0].normal.y >= -1 && collision.contacts[0].normal.y < 0)//从上方碰撞
                {
                    Debug.Log("hit top");
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1.4f, transform.position.z);
                }
                else if (collision.contacts[0].normal.y <= 1 && collision.contacts[0].normal.y > 0)//从下方碰撞
                {
                    Debug.Log("hit bot");
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
                }
                else if (collision.contacts[0].normal.x >= -1 && collision.contacts[0].normal.x < 0)//左边碰撞
                {
                    Debug.Log("hit left");
                    transform.position = new Vector3(transform.position.x - 1.4f, transform.position.y, transform.position.z);
                }
                else if (collision.contacts[0].normal.x <= 1 && collision.contacts[0].normal.x > 0)//右边碰撞
                {
                    Debug.Log("hit right");
                    transform.position = new Vector3(transform.position.x + 1.4f, transform.position.y, transform.position.z);
                }
                rigid.velocity = Vector2.zero;
                isHit = true;
            }
            else
            {
                rigid.velocity = new Vector2(rigid.velocity.x / 10, rigid.velocity.y / 10);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("exit player");
            isHit = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
        Destroy(HP_slider);
    }

    public void ReduceHp(int val)
    {
        Debug.Log(monster.Hp);
        Debug.Log(hp.value);
        monster.Hp -= val;
        hp.value = (monster.Hp * hp_multiple) / 100;

        if (monster.Hp <= 0)
        {
            Destroy(this);
        }
        // HP_slider.GetComponentInChildren<Text>().text = monster.Hp.ToString();
    }
}
