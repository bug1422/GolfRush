using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject goal;
    private bool isCreating = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (!isCreating && IsCreated())
        {
            isCreating = true;
            StartCoroutine(Spawn());
        }
    }
    private bool IsCreated()
    {
        foreach(Transform child in transform)
        {
            if (child.transform.childCount > 0) return false;
        }
        return true;
    }

    private IEnumerator Spawn()
    {
        int r = Random.Range(0, transform.childCount);
        Transform location = transform.GetChild(r);
        yield return new WaitForSeconds(0.5f);
        Instantiate(goal, GetSpawnLocationOffset(location), Quaternion.identity, location);
        isCreating = false;
    }
    private Vector2 GetSpawnLocationOffset(Transform location)
    {
        Vector2[] positions = location.GetComponent<EdgeCollider2D>().points;
        int r = Random.Range(0, positions.Length);
        Vector2 position = location.TransformPoint(Vector2.zero);
        return new Vector2(position.x, position.y);
    }
}
