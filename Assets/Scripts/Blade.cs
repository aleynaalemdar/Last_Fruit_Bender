using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private bool slicing;
    private Collider bladeCollider;
    private Camera mainCamera;
    private TrailRenderer bladeTrail;
    public GameManager gm;
    public Fruit fruit;
 

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    public float minTimeL = 0.07f;

    public float velocity;
    private float previousTime;
    private const float timeLine = 0.1f;
    public float timeInterval;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        
    }

     private void Update() // using for checking input
     {
         if (Input.GetMouseButtonDown(0))
         {
            //previousTime = Time.time;
            StartSlicing();

         } else if (Input.GetMouseButtonUp(0))
         {
           // timeInterval = Time.time - previousTime;
            StopSlicing();

         } else if (slicing)
         {
             ContinueSlicing();
         }

     }

    // FOR MOBILE 
   /* private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    StartSlicing();
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    StopSlicing();
                }
                else if (slicing)
                {
                    ContinueSlicing();
                }
            }
        }
    }
   */
    private void OnEnable()
    {
        StopSlicing(); // !!!!!
    }

    private void OnDisable()
    {
        StopSlicing();
        
        }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();

    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;

    //    Debug.LogError(fruit.countOfSlicedFruits); 
       /* if (timeInterval > minTimeL)
        {
            Debug.Log("hey");
            gm.Combo();
        }*/

        previousTime = Time.time;
        // gm.comboCount = 0;
       // fruit.countOfSlicedFruits = 0;
    }

    private void ContinueSlicing()
    {
        // mouse position in screen space
        // blade position in world space
        // convert !
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        velocity = direction.magnitude / Time.deltaTime; // destince / time = hız
        bladeCollider.enabled = velocity > minSliceVelocity; // !

        if(velocity < 10f)
        {
            //gm.comboCount = 0;
        }
       // Debug.Log(fruit.countOfSlicedFruits);
        transform.position = newPosition;

        

        
    }

}
