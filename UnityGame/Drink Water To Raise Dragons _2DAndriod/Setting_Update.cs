using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Update : MonoBehaviour
{
    [SerializeField] Setting setting;
    [SerializeField] Text VibrationText;
    [SerializeField] Text NotificationText;
    [SerializeField] Image VibrationButton;
    [SerializeField] Image NotificationButton;
    private void Awake()
    {
        TextUpdate();
    }
    public void ClickVibration()
    {
        setting.IsVibration = !setting.IsVibration;
        TextUpdate();
    }
    public void ClickNotification()
    {
        setting.IsNotifications = !setting.IsNotifications;
        TextUpdate();
    }
    void TextUpdate()
    {
        var vtextColor = VibrationText.color;
        var ntextColor = NotificationText.color;

        if (setting.IsVibration == true)
        {
            vtextColor.a = 1f;
        }
        else
        {
            vtextColor.a = .6f;
        }
        VibrationText.color = vtextColor;
        VibrationButton.color = vtextColor;

        if (setting.IsNotifications == true)
        {
            ntextColor.a = 1f;
        }
        else
        {
            ntextColor.a = .6f;
        }
        NotificationText.color = ntextColor;
        NotificationButton.color = ntextColor;
    }
}
