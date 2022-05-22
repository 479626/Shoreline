using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Animator thunder;
    [SerializeField] private GameObject rain;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "L2-Battle")
        {
            SoundManager.instance.FluteSound();
        }
    }

    private void Start()
    {
        StartCoroutine(Loop());
    }

    public void ToggleRain()
    {
        if (rain.activeInHierarchy)
        {
            rain.SetActive(false);
        }
        else
        {
            rain.SetActive(true);
        }
    }

    private IEnumerator Loop()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.instance.ThunderSound();
        yield return new WaitForSeconds(Random.Range(0, 30));
        SoundManager.instance.ThunderSound();
        yield return new WaitForSeconds(0.3f);
        thunder.SetTrigger("flash");
        yield return Loop();
    }
}
