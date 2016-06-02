using UnityEngine;
using System.Collections;

public class SpawnMonsterFly : MonoBehaviour
{
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public float spawnX;
    public float spawnY;

    // Use this for initialization
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {

        Vector3 pos = new Vector3(spawnX, spawnY);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, pos, new Quaternion(0,0,0,0));
    }

}
