using UnityEngine;
using UnityEngine.UI;

public class TouchCode : MonoBehaviour
{
    public Text phaseDisplayText;
    private Touch touch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                phaseDisplayText.text = touch.phase.ToString();
                timeTouchEnded = Time.time;
            }

            else if (Time.time - timeTouchEnded > displayTime)
            {
                phaseDisplayText.text = touch.phase.ToString();
                timeTouchEnded = Time.time;
            }
        }

        else if (Time.time - timeTouchEnded > displayTime)
        {
            phaseDisplayText.text = "";
        }
    }
}
