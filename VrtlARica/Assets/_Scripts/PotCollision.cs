using UnityEngine;

public class PotCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "seed(Clone)")
        {
            Debug.Log("seed ocitan");
            Destroy(collision.gameObject);
            GameManager.Instance.SjemenkaPomaknuta();
        }
        else if (collision.gameObject.name == "wateringCan(Clone)")
        {
            Debug.Log("wateringCan ocitan");
            Destroy(collision.gameObject);
            GameManager.Instance.ZalivenaBiljka();
        }
    }
}
