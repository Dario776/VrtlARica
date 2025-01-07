using UnityEngine;

public class SeedController : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    private GameObject seed;

    // Set the seed GameObject for movement
    public void SetSeed(GameObject newSeed)
    {
        seed = newSeed;
    }

    // Move the seed left
    public void MoveLeft()
    {
        if (seed != null)
        {
            seed.transform.position -= new Vector3(moveSpeed, 0, 0);
            Debug.Log("Seed moved left.");
        }
        else
        {
            Debug.LogError("Seed not set.");
        }
    }

    // Move the seed right
    public void MoveRight()
    {
        if (seed != null)
        {
            seed.transform.position += new Vector3(moveSpeed, 0, 0);
            Debug.Log("Seed moved right.");
        }
        else
        {
            Debug.LogError("Seed not set.");
        }
    }

    // Move the seed up
    public void MoveUp()
    {
        if (seed != null)
        {
            seed.transform.position += new Vector3(0, 0, moveSpeed);
            Debug.Log("Seed moved up.");
        }
        else
        {
            Debug.LogError("Seed not set.");
        }
    }

    // Move the seed down
    public void MoveDown()
    {
        if (seed != null)
        {
            seed.transform.position -= new Vector3(0, 0, moveSpeed);
            Debug.Log("Seed moved down.");
        }
        else
        {
            Debug.LogError("Seed not set.");
        }
    }
}
