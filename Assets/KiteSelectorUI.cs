using System;
using UnityEngine;
using UnityEngine.UI;

public class KiteSelectorUI : MonoBehaviour
{
    [SerializeField] Toggle[] allToggle;


    void Awake()
    {
        foreach (var item in allToggle)
        {
            item.onValueChanged.AddListener(OnValueChanged);
        }
    }

    private void OnValueChanged(bool arg0)
    {
        SetSelectedKiteIndex();
    }

    public void SetSelectedKiteIndex()
    {
        for (var i = 0; i < allToggle.Length; i++)
        {
            if (allToggle[i].isOn)
                PlayerPrefs.SetInt("SelectedKite", i);
        }

    }
}
