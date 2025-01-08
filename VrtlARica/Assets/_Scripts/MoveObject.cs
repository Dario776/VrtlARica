using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public void MoveLeft()
    {
        transform.position -= new Vector3(0.1f, 0, 0);
        Debug.Log(transform.position);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(0.1f, 0, 0);
        Debug.Log(transform.position);
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, 0.1f, 0);
        Debug.Log(transform.position);
        GameManager.Instance.ZalivenaBiljka();
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, 0.1f, 0);
        Debug.Log(transform.position);
        GameManager.Instance.SjemenkaPomaknuta();
    }
}
