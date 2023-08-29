using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class MenuPage : MonoBehaviour
{
    public Image MainImage;
    public TMP_Text mainText;
    public UnityEvent OnButtonClickEvent;
    public Button startButton;
    public GameManager gm;
    public GameObject StartingObject;

    private void Start()
    {
        startButton.onClick.AddListener(HandleButtonClick);
      
    }
  
    public void HandleButtonClick()
    {
        StopAllCoroutines();
       // Debug.Log("Button clicked!");
       
        StartingObject.SetActive(false);
        gm.NewGame();
        // OnButtonClickEvent.Invoke();
    }

}
