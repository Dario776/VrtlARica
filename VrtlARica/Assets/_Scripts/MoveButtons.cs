using UnityEngine;

public class MoveButtons : MonoBehaviour
{
    private SeedController seedController;

    // Method to set the SeedController reference
    public void SetSeedController(SeedController controller)
    {
        seedController = controller;
        Debug.Log("SeedController is set in MoveButtons.");
    }

    // Move the seed to the left
    public void OnMoveLeft()
    {
        if (seedController != null)
        {
            seedController.MoveLeft();
        }
        else
        {
            Debug.LogError("SeedController is not set in MoveButtons.");
        }
    }

    // Move the seed to the right
    public void OnMoveRight()
    {
        if (seedController != null)
        {
            seedController.MoveRight();
        }
        else
        {
            Debug.LogError("SeedController is not set in MoveButtons.");
        }
    }

    // Move the seed upwards
    public void OnMoveUp()
    {
        if (seedController != null)
        {
            seedController.MoveUp();
        }
        else
        {
            Debug.LogError("SeedController is not set in MoveButtons.");
        }
    }

    // Move the seed downwards
    public void OnMoveDown()
    {
        if (seedController != null)
        {
            seedController.MoveDown();
        }
        else
        {
            Debug.LogError("SeedController is not set in MoveButtons.");
        }
    }
}
