using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public bool selfDestructEnabled;
    public float selfDestructTime;
    public BoxCollider2D boundBox;
    
    [Header("Monster Settings")]
    public bool canSpawnMobs;
    [SerializeField] private bool enableOne, enableTwo, enableThree;
    [SerializeField] private GameObject typeOne, typeTwo, typeThree;
    [SerializeField] private float oneSpawnRate, twoSpawnRate, threeSpawnRate;

    private void Start()
    {
        if (enableOne)
        {
            StartCoroutine(spawnMobs(oneSpawnRate, typeOne));
        }
        if (enableTwo)
        {
            StartCoroutine(spawnMobs(twoSpawnRate, typeTwo));
        }
        if (enableThree)
        {
            StartCoroutine(spawnMobs(threeSpawnRate, typeThree));
        }
    }

    private IEnumerator spawnMobs(float spawnRate, GameObject mob)
    {
        var waitTime = new WaitForSeconds(selfDestructTime); 
        if (canSpawnMobs)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject newMob = Instantiate(mob, new Vector3(Random.Range(boundBox.bounds.min.x, boundBox.bounds.max.x), Random.Range(boundBox.bounds.min.y, boundBox.bounds.max.y), 0), Quaternion.identity);
            if (selfDestructEnabled)
            {
                yield return waitTime;
                Destroy(newMob);
                StartCoroutine(spawnMobs(spawnRate, mob));
            }
            else
            {
                StartCoroutine(spawnMobs(spawnRate, mob));
            }
        }
    }
}
