using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonController : MonoBehaviour
{
    private Button btn;
    private bool enabled = true;

    void Start()
    {
        btn = GetComponent<Button>();
    }

    void Update()
    {
        if (btn != null)
        {
            btn.interactable = enabled;
        }
    }

    public void DisableSubmitBtn()
    {
        enabled = false;
    }

    public void EnableSubmitBtn()
    {
        enabled = true;
    }
}
