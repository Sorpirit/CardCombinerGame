using System.Collections;
using System.Collections.Generic;
using Analitics;
using TMPro;
using UnityEngine;

public class PrintAnalytics : MonoBehaviour
{
    [SerializeField] private TMP_Text anayticsText;

    public void Trigger()
    {
        string analytics = MyAnalytics.Instance.LogAnalytics();
        GUIUtility.systemCopyBuffer = analytics;
        anayticsText.text = analytics;
    }
}
