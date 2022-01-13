using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsCtrl : MonoBehaviour
{
    public TextAsset info;

    public TextMeshProUGUI text;

    private void Awake()
    {
        text.text = info.text;
    }
}
