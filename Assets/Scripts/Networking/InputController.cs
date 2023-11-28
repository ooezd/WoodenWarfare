using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Touch currentTouch;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
        }
    }
}
