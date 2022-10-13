using UnityEngine;
using TMPro;

public class BulletBehavior : MonoBehaviour
{
 //bullet
 public GameObject bullet;

 //bullet force
 public float shootForce, upwardForce;

 //Gun stats
 public float timeBetweenShooting, spread, reloadTime,TimeBetweenShots;
 public int magazineSize, bulletPerTap;
 public bool allowButtonHold;

 int bulletLeft, bulletShot;

 //bools
 bool shooting, readyToShoot, reloading;

 //Reference 
 public Camera fpsCam;
 public Transform attackPoint;
 public GameObject CamObj;

 //Graphics 
 public GameObject muzzleFlash;
 public TextMeshProUGUI ammunitionDisplay;
 public GameObject InstructionImage;

 //bug fixing
 public bool allowInvoke = true;

 private void Awake()
 {
     //make sure magazine is full
     bulletLeft = magazineSize;
     readyToShoot = true;
 }

 private void Update()
 {
     //MyInput();
     //Set ammo display, if it exists
     if(ammunitionDisplay != null)
        ammunitionDisplay.SetText(bulletLeft / bulletPerTap + "/" + magazineSize / bulletPerTap);
        

 }
 
 public void MyInput()
     {
         //Check if allowed to hold down button and take corresponding input
         if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
         else shooting = Input.GetKeyDown(KeyCode.Mouse0);
         
         //Reloading
         if(Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading) Reload();
         //Reload auromatically when trying to shoot without ammo
         if(readyToShoot && shooting && !reloading && bulletLeft <= 0 ) Reload();
         //shooting
         if(readyToShoot && shooting && !reloading && bulletLeft > 0)
         {
             //Set bullets shot to 0
             bulletShot = 0;
             Shoot();
         } 
     }
          public void Shoot()
     {
         readyToShoot = false;
         //Find the exact hit position using a RayCast
         Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        //check if ray hits something 
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
          targetPoint = hit.point;  
          
        }
            
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from player
            
            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            //Calculate spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread,spread);

            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiate

            //Rotate bullet to shot direction
            currentBullet.transform.forward = directionWithSpread.normalized;
            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            //Instantiate muzzle flash, if you have one
            if(muzzleFlash != null)
                Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
         bulletLeft--;
         bulletShot++;
 //invoke resetShot funstion (if not already invoked)
     if(allowInvoke)
     {
         Invoke("ResetShot", timeBetweenShooting);
         allowInvoke = false;
     }
     
     //if more than one bulletPerTap make shure to repeat shoot function
     if(bulletShot < bulletPerTap && bulletLeft > 0)
        Invoke("Shoot", TimeBetweenShots);

     }

     private void ResetShot()
     {
         readyToShoot = true;
         allowInvoke = true;

     }
     private void Reload()
     {
         reloading = true;
         Invoke("ReloadingFinished", reloadTime);
     }

     private void ReloadFinished()
     {
         bulletLeft = magazineSize;
         reloading = false;
     }
    /*void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ghost_1")
        {
            //CamObj.transform.LookAt(other);
        }
    }
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.name)
        {
            CamObj.transform.LookAt(collision.gameObject.transform);
        }
    }*/

}
