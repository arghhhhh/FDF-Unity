using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCircle : MonoBehaviour
{
    public int degree;

    public float radius;
    public float direction;

    public GameObject[] ps = new GameObject[8];
    public GameObject spawnPoint;
    
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        float arclength = 2 * Mathf.PI * (degree/360);
        float nextAngle = arclength / ps.Length;
        float angle = 0;

        for (int i = 0; i < ps.Length; i++)
        {
            float x = Mathf.Cos(angle) * radius * direction;
            float z = Mathf.Sin(angle) * radius * direction;

            ps[i].transform.position = new Vector3(spawnPoint.transform.localPosition.x + x, spawnPoint.transform.localPosition.y, spawnPoint.transform.localPosition.z + z);

            angle += nextAngle;
        }
    }
}
