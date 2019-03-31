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

    //runs at start of program
    private void Start()
    {
        date = DateTime.Now;
        print(date.ToString());
        SetUserId();
    }
    //gives user random identity from 0-2^21
    public void SetUserId() {
        userID = UnityEngine.Random.Range(0, 2097152);
    }
    //called when user is starting to enter password
    public void StartTimer() {
        startTime = Time.fixedTime;
    }
    //called when user entered password, bool represents if it was correct or not
    public void EndTimer(bool fail) {
        times.Add(Time.fixedTime - startTime);
        failedlogin.Add(fail);
        if (fail)
            successes++;
        else
            fails++;
    }

    //exports to excel file
    public void ExportLog() {
        string[] rowDataTemp;

        //each index in array represents info to be placed in a respective collum
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

        //converts array into something System.io.file can read
        //2 D array because 1D arrays would make every index be its own collum rather than own row
        string[][] output = new string[1][];
  
        for (int i = 0; i < output.Length; i++)
        {
            output[0] = rowDataTemp;
        }

        //outputs to excel
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
