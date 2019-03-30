using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatLogger : MonoBehaviour
{

    List<float> times = new List<float>();
    List<bool> failedlogin = new List<bool>();
    long userID =0;
    float startTime = 0;
    DateTime date;
    int successes = 0;
    int fails = 0;


    private void Start()
    {
        date = DateTime.Now;
        SetUserId();
    }

    public void SetUserId() {
        userID = UnityEngine.Random.Range(0, 2097152);
    }
    public void StartTimer() {
        startTime = Time.fixedTime;
    }

    public void EndTimer(bool fail) {
        times.Add(Time.fixedTime - startTime);
        failedlogin.Add(fail);
        if (fail)
            successes++;
        else
            fails++;
    }

    public void PrintLog() {
        for (int i = 0; i < times.Count; i++) {
            print(failedlogin[i]+":"+times[i]);

        }

    }

    public void ExportLog() {
    }
}
