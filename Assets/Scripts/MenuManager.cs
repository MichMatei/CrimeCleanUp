using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject start_menu;
    public GameObject pause_menu;
    public GameObject in_game_UI;
    public MouseLook mouse_look_scrypt;
    public PlayerMovement player_movement_scrypt;

    bool pause_active = false;

    // Start is called before the first frame update
    void Start()
    {
        start_menu.SetActive(true);
        pause_menu.SetActive(false);
        in_game_UI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        mouse_look_scrypt.enabled = false;
        player_movement_scrypt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && start_menu.activeSelf == false)
        {
            Pause_ON_and_OFF();
        }
    }

    public void OnStartPressed()
    {
        start_menu.SetActive(false);
        in_game_UI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouse_look_scrypt.enabled = true;
        player_movement_scrypt.enabled = true;
    }

    public void OnExitPressed()
    {
        if (pause_active)
        {
            pause_active = false;
            pause_menu.SetActive(false);
            start_menu.SetActive(true);
            mouse_look_scrypt.enabled = false;
            player_movement_scrypt.enabled = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void Pause_ON_and_OFF()
    {
        if (pause_active)
        {
            pause_active = false;
            pause_menu.SetActive(false);
            in_game_UI.SetActive(true); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouse_look_scrypt.enabled = true;
            player_movement_scrypt.enabled = true;
        }
        else
        {
            pause_active = true;
            pause_menu.SetActive(true);
            in_game_UI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouse_look_scrypt.enabled = false;
            player_movement_scrypt.enabled = false;
        }
    }
}
