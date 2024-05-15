using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbortProgress : MonoBehaviour
{
    public void Abort()
    {
        PlayerPrefs.DeleteAll();
    }
}
