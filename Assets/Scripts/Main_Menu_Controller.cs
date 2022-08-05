using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Main_Menu_Controller : MonoBehaviour
{
    Canvas canvas_MainMenu;
    [SerializeField]
    NetworkManagerHUD networkManagerHUD;
    [SerializeField]
    Canvas canvas_inGame;
   
    void Start()
    {
        canvas_MainMenu = GetComponent<Canvas>(); //Assigning 'canvas_MainMenu' the desired Canvas present in the scene.
        canvas_inGame.enabled = false;
    }

    
    void Update()
    {
     
    }

    public void OnClickPlay()
    {
        canvas_MainMenu.enabled = false;
        networkManagerHUD.enabled = true;// turning on the 'NetworkHUD' when 'play' button is clicked.
        canvas_inGame.enabled=true;

    }

    public void OnClickExit() // For eiting the application.
    {
        Application.Quit();
    }
}
