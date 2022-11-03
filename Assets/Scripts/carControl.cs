using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControl : MonoBehaviour
{
    public float carSpeed;
    //public float maxPos = 2.75f;
    Vector3 position;
    public uiManager ui;
    public audioManager am;

    bool currentPlatformAndroid = false;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
#if UNITY_ANDROID
        currentPlatformAndroid = true;
#else
        currentPlatformAndroid = false;
#endif

    }

    // Start is called before the first frame update
    void Start()
    {
        am.carSound.Play();
        ///ui = GetComponent<uiManager>();
        position = transform.position;

        if (currentPlatformAndroid == true)
        {
            Debug.Log("Android");
        }
        else
        {
            Debug.Log("Windows");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlatformAndroid == true)
        {
            //android specific code
            //TouchMove();
            AccelerometerMove();

        }

        else
        {
            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
            
        }

        position = transform.position;
        position.x = Mathf.Clamp(position.x, -2.55f, 2.55f);
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy Car")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            ui.gameOverActivate();
            am.carSound.Stop();
        }
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-carSpeed, 0);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(carSpeed, 0);
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }

    void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width / 2;
            if(touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                MoveLeft();
            }
            else if (touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                MoveRight();
            }
        }
        else
        {
            SetVelocityZero();
        }
    }

    void AccelerometerMove()
    {
        float x = Input.acceleration.x;
        Debug.Log("X = " + x);
        if (x < -0.1f)
        {
            MoveLeft();
        }
        else if (x > +0.1f)
        {
            MoveRight();
        }
        else
        {
            SetVelocityZero();
        }
    }
}
