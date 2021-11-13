using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform spaceship;
    [SerializeField] private float timeZerotoMax;
    [SerializeField] private Rigidbody rb;
    private float rotationSpeed = 80f;
    private float yRotation = 0.0f;
    private float AccelerationperSec;
    [SerializeField] public float currentSpeed = 0.0f;
    private float boostLimit;
    private float boostSpeed;
    private bool boost = false;
    // Start is called before the first frame update

    private void Awake()
    {
        LockCursor();
        AccelerationperSec = (maxSpeed-minSpeed) / timeZerotoMax;
        boostSpeed = AccelerationperSec * 0.50f;
        boostLimit = maxSpeed * .15f;

    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Shoot_Is_Pressed();
        Movement();
    }

    private void Movement()
    {
        float roatationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float roatationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float roll = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        spaceship.Rotate(Vector3.up * roatationX);
        spaceship.Rotate(Vector3.forward * roatationY * -1);
        spaceship.Rotate(Vector3.right * roll);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            boost = true;
            maxSpeed += boostLimit;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            boost = false;
            maxSpeed -= boostLimit;
        }

        if (Input.GetKey("w") || boost)
        {
            currentSpeed += (AccelerationperSec + (boost ? boostSpeed : 1.0f)) * Time.deltaTime;
            //move = Mathf.Min(move, maxSpeed);

        }
        else if (Input.GetKey("s") && !boost)
        {
            currentSpeed -= (AccelerationperSec * Time.deltaTime);
        }
        else if (!boost)
        {
            currentSpeed *= 0.999f;
        }



        currentSpeed = Clamp(currentSpeed, minSpeed, maxSpeed);
        rb.transform.position += transform.forward * currentSpeed * Time.deltaTime;

        //Debug.Log(rb.velocity);
    }

    private void Shoot_Is_Pressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.StarFighter.ON_FIRE_FIRE_BLASTER);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.StarFighter.ON_FIRE_STOP_BLASTER);
        }
    }

    private float Clamp(float temp, float low, float high)
    {
        if (temp < low)
            return low;
        else if (temp > high)
            return high;
        else
            return temp;
    }

    public void Disable()
    {
        this.minSpeed = 0;
        this.maxSpeed = 0;
        this.boostLimit = 0;
    }

    public bool getBoost()
    {
        return boost;
    }
}

