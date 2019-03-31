using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
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
        print(date.ToString());
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

    public void ExportLog() {
        List<string[]> rowData = new List<string[]>();
        string[] rowDataTemp;


        rowDataTemp = new string[times.Count+4];
        rowDataTemp[0] = userID.ToString();
        rowDataTemp[1] = date.ToString();
        rowDataTemp[2] = successes.ToString();
        rowDataTemp[3] = fails.ToString();

        for (int i = 0; i < times.Count; i++) {
            if (failedlogin[i])
                rowDataTemp[i + 4] = "s:";
            else
                rowDataTemp[i + 4] = "f:";
            rowDataTemp[i + 4] += times[i].ToString();
        }

        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();
        StreamWriter outStream = System.IO.File.AppendText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath + "Saved_data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
