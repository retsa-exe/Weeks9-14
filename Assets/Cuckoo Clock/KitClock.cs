using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;

    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    Coroutine clockIsRunning;
    IEnumerator doOneHour;

    private void Start()
    {
        clockIsRunning = StartCoroutine(MoveTheClock());
    }

    IEnumerator MoveTheClock()
    {
        while (true)
        {
            //yield return doOneHour = StartCoroutine(MoveTheClockHandsOneHour());
            doOneHour = MoveTheClockHandsOneHour();
            yield return doOneHour;
        }
    }

    IEnumerator MoveTheClockHandsOneHour()
    {
        t = 0;
        while (t < timeAnHourTakes)
        {
            t += Time.deltaTime;
            minuteHand.Rotate(0, 0, - (360 / timeAnHourTakes) * Time.deltaTime);
            hourHand.Rotate(0, 0, - (30 / timeAnHourTakes) * Time.deltaTime);

            yield return null;
        }
        hour++;
        if (hour == 13)
        {
            hour = 1;
        }
        OnTheHour.Invoke(hour);
    }

    public void StopTheClock()
    {
        if (clockIsRunning != null)
        {
            StopCoroutine(clockIsRunning);
        }
        if (doOneHour != null)
        {
            StopCoroutine(doOneHour);
        }
        
    }
}
