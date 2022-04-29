using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Animator animator;
    public GameObject background;

    void Start()
    {
        animator.SetBool("on", false);
    }

    public void Death()
    {
        background.SetActive(true);
        animator.SetBool("on", true);
    }

    public void OnButtonClick()
    {
        background.SetActive(false);
        animator.SetBool("on", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
