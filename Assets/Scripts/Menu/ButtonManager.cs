using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    private bool mapIsOpen;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OpenPauseMenu();
        }
    }

    void OpenPauseMenu()
    {
        Debug.Log("Pause menu opened");
        // Open pause menu
    }

    /*void OnGUI()
    {
        Listens to any key press, logic can be used for anything in future
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None)
        {
            // Logic
        }
    }*/

}
