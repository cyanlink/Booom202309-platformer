using System.Collections;
using System.Collections.Generic;
using BOOOM;
using UnityEngine;
using UnityEngine.InputSystem;
using static BOOOM.InputAsset;

public class InputRouter : MonoBehaviour
{
    private InputAsset inputMaps;

    private MovementActions movementMap;
    private UIActions uiMap;

    public MovementActions Movement => movementMap;
    public UIActions UI => uiMap;

    void Awake()
    {
        inputMaps = new BOOOM.InputAsset();

        movementMap = inputMaps.Movement;
        uiMap = inputMaps.UI;

        movementMap.Enable();
        uiMap.Disable();
    }

}
