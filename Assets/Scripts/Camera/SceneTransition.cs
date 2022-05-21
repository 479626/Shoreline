using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int scene;
    public Animator anim;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Interaction interaction;

    private void MovePlayer()
    {
        SoundManager.instance.DoorSound();
        playerStorage.initialValue = playerPos;
        SceneManager.LoadScene(scene);
    }

    #region Collision Detection

    public void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || col.isTrigger) return;
        
        interaction.InteractOn();
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            anim.SetBool("Open", true);
        }
        if (Input.GetKey(KeyCode.F))
        {
            MovePlayer();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 8)
            {
                anim.SetBool("Open", false);
            }
            interaction.InteractOff();
        }
    }

    #endregion
}
