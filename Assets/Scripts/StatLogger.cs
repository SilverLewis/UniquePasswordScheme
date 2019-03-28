using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLogger : MonoBehaviour
{

    List<float> times = new List<float>();
    List<bool> failedlogin = new List<bool>();

    float startTime = 0;


    public void StartTimer() {
        startTime = Time.fixedTime;
    }

    public void EndTimer(bool fail) {
        times.Add(Time.fixedTime - startTime);
        failedlogin.Add(fail);
    }
}
