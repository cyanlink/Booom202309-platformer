using System.Collections;
using System.Collections.Generic;
using BOOOM;
using UnityEngine;
using UnityEngine.InputSystem;
using static BOOOM.InputAsset;

public class InputRouter : MonoBehaviour
{
    private InputAsset m_Controls;
    public InputAsset InputControls
    {
        get
        {
            if (m_Controls == null)
                m_Controls = new InputAsset();
            return m_Controls;
        }
    }
}
