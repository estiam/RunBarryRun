using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetButtonDown("Fire1"))
            {
                target.GetComponent<MenuHandler>().ToggleMenu();
            }
            return;
        }
        // Detect touch event
        foreach (var touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    target.GetComponent<MenuHandler>().ToggleMenu();
                    return;
                }
            }
        }

        //if (Input.GetButtonDown("Fire1") && !EventSystem.current.IS())
        //{
        //}
    }

}