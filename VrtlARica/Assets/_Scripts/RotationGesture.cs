using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationGesture : MonoBehaviour
{
    [SerializeField] private InputAction pressAction, positionAction;
    [SerializeField] private float swipeThreshold = 100f; 
    [SerializeField] private float rotationDegree = 15f; 
    [SerializeField] private float rotationSpeed = 5f; 

    private Coroutine swipeCoroutine;
    private Vector2 initialSwipePosition;
    private Quaternion targetRotation;

    private void Awake()
    {
        if (pressAction == null || positionAction == null)
        {
            Debug.LogError("InputActions are not assigned!");
        }

        pressAction?.Enable();
        positionAction?.Enable();

        targetRotation = transform.rotation;
    }

    private void Start()
    {
        pressAction.started += _ => SwipeStart();
        pressAction.canceled += _ => SwipeEnd();
    }

    private void SwipeStart()
    {
        Debug.Log("Swipe started");
        if (swipeCoroutine == null)
        {
            swipeCoroutine = StartCoroutine(SwipeDetect());
        }
    }

    private void SwipeEnd()
    {
        Debug.Log("Swipe ended");
        if (swipeCoroutine != null)
        {
            StopCoroutine(swipeCoroutine);
            swipeCoroutine = null;
        }
    }

    public void DisableInputActions()
    {
        pressAction.Disable();
        positionAction.Disable();
        Debug.Log("Input actions disabled.");
    }

    private IEnumerator SwipeDetect()
    {
        initialSwipePosition = positionAction.ReadValue<Vector2>();

        while (true)
        {
            Vector2 currentSwipePosition = positionAction.ReadValue<Vector2>();
            Vector2 swipeVector = currentSwipePosition - initialSwipePosition;

            if (swipeVector.magnitude >= swipeThreshold)
            {
                if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
                {
                    if (swipeVector.x > 0)
                    {
                        RotateRight();
                    }
                    else
                    {
                        RotateLeft();
                    }

                    initialSwipePosition = currentSwipePosition;
                }
            }

            yield return null;
        }
    }

    //nisu iste kao u RotateObject zbog inverzije
    private void RotateLeft()
    {
        targetRotation *= Quaternion.Euler(0, rotationDegree, 0);
        Debug.Log("Rotated Left");
    }

    private void RotateRight()
    {
        targetRotation *= Quaternion.Euler(0, -rotationDegree, 0);
        Debug.Log("Rotated Right");
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
