using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TextDebugManager : MonoBehaviour, IHoldHandler, IInputHandler
{

    public TextMesh AnchorDebugText;
    private string _debugTextHold = "";
    private string _debugTextInput = "";

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (AnchorDebugText != null)
            AnchorDebugText.text = string.Format(
              "Hold: {0}\nInput: {1}", _debugTextHold, _debugTextInput);
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        _debugTextHold = "OnHoldStarted";
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        _debugTextHold = "OnHoldCompleted";
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        _debugTextHold = "OnHoldCanceled";
    }

    public void OnInputUp(InputEventData eventData)
    {
        _debugTextInput = "OnInputUp";
    }

    public void OnInputDown(InputEventData eventData)
    {
        _debugTextInput = "OnInputDown";
    }
}