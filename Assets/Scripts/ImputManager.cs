using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputManager : MonoBehaviour
{
    int[] password;
    public StateManager manager;
    int passwordPlace = 0;

    private void Start()
    {
        password = new int[StaticVariables.passwordLength];
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            if (manager.stage % 2 == 0 && manager.stage < 6)
            {
                manager.stage++;
                manager.NextScreen();
            }
        }
    }

    public void DirectionChosen(int i)
    {
        if (passwordPlace == StaticVariables.passwordLength - 1)
        {
            password[passwordPlace] = i;
            manager.CheckPassword(password);
            passwordPlace = 0;

        }
        else if (passwordPlace < StaticVariables.passwordLength - 1)
        {
            password[passwordPlace] = i;
            passwordPlace++;
        }
    }
    
    void PrintRealPassword()
    {
        manager.PrintRealPassword();
    }
    void PrintEnteredPassword()
    {
        int[] a = password;
        string fake = "";
        for (int i = 0; i < a.Length; i++)
        {
            fake += a[i];
        }
        print("fake: " + fake);
    }
}
