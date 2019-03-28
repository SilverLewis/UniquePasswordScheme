using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    int[] password = new int[7];
    int currentDirection = 0;
    public PasswordHolder passHolder;


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

    void AddPassword(string domain, int[] pass)
    {
        passHolder.AddPassword(domain, pass);
    }

    void AddPassword(int[] pass)
    {
        passHolder.AddPassword("test", pass);
    }

    public void DirectionChosen(int i)
    {
        if (currentDirection == 7)
        {
            for (int j = 0; j < 7; j++)
                print(password[j]);
            AddPassword(password);
            currentDirection = 0;

        }
        else if (currentDirection < 7)
        {
            password[currentDirection] = i;
            currentDirection++;
        }
    }
}
