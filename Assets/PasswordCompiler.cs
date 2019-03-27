using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 0, 0, 0, 0, 0 };
        int[] arr2 = { 7, 7, 7, 7, 7, 7, 7 };
        long p1, p2;
        p1 = EncodePassword(arr);
        p2 = EncodePassword(arr2);
        print(EncodePassword(arr));
        print(EncodePassword(arr2));
        print(DecodePassword(p1) == arr);
       print(DecodePassword(p2) == arr2);

    }

    void Update()
    {
        
    }


    long EncodePassword(int[] pass) {
        long passCode=0;
        for (int i = 0; i < pass.Length; i++) {
            passCode += (long)(Mathf.Pow(8, (pass.Length-i-1)))*pass[i];
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
            passCode -= cur*pass[i];
        }
        for (int i = 0; i < pass.Length; i++)
            print(pass[i]);
        return pass;
    }

    void SearchMatches(int[] pass) {
        //create hashmap object
    }

    void AddPassword(string domain, int[] pass) {
        //add to hasmap
    }



}
