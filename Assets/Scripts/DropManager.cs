using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject silverCoin;
    public InteractionCounter counter;

    public void SpawnCoin(string dropName, float x, float y)
    {
        GameObject coin = Instantiate(silverCoin, new Vector3(x, y), Quaternion.identity);
        coin.name = dropName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter.coins++;
        Destroy(gameObject);
    }
}
