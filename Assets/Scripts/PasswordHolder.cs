using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordHolder : MonoBehaviour
{
    Dictionary<string, long> password = new Dictionary<string, long>();

    int noDomain = 0;

    public void AddPassword(string domain, int[] pass)
    {
        long codedPass = EncodePassword(pass);
        password.Add(domain, codedPass);
    }

    private void AddPassword(string domain, int pass)
    {
        password.Add(domain, pass);
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
        return pass;
    }

    public int[] GetPassword(string domain) {
        return DecodePassword(password[domain]);
    }

    public bool CheckLogin(string domain, int[] arr) {
        if (password[domain] == EncodePassword(arr))
            return true;
        return false;
    }
    
}
