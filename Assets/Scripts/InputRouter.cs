using System.Collections;
using System.Collections.Generic;
using TFR;
using UnityEngine;
using UnityEngine.InputSystem;
using static TFR.InputAsset;

public class InputRouter : MonoBehaviour
{
    private InputAsset inputMaps;

    private MovementActions movementMap;
    private UIActions uiMap;

    public MovementActions Movement => movementMap;
    public UIActions UI => uiMap;

    void Awake()
    {
        inputMaps = new TFR.InputAsset();

        movementMap = inputMaps.Movement;
        uiMap = inputMaps.UI;

        movementMap.Enable();
        uiMap.Disable();
    }

}
