using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchGesture : MonoBehaviour
{
    [SerializeField] InputAction firstFingerPosition, secondFingerPosition, secondFingerContact;
    [SerializeField] private float pinchThreshold = 50f;
    private Coroutine pinchCoroutine;
    private GameManager gameManager;

    private void Awake()
    {
        firstFingerPosition.Enable();
        secondFingerPosition.Enable();
        secondFingerContact.Enable();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        secondFingerContact.started += _ => PinchStart();
        secondFingerContact.canceled += _ => PinchEnd();
    }

    private void PinchStart()
    {
        if (pinchCoroutine == null)
        {
            pinchCoroutine = StartCoroutine(PinchDetect());
        }
    }

    private void PinchEnd()
    {
        if (pinchCoroutine != null)
        {
            StopCoroutine(pinchCoroutine);
            pinchCoroutine = null;
        }
    }

    private IEnumerator PinchDetect()
    {
        float previousDistance = Vector2.Distance(
            firstFingerPosition.ReadValue<Vector2>(),
            secondFingerPosition.ReadValue<Vector2>());

        while (true)
        {
            float currentDistance = Vector2.Distance(
                firstFingerPosition.ReadValue<Vector2>(),
                secondFingerPosition.ReadValue<Vector2>());

            if (Mathf.Abs(currentDistance - previousDistance) > pinchThreshold)
            {
                if (gameManager.placeObject.currentPotIndex <= 6)
                {
                    if (currentDistance > previousDistance)
                    {
                        Debug.Log("Pinched out");
                        gameManager.placeObject.ReplaceCurrentPotWithNextPotInLine();
                        DisableInputActions();
                    }
                }
                else
                {
                    if (currentDistance < previousDistance)
                    {
                        Debug.Log("Pinched in");
                        gameManager.placeObject.ReplaceCurrentPotWithNextPotInLine();
                        DisableInputActions();
                    }
                }
                previousDistance = currentDistance;
            }

            yield return null;
        }
    }
    private void DisableInputActions()
    {
        firstFingerPosition.Disable();
        secondFingerPosition.Disable();
        secondFingerContact.Disable();
        Debug.Log("Input actions disabled.");
    }
}
