using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{

    public UnityEvent OnButtonClickEvent;
    public Button Button;
    public GameManager gm;
    public GameObject StartingObject;
    public GameObject GameOverObject;
    public XObject Xobject;
    public XObject Xobject1;
    public XObject Xobject2;
    // public Image fadeImage;


    private void Start()
    {
        Button.onClick.AddListener(HandleButtonClick);
    }
   

    public void HandleButtonClick()
    {
        FindObjectOfType<XObject>().StartByDefault();
        Xobject.StartByDefault();
        Xobject1.StartByDefault();
        Xobject2.StartByDefault();
        
        Debug.LogError("a");
        StartingObject.SetActive(false);
        GameOverObject.SetActive(false);
        gm.NewGame();
    }

}
