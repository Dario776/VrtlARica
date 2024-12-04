using UnityEngine;

public class SwipeControlsUpute : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x)
            {
                Right();
            }

            if (endTouchPosition.x > startTouchPosition.x)
            {
                Left();
            }
        }
    }

    private void Right()
    {
        this.GetComponent<Upute>().ShowNextUpute();
    }
    private void Left()
    {
        this.GetComponent<Upute>().ShowLastUpute();
    }




}