using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCubeScript : MonoBehaviour
{
    public GameObject GroundPuff;
    public GameObject CubePuff;
    public GameObject CubeDestroyPuff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FlyingCubeScript>() != null)
        {
            collision.gameObject.GetComponent<FlyingCubeScript>().CubeToCubePuff();
        }
    }
    public void HitGround()
    {
        Instantiate(GroundPuff, transform.position, Quaternion.identity);
    }
    public void CubeToCubePuff()
    {
        Instantiate(CubePuff, transform.position, Quaternion.identity);
    }
    public void BreakCube()
    {
        Instantiate(CubeDestroyPuff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
