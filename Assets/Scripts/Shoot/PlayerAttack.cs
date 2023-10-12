using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float shotTimer;
    public GameObject playerShot;
    public Transform shotParent;
    private bool canShoot;
    private bool shootUp;
    private bool shootDown;
    private bool shootLeft;
    private bool shootRight;
    private Vector2 shootDir;
    //public SFX sfx;

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (canShoot)
        {
            shootUp = Input.GetKey(KeyCode.UpArrow);
            shootDown = Input.GetKey(KeyCode.DownArrow);
            shootLeft = Input.GetKey(KeyCode.LeftArrow);
            shootRight = Input.GetKey(KeyCode.RightArrow);
            shootDir = Vector2.zero;

            if (shootUp)
            {
                shootDir.y = 1;
                // sfx.PlaySound("shot");
            }
            else if (shootDown)
            {
                shootDir.y = -1;
                // sfx.PlaySound("shot");
            }

            else if (shootRight)
            {
                shootDir.x = 1;
                // sfx.PlaySound("shot");
            }
            else if (shootLeft)
            {
                shootDir.x = -1;
                // sfx.PlaySound("shot");
            }

            if (shootDir != Vector2.zero)
            {
                Instantiate(playerShot, transform.position, Quaternion.LookRotation(Vector3.forward, shootDir), shotParent);
                StartCoroutine(ShotDelay());
            }
        }
    }

    IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotTimer);
        canShoot = true;
    }
}
