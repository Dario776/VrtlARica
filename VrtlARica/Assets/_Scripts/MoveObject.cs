using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private GameObject targetObject;

    // Method to set the target object to move
    public void SetMoveTarget(GameObject target)
    {
        targetObject = target;
    }

    // Method to move the object left
    public void MoveLeft()
    {
        if (targetObject != null)
        {
            targetObject.transform.position -= new Vector3(0.1f, 0, 0);  // Adjust as needed
            Debug.Log(transform.position);
        }
    }

    // Method to move the object right
    public void MoveRight()
    {
        if (targetObject != null)
        {
            targetObject.transform.position += new Vector3(0.1f, 0, 0);  // Adjust as needed
            Debug.Log(transform.position);
        }
    }

    // Method to move the object up
    public void MoveUp()
    {
        if (targetObject != null)
        {
            targetObject.transform.position += new Vector3(0, 0.1f, 0);  // Adjust as needed
            Debug.Log(transform.position);
        }
    }

    // Method to move the object down
    public void MoveDown()
    {
        if (targetObject != null)
        {
            targetObject.transform.position -= new Vector3(0, 0.1f, 0);  // Adjust as needed
            Debug.Log(transform.position);
        }
    }
}
