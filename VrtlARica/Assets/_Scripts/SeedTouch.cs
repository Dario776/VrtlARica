using UnityEngine;

public class SeedTouch : MonoBehaviour
{
    private bool isSelected = false;
    private Renderer seedRenderer;

    private void Start()
    {
        seedRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Check for touches
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch detected");
            Touch touch = Input.GetTouch(0);

            // Check for a touch phase
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began at position: " + touch.position);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject) // Check if this object was touched
                    {
                        isSelected = !isSelected;

                        // Highlight the seed if selected
                        if (isSelected)
                        {
                            seedRenderer.material.color = Color.yellow; // Highlight color
                        }
                        else
                        {
                            seedRenderer.material.color = Color.white; // Default color
                        }
                    }
                }
            }
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}