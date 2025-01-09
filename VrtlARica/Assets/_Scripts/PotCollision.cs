using UnityEngine;

public class PotCollision : MonoBehaviour
{
    // stiti kod da se samo jednom izvede gameManager funkcija
    private bool isSeedMoved;
    private bool isWateringCanMoved;
    private GameManager gameManager;

    private void Awake()
    {
        isSeedMoved = false;
        isWateringCanMoved = false;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains(Constants.seed))
        {
            SeedMoved(collider.gameObject);
        }
        else if (collider.gameObject.name.Contains(Constants.wateringCan))
        {
            WateringCanMoved(collider.gameObject);
        }
    }

    public void SeedMoved(GameObject seed)
    {
        if (!isSeedMoved)
        {
            isSeedMoved = true;
            Destroy(seed);
            gameManager.SeedMoved();
        }
    }

    public void WateringCanMoved(GameObject wateringCan)
    {
        if (!isWateringCanMoved)
        {
            isWateringCanMoved = true;
            Destroy(wateringCan);
            gameManager.WateringCanMoved();
        }
    }
}
