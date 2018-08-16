using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandRotate : MonoBehaviour, IManipulationHandler
{
    public GameObject targetObject;

    [Tooltip("Speed of static rotation when tapping game object.")]
    [SerializeField]
    float RotateSpeed = 25f;

    [Tooltip("Speed of interactive rotation via navigation gestures.")]
    [SerializeField]
    float RotationFactor = 50f;

    Quaternion lastRotation;

    [SerializeField]
    bool rotatingEnabled = true;
    public void SetRotating(bool enabled)
    {
        rotatingEnabled = enabled;
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
        lastRotation = transform.rotation;

        InputMode.instance.setInputMode(InputMode.InputType.ROT);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (rotatingEnabled)
        {
            var rotation = new Quaternion(eventData.CumulativeDelta.y * RotationFactor,
                eventData.CumulativeDelta.x * RotationFactor,
                eventData.CumulativeDelta.z * RotationFactor,
                0f);

            Rotate(rotation);

            //sharing & messaging
            //SharingMessages.Instance.SendRotating(Id, eventData.CumulativeDelta);
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
        
    void Rotate(Quaternion rotation)
    {
        targetObject.transform.rotation = Quaternion.Euler(
            new Vector3(lastRotation.x + rotation.x,
                 lastRotation.y + rotation.y,
                 lastRotation.z + rotation.z) * RotateSpeed);
    }
}
