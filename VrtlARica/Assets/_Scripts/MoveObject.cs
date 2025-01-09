using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public void MoveLeft()
    {
        transform.position -= new Vector3(0.1f, 0, 0);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(0.1f, 0, 0);
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, 0.1f, 0);
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, 0.1f, 0);
    }
}
