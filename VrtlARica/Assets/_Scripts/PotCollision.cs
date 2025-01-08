using UnityEngine;

public class PotCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnTriggerEnter");

        if (collision.gameObject.name == "seed")
        {
            Debug.Log("seed ocitan");
            Destroy(collision.gameObject);
            GameManager.Instance.SjemenkaPomaknuta();
        }
        else if (collision.gameObject.name == "wateringCan")
        {
            Debug.Log("wateringCan ocitan");
            Destroy(collision.gameObject);
            GameManager.Instance.ZalivenaBiljka();
        }
    }
}
