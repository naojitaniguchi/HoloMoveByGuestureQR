using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandControl : MonoBehaviour, IManipulationHandler
{
    public GameObject targetObject;
    public GameObject canvasObject;

    // for draging
    [SerializeField]
    float DragSpeed = 1.5f;

    [SerializeField]
    float DragScale = 1.5f;

    [SerializeField]
    float MaxDragDistance = 3f;

    [SerializeField]
    bool draggingEnabled = true;

    // for rotate
    [Tooltip("Speed of static rotation when tapping game object.")]
    [SerializeField]
    float RotateSpeed = 25f;

    [Tooltip("Speed of interactive rotation via navigation gestures.")]
    [SerializeField]
    float RotationFactor = 50f;

    [SerializeField]
    bool rotatingEnabled = true;


    // for Scaling
    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.5f;

    [SerializeField]
    float ResizeScaleFactor = 1.5f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.5f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 4f;

    [SerializeField]
    bool resizingEnabled = true;


    Vector3 lastScale;
    Quaternion lastRotation;
    Vector3 lastRotationEular;
    Vector3 lastPosition;
    Vector3 lastPositionCanvas;


    public void SetDragging(bool enabled)
    {
        draggingEnabled = enabled;
    }

    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
    }

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = gameObject;
        }
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(targetObject);
        switch ( InputModeByButton.inputType)
        {
            case InputModeByButton.InputType.MOVE:
                lastPosition = targetObject.transform.position;
                lastPositionCanvas = canvasObject.transform.position;
                break;
            case InputModeByButton.InputType.ROT:
                lastRotation = targetObject.transform.rotation;
                lastRotationEular = lastRotation.eulerAngles;
                break;
            case InputModeByButton.InputType.SCALE:
                lastScale = targetObject.transform.localScale;
                break;
        }
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        // Debug.Log(InputModeByButton.inputType);

        switch (InputModeByButton.inputType)
        {
            case InputModeByButton.InputType.MOVE:
                if (draggingEnabled)
                {
                    Drag(eventData.CumulativeDelta);
                }
                break;

            case InputModeByButton.InputType.ROT:
                if (rotatingEnabled)
                {
                    RotateByAxis(eventData.CumulativeDelta);
                }
                break;

            case InputModeByButton.InputType.SCALE:
                if (resizingEnabled)
                {
                    Resize(eventData.CumulativeDelta);
                }

                break;
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void Drag(Vector3 positon)
    {
        var targetPosition = lastPosition + positon * DragScale;
        var targetPositionCanvas = lastPositionCanvas + positon * DragScale;
        if (Vector3.Distance(lastPosition, targetPosition) <= MaxDragDistance)
        {
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPosition, DragSpeed);
            canvasObject.transform.position = Vector3.Lerp(canvasObject.transform.position, targetPositionCanvas, DragSpeed);
        }
    }

    void RotateByAxis( Vector3 rotVec)
    {
        targetObject.transform.Rotate(new Vector3(0, 1, 0), rotVec.x * RotateSpeed * -1.0f, Space.World);
        targetObject.transform.Rotate(new Vector3(1, 0, 0), rotVec.y * RotateSpeed, Space.World);
        targetObject.transform.Rotate(new Vector3(0, 0, 1), rotVec.z * RotateSpeed, Space.World);
    }

    // eventData.CumulativeDelta

    void Resize(Vector3 newScale)
    {
        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x
        if (AllowResizeWarp)
        {
            resizeX = newScale.x * ResizeScaleFactor;
            resizeY = newScale.y * ResizeScaleFactor;
            resizeZ = newScale.z * ResizeScaleFactor;
        }
        else
        {
            resizeX = resizeY = resizeZ = newScale.x * ResizeScaleFactor;
        }

        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);

        targetObject.transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(resizeX, resizeY, resizeZ),
            ResizeSpeedFactor);
    }
}
