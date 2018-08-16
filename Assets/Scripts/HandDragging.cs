using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandDragging : MonoBehaviour, IManipulationHandler
{
    public GameObject targetObject;
    public GameObject canvasObject;

    [SerializeField]
    float DragSpeed = 1.5f;

    [SerializeField]
    float DragScale = 1.5f;

    [SerializeField]
    float MaxDragDistance = 3f;
        
    Vector3 lastPosition;
    Vector3 lastPositionCanvas;

    [SerializeField]
    bool draggingEnabled = true;
    public void SetDragging(bool enabled)
    {
        draggingEnabled = enabled;
    }

    void Start()
    {
        if ( targetObject == null)
        {
            targetObject = gameObject;
        }
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(targetObject);
        lastPosition = targetObject.transform.position;
        lastPositionCanvas = canvasObject.transform.position;
        InputMode.instance.setInputMode(InputMode.InputType.MOVE);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (draggingEnabled)
        {         
            Drag(eventData.CumulativeDelta);

            //sharing & messaging
            //SharingMessages.Instance.SendDragging(Id, eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        InputMode.instance.setInputMode(InputMode.InputType.NONE);
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        InputMode.instance.setInputMode(InputMode.InputType.NONE);
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
}
