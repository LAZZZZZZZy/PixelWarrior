using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIController : MonoBehaviour
{

    //menu setting
    private bool isGamePause = true;
    private float Timer;
    public CanvasGroup menu;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.transform.GetSiblingIndex() != menu.transform.parent.childCount)
        {
            menu.transform.SetSiblingIndex(menu.transform.parent.childCount - 1);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void Option()
    {

    }

    private void Resume()
    {
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void showMenu()
    {
        Pause();
        menu.alpha = 1;
        menu.blocksRaycasts = true;
        menu.interactable = true;
    }

    public void hideMenu()
    {
        Resume();
        menu.alpha = 0;
        menu.blocksRaycasts = false;
        menu.interactable = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
