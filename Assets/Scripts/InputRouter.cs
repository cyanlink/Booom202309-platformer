using System.Collections;
using System.Collections.Generic;
using TFR;
using UnityEngine;
using UnityEngine.InputSystem;
using static TFR.InputAsset;

public class InputRouter : MonoBehaviour
{
    [SerializeField] private InputAsset inputMaps;

    private MovementActions movementMap;
    private UIActions uiMap;

    public MovementActions Movement => movementMap;
    public UIActions UI => uiMap;

    void Awake()
    {
        movementMap = inputMaps.Movement;
        uiMap = inputMaps.UI;
    }

}
