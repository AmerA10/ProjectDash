using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Maybe can use object pooling for something like this
public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float timeToShoot;
    [SerializeField] Transform shootPos;
    private float timer;
    [SerializeField] float speed;
    Vector2 shootDir;

    bool canShoot = true;

    //The logic behind this script is to shoot the projectile at a set amount of time in a stright direction * speed



    private void Awake()
    {

        shootDir = this.transform.right;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToShoot;

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToShoot)
        {
            ShootProjectile();
            timer = 0f;
        }
    }

    private void ShootProjectile()
    {
        GameObject proj = Instantiate(projectile, shootPos.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = this.transform.right * speed;

    }

}
