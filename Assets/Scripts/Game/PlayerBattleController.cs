using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBattleController : MonoBehaviour
{
    //obj class
    public Player player;
    //battle
    private int weapon_speed;
    private Vector2 weapon_direction;
    private float weapon_angle;
    private int weapon_count;
    private bool fired;
    public GameObject weapon;
    public float speed;
    //hp
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public Slider HP_slider;
    private float hp_multiple;
    //falsh
    bool isInvincible = false;
    private SpriteRenderer renderer;
    private float gapTime;
    public float flash_duration;
    //setting
    private bool isGamePause = true;
    private float Timer;

    public int Weapon_speed { get => weapon_speed; set => weapon_speed = value; }
    public Vector2 Weapon_direction { get => weapon_direction; set => weapon_direction = value; }
    public float Weapon_angle { get => weapon_angle; set => weapon_angle = value; }
    public bool Fired { get => fired; set => fired = value; }

    // Start is called before the first frame update
    void Start()
    {
        //initialize the player
        player = ProjectManager.Instance.player;
       // Resources.Load<Sprite>("")
        fired = false;
        weapon_speed = 20;
        weapon_count = weapon_speed;
        //flash
        renderer = GetComponent<SpriteRenderer>();
        //hp
        hp_multiple = 100f / player.Hp;
        //DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded && isGamePause)
        {
            Pause();
        }
        else
        {
            Resume();
        }

        if (player.Hp == 0)
        {
            //Debug.Log("died");
        }

        if (isInvincible)
        {
            gapTime += Time.deltaTime;
            if (gapTime < flash_duration)
            {
                float remainder = gapTime % 0.3f;
                renderer.enabled = remainder >= 0.15f;
            }
            else
            {
                renderer.enabled = true;
                isInvincible = false;
            }
        }
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

    public bool ReduceHp(float val)
    {
        // Debug.Log(HP_slider.value);
        if (isInvincible == false)
        {
            player.Hp -= val;
            HP_slider.value = (player.Hp * hp_multiple) / 100f;
            HP_slider.GetComponentInChildren<Text>().text = player.Hp.ToString();
            isInvincible = true;
            gapTime = 0;
            return true;
        }

        return false;
    }

    public void Pause()
    {
        Timer += 0.1f;
        Time.timeScale = 0f;
        isGamePause = true;
        Debug.Log(Timer);
        if (Timer >= 3f)
        {
            isGamePause = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isGamePause = false;
    }
}
