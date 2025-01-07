using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TapDetection : MonoBehaviour
{
    //potrebno da se detektiraju objekti koji se mogu selektirati, a ne npr. ARPlane
    [SerializeField]
    private LayerMask detectableLayer;

    [SerializeField]
    public bool isOutlined;
    private Outline outline;
    private static TapDetection currentlyTappedOn;

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

        Debug.Log("Finger tapped at screen position: " + finger.currentTouch.screenPosition);

        Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, detectableLayer))
        {
            GameObject tappedObject = hitInfo.collider.gameObject;
            Debug.Log("Tapped object: " + tappedObject.name);

            if (tappedObject == gameObject)
            {
                Debug.Log("Correct object tapped: " + tappedObject.name);
                currentlyTappedOn = this;
            }
        }
        else
        {
            Debug.Log("No object detected in raycast.");
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

    public static TapDetection GetCurrentlyTappedObject()
    {
        if (currentlyTappedOn != null)
        {
            Debug.Log("Currently tapped object: " + currentlyTappedOn.gameObject.name);
        }
        else
        {
            Debug.Log("No object is currently tapped!");
        }

        TapDetection tappedObject = currentlyTappedOn;
        currentlyTappedOn = null;
        return tappedObject;
    }


}