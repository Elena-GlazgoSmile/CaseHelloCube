using UnityEngine;

public class HelloWorldManager : MonoBehaviour
{
    public GameObject cube;
    private bool hasJumped = false;
    private float jumpForce = 5f;

    void Start()
    {
        Debug.Log("HelloWorld");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cube != null)
            {
                if (!hasJumped)
                {
                    Jump();
                    hasJumped = true;
                    Debug.Log("Прыжок");
                }
                else
                {
                    Destroy(cube);
                    Debug.Log("Удаление");
                }
            }
            
        }
    }

    void Jump()
    {
        Rigidbody rb = cube.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = cube.AddComponent<Rigidbody>();
        }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}