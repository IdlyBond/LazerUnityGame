using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    private GameObject player;
    public GameObject restartPrefab;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!player)
        {
            Instantiate(restartPrefab, GameObject.Find("Canvas").transform);
            Destroy(gameObject);
        }
    }
}
