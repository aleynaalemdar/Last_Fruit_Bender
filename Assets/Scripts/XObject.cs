using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XObject : MonoBehaviour
{
    public GameObject Default;
    public GameObject Loss;

    public void StartByDefault()
    {
        Default.SetActive(true);
        Loss.SetActive(false);
    } 

    public void LossSituation()
    {
        Default.SetActive(false);
        Loss.SetActive(true);
    }
}
