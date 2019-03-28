using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    int[] password = new int[7];
    int currentDirection = 0;
    public PasswordHolder passHolder;



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
