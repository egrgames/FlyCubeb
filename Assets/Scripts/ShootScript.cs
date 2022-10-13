using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class ShootScript : MonoBehaviour
{

    public float Speed = 10f;
    public float PeriodOfTime = 3f;
    public float MaxDistance = 15f;

    public GameObject CubeToShoot;
    public GameObject InputFieldSpeed;
    public GameObject InputFieldTime;
    public GameObject InputFieldDistance;
    public GameObject Wall;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("shoot", PeriodOfTime);
        TouchScreenKeyboard.Open("MaxDistance");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void EndSpeed(string SpeedString)
    {
        Debug.Log(SpeedString);
        Speed = float.Parse(SpeedString, CultureInfo.InvariantCulture.NumberFormat);
    }
    public void EndTime(string PeriodOfTimeString)
    {
        PeriodOfTime = float.Parse(PeriodOfTimeString, CultureInfo.InvariantCulture.NumberFormat);
    }
    public void EndDistance(string MaxDistanceString)
    {
        MaxDistance = float.Parse(MaxDistanceString, CultureInfo.InvariantCulture.NumberFormat);
        Wall.transform.position = new Vector3(-9f, 0f, 10.3f + MaxDistance);
    }
    void shoot()
    {
        GameObject FlyingCube = Instantiate(CubeToShoot, transform.position, Quaternion.identity);
        FlyingCube.GetComponent<Rigidbody>().AddForce(FlyingCube.transform.forward.normalized * Speed, ForceMode.Impulse);
        Invoke("shoot", PeriodOfTime);
    }
}
