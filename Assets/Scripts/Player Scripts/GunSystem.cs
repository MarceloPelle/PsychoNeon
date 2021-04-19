using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //Gun stats
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots,shootForce,bulletSpeed,upwardForce;
    public float reloadAnimationTime;
    public int   maxMagazine;
    public int   magazine, ammo, mags, bulletsPerTap;
    public bool  allowButtonHold;
    int          bulletsShot;

    private int  magazineTemp;

    //bools 
    bool shooting, readyToShoot, isReloading = false;

    //Referencias
    public Camera     fpsCam;
    public Transform  attackPoint;
    public RaycastHit rayHit;
    public LayerMask  whatIsEnemy;

    //Graficos
    public GameObject      muzzleFlash;
    public TextMeshProUGUI magazineText;
    public TextMeshProUGUI ammoText;
    public CameraShake     camShake;
    public float           camShakeMagnitude, camShakeDuration;

    //Animator
    public Animator anim;
    
    private void Awake()
    {
        ammo = magazine * mags;
        magazineTemp = magazine;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        magazineText.SetText(magazine + "");

        //Texto que dice la munición 
        ammoText.SetText(ammo + "");
    }
    private void MyInput()
    {
        if (allowButtonHold) 
            shooting = Input.GetKey(KeyCode.Mouse0);
        else 
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo > 0 && magazine != maxMagazine)
        {
            reloadTime = reloadAnimationTime;
            anim.SetInteger("Reloading", 1);
            isReloading = true;
        }
        if(isReloading && reloadTime <= 1 && magazine != maxMagazine)
        {  
            reloadTime = 0;
            anim.SetInteger("Reloading", -1);
            isReloading = false;
            ammo = ammo - maxMagazine + magazine;
            magazine = magazineTemp;
            if(ammo < 0)
            {
                magazine += ammo;
                ammo = 0;
            }
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }



        if (ammo <= 0 && magazine <= 0)
        {
            readyToShoot = false;
        }

        anim.SetBool("SinBalas", magazine <= 0);

        //Shoot
        if (readyToShoot && shooting && !isReloading && magazine > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        magazine--;
        bulletsShot--;

        anim.SetBool("Shoot", true);
        readyToShoot = false;

        //recoil
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Cualcula Direccion de el recoil
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast

        //Find the exact hit position using raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));// ray in middle screen
        RaycastHit hit;

        //check if ray hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to TargetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bulet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bulleto to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce * bulletSpeed, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce * bulletSpeed, ForceMode.Impulse);


        //ShakeCamera
        StartCoroutine(camShake.Shake(camShakeDuration,camShakeMagnitude));

        //Graficos
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && magazine > 0)
            Invoke("Shoot", timeBetweenShots);
        
    }
    private void ResetShot()
    {
        readyToShoot = true;
        anim.SetBool("Shoot", false);
    }
 
}
