using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModeByButton {
    public enum InputType
    {
        MOVE,
        ROT,
        SCALE,
        NONE
    }

    public static InputType inputType;
}
