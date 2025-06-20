using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class OutlineOnTap : MonoBehaviour
{
    //potrebno da se detektiraju objekti koji se mogu selektirati, a ne npr. ARPlane
    [SerializeField] private LayerMask detectableLayer;
    [SerializeField] public bool isOutlined;
    private Outline outline;
    private static OutlineOnTap currentlyTappedOn;

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += TapAction;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= TapAction;
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

    public static OutlineOnTap GetCurrentlyTappedObject()
    {
        OutlineOnTap tappedObject = currentlyTappedOn;
        currentlyTappedOn = null;
        return tappedObject;
    }

}