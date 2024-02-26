using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        Goal.spawnBall += Spawn;
    }
    private void Spawn()
    {
        Instantiate(ball, transform.position, Quaternion.identity, null);
    }
}
