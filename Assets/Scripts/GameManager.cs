using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score;

    private Blade blade;
    public Spawner spawner;
    public XObject Xobject;
    public XObject Xobject1;
    public XObject Xobject2;

    public GameObject GameOverObject;
    public GameOver go;
    public Image fadeImage;
    public GameObject ComboObject;
    

    public int missedFruits = 0;
    public int maxMissedFruits = 3;

    public int count =0;
    // public bool canStartGame = false;
    public int comboCount = 0;
    private float minComboVelocity = 1.0f;
    private float minRequiredTime = 0.5f;
    public float lastTime;
    public GameOver gameOver;
    public CanvasGroup gameOverGroup;

    public TMP_Text ComboText;


    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
      
    }

    public void Start()
    {
       // NewGame();
       // spawner.enabled = true; 
    }

    public void NewGame()
    {
        //Debug.LogError("bir şey");
        Time.timeScale = 1f;

        blade.enabled = true;
        
        score = 0;
        count = 0;
        scoreText.text = score.ToString();
        // question: why didnt we call our ıncreasescore function in here?
        // where the score will be increased?
  
        ClearScene();
        spawner.enabled = true;

    }

    private void Update()
    {
        timeCurr += Time.deltaTime;
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach(Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }

       
    }
    public void countCombo(int amount)
    {
        comboCount++;

        if (comboCount >= 2)
        {
          IncreaseScore(amount);
          Debug.Log("Combo!!!"+comboCount);
         StartCoroutine(ComboTextOnScreen());

        }
    }

    float timeCurr=0f;
    
    public void Combo()
    {
       // Debug.Log("c");

        if (comboCount == 0)
        {
            timeCurr = 0f;
            comboCount = 1;

        }
        else if (timeCurr< minRequiredTime)
        {
            countCombo(1);
            //Debug.Log("u");
        }
        else
        {
            timeCurr = 0f;
            comboCount = 1;
        }

        
    }

    public IEnumerator ComboTextOnScreen()
    {
        ComboObject.SetActive(true);
        ComboText.text = "Combo "+ comboCount.ToString();
        yield return new WaitForSecondsRealtime(1f);
        ComboObject.SetActive(false);

    }

    public void IncreaseScore(int amount)
    {
        score+= amount;
        scoreText.text = score.ToString();

    }
       
    public void Explode()
    {
        blade.enabled = false;
       spawner.enabled = false;

        FindObjectOfType<XObject>().StartByDefault();

        Xobject.StartByDefault();
        Xobject1.StartByDefault();
        Xobject2.StartByDefault();

        StartCoroutine(ExplodeSequence());
        fadeImage.gameObject.SetActive(true);
        count = 0;


    }


    public IEnumerator ExplodeSequence()
    {
        GameOverObject.SetActive(true);
        
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);
            gameOverGroup.alpha = Mathf.Lerp(0,1,t);

           //
           //Time.timeScale = 1f - t;


            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

      //  yield return new WaitForSecondsRealtime(1.5f);

        // gm.NewGame();
       // ClearScene();


       // elapsed = 0f;

     
        fadeImage.color = Color.clear;
    }


        public void IncreaseFruitCount()
    {
        count++;
        if (count == 1)
        {
            Xobject.LossSituation();
            Xobject1.StartByDefault();
            Xobject2.StartByDefault();


        }
        if (count == 2)
        {
            Xobject1.LossSituation();
            Xobject2.StartByDefault();
        }

        if (count >= 3)
        {
            Xobject2.LossSituation();
            count = 0;
            Explode();
   
           FindObjectOfType<XObject>().StartByDefault();

            Xobject.StartByDefault();
            Xobject1.StartByDefault();
            Xobject2.StartByDefault();
           

        }
    }


}
