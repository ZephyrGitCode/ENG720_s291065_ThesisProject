using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleHint : MonoBehaviour
{
    public GameObject HintText;
    public bool HintOn = true;

    public void Toggle()
    {
        if (HintText != null)
        {
            HintText.gameObject.SetActive(!HintOn);
            HintOn = !HintOn;
        }
    }
}
