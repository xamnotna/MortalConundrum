using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreventDeselectionGroup : MonoBehaviour
{
 EventSystem evt;

    private void Start()
    {
        evt = EventSystem.current;
    }

    GameObject sel;

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
        {
            sel = evt.currentSelectedGameObject;
        }
        else if (sel != null && evt.currentSelectedGameObject == null)
        {
            evt.SetSelectedGameObject(sel);
        }
    }
}
