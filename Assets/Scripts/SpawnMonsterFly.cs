using UnityEngine;
using System.Collections;

public class SpawnMonsterFly : MonoBehaviour
{
    public GameObject enemy;                // The enemy prefab to be spawned.
    private GameObject go;
    private bool spawning = false;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        go = (GameObject)Instantiate(enemy, transform.position, new Quaternion(0, 0, 0, 0));
        spawning = false;
    }

    void Update()
    {
        if (go == null && spawning == false)
        {
            spawning = true;
            Invoke("Spawn", 3);
        }
    }

}
