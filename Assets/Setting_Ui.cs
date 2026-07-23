using System;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Ui : MonoBehaviour
{
    [SerializeField] Toggle soundUi;
    [SerializeField] Toggle musicUI;


    void Start()
    {
        soundUi.onValueChanged.AddListener(MuteSound);
        musicUI.onValueChanged.AddListener(MuteMusic);
    }

    private void MuteMusic(bool arg0)
    {
        AudioPlayer.instance.muteMusic = !arg0;
    }

    private void MuteSound(bool arg0)
    {
        AudioPlayer.instance.bgSound.mute = !arg0;
    }
}
