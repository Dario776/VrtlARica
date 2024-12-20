using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private TouchControls touchControls;
    private Coroutine zoomCoroutine;
    private Camera mainCamera;

    private void Awake()
    {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.PrimaryTouchContact.started += ctx => SwipeStart(ctx);
        touchControls.Touch.PrimaryTouchContact.canceled += ctx => SwipeEnd(ctx);
        touchControls.Touch.SecondaryTouchContact.started += ctx => ZoomStart();
        touchControls.Touch.SecondaryTouchContact.canceled += ctx => ZoomEnd();
    }

    private void SwipeEnd(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera,
            touchControls.Touch.PrimaryFingerPosition.ReadValue<Vector2>()), (float)ctx.time);
        }
    }

    private void SwipeStart(InputAction.CallbackContext ctx)
    {
        // ako se neko pretplatio na event (da ne saljemo bez razloga)
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCamera,
            touchControls.Touch.PrimaryFingerPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera,
            touchControls.Touch.PrimaryFingerPosition.ReadValue<Vector2>());
    }


    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f;
        float distance = 0f;
        while (true)
        {
            distance = Vector2.Distance(touchControls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(),
            touchControls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

            if (distance > previousDistance)
            {
                Debug.Log("zooming out");
            }
            else if (distance < previousDistance)
            {
                Debug.Log("zooming in");
            }

            previousDistance = distance;
            yield return null;
        }
    }
}
