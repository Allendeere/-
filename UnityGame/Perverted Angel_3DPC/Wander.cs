using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotSpeed = 100f;

    bool Wandering = false;
    bool RotatingL = false;
    bool RotatingR = false;
    bool Walking = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Walking == false)
        {
            StartCoroutine(Wander_());
        }
        if (RotatingR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (RotatingL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (Walking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander_()
    {
        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 3);
        int rotLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 5);

        Wandering = true;
        yield return new WaitForSeconds(walkWait);
        Walking = true;
        yield return new WaitForSeconds(walkTime);
        Walking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotLorR == 1)
        {
            RotatingR = true;
            yield return new WaitForSeconds(rotTime);
            RotatingR = false;
        }
        if (rotLorR == 2)
        {
            RotatingL = true;
            yield return new WaitForSeconds(rotTime);
            RotatingL = false;
        }
        Wandering = false;
    }
}
