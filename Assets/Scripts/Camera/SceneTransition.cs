using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int scene;
    public Animator anim;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Interaction interaction;

    void MovePlayer()
    {
        SoundManager.instance.DoorSound();
        playerStorage.initialValue = playerPos;
        SceneManager.LoadScene(scene);
    }

    #region Collision Detection

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            interaction.InteractOn();
            if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 8)
            {
                anim.SetBool("Open", true);
            }
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("F key recognised");
                MovePlayer();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            anim.SetBool("Open", false);
            interaction.InteractOff();
        }
    }

    #endregion
}
