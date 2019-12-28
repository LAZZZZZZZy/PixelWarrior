using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public float[] town_spawn_locations;
    public List<Vector2> towns;

    public Vector2 size;
    public int town_num;
    public GameObject townSprite;
    // Start is called before the first frame update
    void Awake()
    {
        town_spawn_locations = new float[] { -50f, 50f ,-16f,50f,16f,50f, -50f, 16f, -16f, 16f, 16f, 16f, -50f, -16f, -16f, -16f, 16f, -16f };
        size = new Vector2(100, 100);
        town_num = 15;
        CreateMap();
    }

    private void CreateMap()
    {
        //create towns
        for (int i = 0; i < 18; i = i+2)
        {
            float x = Random.Range(town_spawn_locations[i], town_spawn_locations[i] + 33f);
            float y = Random.Range(town_spawn_locations[i + 1], town_spawn_locations[i + 1] - 33f);
            GameObject.Instantiate(townSprite, new Vector2(x, y), Quaternion.identity, null);
            towns.Add(new Vector2(x,y));
        }
        //
    }

    public void changeScene()
    {
        SceneManager.LoadScene("InnerMap");
    }

}
