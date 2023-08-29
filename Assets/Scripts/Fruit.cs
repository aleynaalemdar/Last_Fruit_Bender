using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody; // for transfering the velocity ? + for adding force
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    public Rigidbody upRb;
    public Rigidbody downRb;

    public GameManager gm;

    public bool isActive = true;

    public int points = 1;

    public bool isSlice = false;

    //public int countOfSlicedFruits = 0;

 

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();

        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    

    }

    private void Update()
    {
        if (transform.position.y < -20 && isActive && !isSlice)
        {
            FindObjectOfType<GameManager>().IncreaseFruitCount();
            isActive = false;

        }
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        //FindObjectOfType<GameManager>().IncreaseScore(points);
      //countOfSlicedFruits++;
//        Debug.Log(countOfSlicedFruits);

        whole.SetActive(false);
        sliced.SetActive(true);

        isSlice = true;

        fruitCollider.enabled = false;
        juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);


        upRb.velocity = fruitRigidbody.velocity; // whole v == slice v
        upRb.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        upRb.AddTorque(direction * force, ForceMode.Impulse);
        //  ForceMode.Impulse for one time

        downRb.velocity = fruitRigidbody.velocity; // whole v == slice v
        downRb.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        downRb.AddTorque(-1 * direction * force, ForceMode.Impulse);

       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            // our blade = player
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
            FindObjectOfType<GameManager>().IncreaseScore(points);
            FindObjectOfType<GameManager>().Combo();



        }

    }

}