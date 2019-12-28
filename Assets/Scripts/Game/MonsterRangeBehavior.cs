using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterRangeBehavior : MonoBehaviour
{
    public GameObject target;
    public Monster monster;
    private Rigidbody2D rigid;
    public GameObject HP_slider;
    private Slider hp;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private int weapon_count;
    public GameObject bullet;
    private float hp_multiple;
    // Start is called before the first frame update

    void Start()
    {
        hp_multiple = 100 / monster.Hp;
        target = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        hp = HP_slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // hp条跟随
        HP_slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        // 追踪player
        if (weapon_count > monster.AttackSpeed * 60  && Vector3.Distance(target.transform.position,transform.position) <= 5f)
        {
            float angle = Vector2.SignedAngle(new Vector2(target.transform.position.x, 0), new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y));
            Vector2 direction = target.transform.position - transform.position;
            GameObject atk = GameObject.Instantiate(bullet, (Vector2)transform.position + direction.normalized, Quaternion.Euler(0, 0, angle), null);
            atk.GetComponent<EnemyBulletMovement>().Direction = direction.normalized;
            weapon_count = 0;
        }
        weapon_count++;
        
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

    public void ReduceHp(int val)
    {
        Debug.Log(monster.Hp);
        monster.Hp -= val;
        hp.value = (monster.Hp * hp_multiple )/ 100;
       // HP_slider.GetComponentInChildren<Text>().text = monster.Hp.ToString();
    }
}
