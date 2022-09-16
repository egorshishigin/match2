using UnityEngine;

public class Vibrator : MonoBehaviour
{
    public bool isOn = false;

    public void Vibrate()
    {
        if (isOn)
        {
            Handheld.Vibrate();
        }
        else return;
    }
}
