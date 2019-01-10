using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonController : MonoBehaviour
{
    private Button btn;
    private bool enabledFlag = true;

    void Start()
    {
        btn = GetComponent<Button>();
    }

    void Update()
    {
        if (btn != null)
        {
            btn.interactable = enabledFlag;
        }
    }

    public void DisableSubmitBtn()
    {
        enabledFlag = false;
    }

    public void EnableSubmitBtn()
    {
        enabledFlag = true;
    }
}
