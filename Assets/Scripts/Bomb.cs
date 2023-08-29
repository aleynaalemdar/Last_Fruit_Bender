using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

   // public bool isGameOver = false;
    public XObject Xobject;
    public XObject Xobject1;
    public XObject Xobject2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<XObject>().StartByDefault();
            FindObjectOfType<GameManager>().Explode();
            // FindObjectOfType<GameManager>().IncreaseFruitCount();
         



        }

    }
}
