using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;


    public GameObject[] fruitPrefabs; // an array for fruits

    public GameObject bombPrefab;

    [Range(0f,1f)]
    public float bombChance=0.05f;

    public float minSpawnDelay = 0.25f; // delay for spawn actions?
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 15f; // despawning objects when they are irrelivant

    //private int fruitsExited = 0; 

    public GameManager gm;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        //playableArea = GetComponent<Collider>();
    }

   private void OnEnable()
    {
        StartCoroutine(Spawn());

    }

    private void OnDisable()
    {
       StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);

        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance)
                
            {
                prefab = bombPrefab;

            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // for rotation

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation); // fruit oluşturduk


            Destroy(fruit, maxLifetime); // zamanı dolduktan sonra yok ediyor objeyi

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            // ??
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }



}
