using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject SelectionMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        SelectionMenu.SetActive(true);
    }

    public void NormalCivil()
    {
        SceneManager.LoadScene("Map");
    }
}
