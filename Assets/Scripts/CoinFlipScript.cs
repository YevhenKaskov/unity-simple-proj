using UnityEngine;

public class CoinFlipScript : MonoBehaviour
{
    public Rigidbody coinRbody;
    public float goUpVelocity = 10.0f;
    public float sideVelocity = 20.0f;
    public float rotationSpeed = 10.0f;
    Vector3 eulerAngleVelocity;
    private bool coinLanded = true;


    void Start()
    {
        coinRbody = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(rotationSpeed * 90, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coinRbody.linearVelocity = Vector3.up * goUpVelocity;
            coinLanded = false;
        }
        if (coinLanded)
        {
            float translationZ = Input.GetAxis("Vertical") * sideVelocity;
            float translationX = Input.GetAxis("Horizontal") * sideVelocity;
            transform.Translate(0, 0, translationZ * Time.deltaTime);
            transform.Translate(translationX * Time.deltaTime, 0, 0);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                coinRbody.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                float rotationX = Input.GetAxisRaw("Vertical") * rotationSpeed * 90;
                float rotationZ = Input.GetAxisRaw("Horizontal") * rotationSpeed * 90;
                eulerAngleVelocity = new Vector3(rotationX, 0, -rotationZ);
                Vector3 userInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                coinRbody.MovePosition(transform.position + userInput * Time.deltaTime * sideVelocity);
            }
        }
    }

    void FixedUpdate()
    {
        if (!coinLanded)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            coinRbody.MoveRotation(coinRbody.rotation * deltaRotation);
            if (coinRbody.position.y < 1)
            {
                coinLanded = true;
                coinRbody.rotation = Quaternion.Euler(0, 0, 0);
                eulerAngleVelocity = new Vector3(rotationSpeed * 90, 0, 0);
            }
        }
    }
}
