using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public int scene;
    public float teleportX;
    public float teleportY;

    public Interaction interaction;
    private GameObject player;
    private GameObject camera;
    private GameObject localPlayer;

    void Start()
    {
        player = null;
    }

    void Update()
    {
        SceneLoader();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            interaction.InteractOn();
            player = col.gameObject;
        }
    }

    private void SceneLoader()
    {
        if (player != null && Input.GetKey(KeyCode.F))
        {
            player.transform.position = new Vector3(teleportX, teleportY, 0F);
            DontDestroyOnLoad(player);
            player = null;
            SceneManager.LoadScene(scene);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Debug.Log("Lost player");
            player = null;
            interaction.InteractOff();
        }
    }
}
