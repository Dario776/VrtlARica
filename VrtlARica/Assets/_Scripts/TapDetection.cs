using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TapDetection : MonoBehaviour
{
    //potrebno da se detektiraju objekti koji se mogu selektirati, a ne npr. ARPlane
    [SerializeField] private LayerMask detectableLayer;
    [SerializeField] public bool isOutlined;
    private Outline outline;
    private static TapDetection currentlyTappedOn;

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += TapAction;
/*         EnhancedTouch.Touch.onFingerMove += DragAction;
        EnhancedTouch.Touch.onFingerUp += EndDragAction; */
        
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= TapAction;
/*         EnhancedTouch.Touch.onFingerMove -= DragAction;
        EnhancedTouch.Touch.onFingerUp -= EndDragAction; */
    }

    private void TapAction(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) { return; }

        Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, detectableLayer))
        {
            GameObject tappedObject = hitInfo.collider.gameObject;

            if (tappedObject == gameObject)
            {
                Debug.Log("Tapped on object: " + tappedObject.name);
                currentlyTappedOn = this;
            }
        }

    }

/*     private void DragAction(EnhancedTouch.Finger finger){
        Debug.Log("Drag action");
        if (currentlyTappedOn != this || finger.index != 0) { return; }

        Vector3 tapPosition = finger.currentTouch.screenPosition;
        tapPosition.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tapPosition);

        transform.position = worldPosition;
    }

    private void EndDragAction(EnhancedTouch.Finger finger)
    {
        if (currentlyTappedOn == this && finger.index == 0)
        {
            Debug.Log("Released drag on object: " + gameObject.name);
            currentlyTappedOn = null;
        }
    } */

    public void ToggleOutline()
    {
        isOutlined = !isOutlined;

        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }

        if (outline != null)
        {
            outline.enabled = isOutlined;
        }
    }

    //opcenito ako se zeli oznaciti neki objekt na koji je korisnik pritisnuo
    private void ToggleOutline(GameObject tappedObject)
    {
        outline = tappedObject.GetComponent<Outline>();
        isOutlined = !isOutlined;
        outline.enabled = isOutlined;
    }

    public static TapDetection GetCurrentlyTappedObject()
    {
        TapDetection tappedObject = currentlyTappedOn;
        currentlyTappedOn = null;
        return tappedObject;
    }

}