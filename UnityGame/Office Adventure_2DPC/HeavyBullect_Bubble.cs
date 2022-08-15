using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeavyBullect_Bubble : MonoBehaviour
{
    [SerializeField] GameObject bullect;
    [SerializeField] GetComponent gcp;
    private void Start()
    {
        gcp = GameObject.Find("LevelLoad").GetComponent<GetComponent>();
        StartCoroutine(ExampleCoroutine());
    }

    void Shoot()
    {
         GameObject bullets = 
           Instantiate(bullect, gcp.Weapon.transform.position, Quaternion.Euler
           (gcp.Weapon.spawnPos.rotation.eulerAngles.x, gcp.Weapon.spawnPos.rotation.eulerAngles.y, gcp.Weapon.spawnPos.rotation.eulerAngles.z + Random.Range(-28, 28))) ;

       Rigidbody2D rb = bullets.GetComponent<Rigidbody2D>();
        rb.AddForce(gcp.Weapon.spawnPos.up * gcp.Weapon.direction, ForceMode2D.Impulse);

    }
    void Del()
    {
        Destroy(this.gameObject, 2f);
    }


    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.1f);
        Shoot();

        yield return new WaitForSeconds(.1f);
        Shoot(); Shoot();

        yield return new WaitForSeconds(.1f);
        Shoot();

        yield return new WaitForSeconds(.1f);
        Shoot(); Shoot();

        yield return new WaitForSeconds(.1f);
        Shoot();

        yield return new WaitForSeconds(.1f);
        Shoot(); Shoot();

        Del(); Debug.Log("Del");

    }
}
