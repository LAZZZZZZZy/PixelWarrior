using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Awake()
    {
        Item it = new Item(new Vector2(3,2), 0, 0, "qiang");
        item.GetComponent<ItemUI>().Item = it;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
