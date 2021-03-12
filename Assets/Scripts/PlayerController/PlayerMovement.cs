using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float MoveSpeed;
    public GameObject bulletPrefab;
    public GameObject particleSplash;
    public Transform firePos;
    public FixedJoystick joystick;
    public FixedJoystick rotateJoystick;
    public bool rotatebool;
    public bool canShoot;


    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementJoystick();//movement function
        RotateJoystick();// rotation function
        if (rotatebool && canShoot)
        {
            InvokeRepeating("FireBullet", 0, 0.8f);// if rotating only then it will shoot
        }
        else if(!rotatebool)
        {
            CancelInvoke();
        }
    }
    void FireBullet()
    {
        canShoot = false;
        PoolManager.instance.UseObject(bulletPrefab, firePos.position, firePos.rotation);//object pooling
    }

    void MovementJoystick()
    {
        float hAxis = joystick.Horizontal;
        float vAxis = joystick.Vertical;
        Vector2 convertedXY = ConvertAxisToCameraDirection(Camera.main.transform.position, hAxis, vAxis);

        Vector3 direction = new Vector3(convertedXY.x, 0, convertedXY.y).normalized;
        transform.Translate(direction * MoveSpeed * Time.fixedDeltaTime, Space.World);
        if (!rotatebool)
        {
            //if user not rotating manually then the player moves with left control
            Vector3 lookPos = transform.position + direction;
            transform.LookAt(lookPos);
        }
    }
    void RotateJoystick()
    {
        float hAxis = rotateJoystick.Horizontal;
        float vAxis = rotateJoystick.Vertical;
        if (hAxis == 0 || vAxis == 0)
        {
            canShoot = true;
            rotatebool = false;

        }
        else
        {
            rotatebool = true;
        }
        Vector2 convertedXY = ConvertAxisToCameraDirection(Camera.main.transform.position, hAxis, vAxis);

        Vector3 direction = new Vector3(convertedXY.x, 0, convertedXY.y).normalized;
        Vector3 lookPos = transform.position + direction;
        transform.LookAt(lookPos);
    }
    

    Vector2 ConvertAxisToCameraDirection(Vector3 camPos,float hAxis, float vAxis )
    {
        Vector2 joystickDirection = new Vector2(hAxis,vAxis).normalized;
        Vector2 camPosIn2D = new Vector2(camPos.x, camPos.z);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 cameraToPlayerDirection = (Vector2.zero - camPosIn2D).normalized;
        float angle = Vector2.SignedAngle(cameraToPlayerDirection, new Vector2(0, 1));
        Vector2 finalDirection = RotateVector(joystickDirection, - angle);
        return finalDirection;
    }

    Vector2 RotateVector(Vector2 vector,float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float _x = vector.x * Mathf.Cos(rad) - vector.y * Mathf.Sin(rad);
        float _y = vector.x * Mathf.Sin(rad) + vector.y * Mathf.Cos(rad);
        return new Vector2(_x, _y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            PoolManager.instance.ReturnObject(collision.gameObject);
            obj = PoolManager.instance.UseObject(particleSplash, collision.transform.position, collision.transform.rotation);
            obj.SetActive(true);
            ParticleSystem particle = obj.GetComponent<ParticleSystem>();
            particle.Play();
            PoolManager.instance.ReturnObject(obj, 1f);
            /// return the particleffect back to pool
        }
    }
   
}
