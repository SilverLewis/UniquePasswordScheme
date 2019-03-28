using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    int[] password = new int[7];
    int currentDirection = 0;

    void Start()
    {
     

    }

    void Update()
    {

    }


    long EncodePassword(int[] pass)
    {
        long passCode = 0;
        for (int i = 0; i < pass.Length; i++)
        {
            passCode += (long)(Mathf.Pow(8, (pass.Length - i - 1))) * pass[i];
        }
        return passCode;
    }

    int[] DecodePassword(long passCode)
    {
        int[] pass = new int[7];
        for (int i = 0; i < pass.Length; i++)
        {
            long cur = (long)(Mathf.Pow(8, (pass.Length - i - 1)));
            pass[i] = (int)(passCode / cur);
            passCode -= cur * pass[i];
        }
        for (int i = 0; i < pass.Length; i++)
            print(pass[i]);
        return pass;
    }

    void SearchMatches(int[] pass)
    {
        //create hashmap object
    }

    void AddPassword(string domain, int[] pass)
    {
        //add to hasmap
    }

    public void DirectionChosen(int i)
    {
        if (currentDirection == 7)
        {
            for (int j = 0; j < 7; j++)
                print(password[j]);
        }
        else if (currentDirection < 7)
        {
            password[currentDirection] = i;
            currentDirection++;
        }

        
    }


}
