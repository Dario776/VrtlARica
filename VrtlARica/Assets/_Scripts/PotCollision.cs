using UnityEngine;

public class PotCollision : MonoBehaviour
{
    public GameObject transformedPotModel;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger detected with: {other.gameObject.name} (Tag: {other.tag})");

        if (other.CompareTag("Sjemenka"))
        {
            Debug.Log("Seed has entered the pot trigger!");

            if (transformedPotModel != null)
            {
                Debug.Log("Activating transformed pot model.");
                transformedPotModel.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Transformed pot model is not assigned.");
            }
        }
    }
}
