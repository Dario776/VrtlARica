using UnityEngine;

public class PotCollision : MonoBehaviour
{
    public GameObject transformedPotModel;  // The new pot model that will appear when the seed reaches the pot

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("nezz");
        if (collision.gameObject.CompareTag("Sjemenka"))
        {
            Debug.Log("uspio");
            // When the seed collides with the pot, transform the pot model
            if (transformedPotModel != null)
            {
                transformedPotModel.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
