using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Zooming feature not yet implemented. This feature may be affected by
    // whether or not a pixel perfect camera is being used.
    // If pixel perfect camera component is enabled, try adjusting the pixels
    // per unit on that component. If disabled, try adjusting the orthographic
    // size on the normal camera component.

    private Vector3 initialPosition;
    private Vector3 movementVector;
    private float xDifference;
    private float yDifference;

    public KeyCode neutralPosButton;
    public float moveSpeed;
    public float maxHorizontalOffset;
    public float maxVerticalOffset;

    private void Start()
    {
        initialPosition = gameObject.transform.position;
        movementVector = new Vector3();
    }

    private void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        // Differences will be negative if left or down, positive if right or up from initialPosition.
        xDifference = gameObject.transform.position.x - initialPosition.x;
        yDifference = gameObject.transform.position.y - initialPosition.y;

        // If camera is out of bounds in the x direction...
        if(Mathf.Abs(xDifference) >= maxHorizontalOffset)
        {
            // If the horizontal movement direction does not go back to the center, do not allow x movement.
            if(movementVector.x * xDifference >= 0)
            {
                movementVector.x = 0;
            }
        }
        // If camera is out of bounds in the y direction...
        if(Mathf.Abs(yDifference) >= maxVerticalOffset)
        {
            // If the vertical movement direction does not go back to the center, do not allow y movement.
            if (movementVector.y * yDifference >= 0)
            {
                movementVector.y = 0;
            }
        }

        gameObject.transform.Translate(movementVector * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(neutralPosButton))
        {
            gameObject.transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        }
    }
}
