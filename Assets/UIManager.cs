using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup PlayerUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hidePlayerUI()
    {
        if (PlayerUI.alpha == 0)
        {
            PlayerUI.alpha = 1;
            PlayerUI.blocksRaycasts = true;
        }
        else
        {
            PlayerUI.alpha = 0;
            PlayerUI.blocksRaycasts = false;
        }
            
    }
}
