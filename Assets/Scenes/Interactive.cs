using UnityEngine;

public class PhysicsSandbox : MonoBehaviour
{
    public GameObject cubePiecePrefab;
    public float explodeForce = 500f;
    public Material materialUp;
    public Material materialCollision;
    public Transform sphere;
    public float attractionForce = 10f;
    private Rigidbody rb;
    private Rigidbody sphereRb;
    private bool isAttracting = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Material originalMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (sphere != null)
            sphereRb = sphere.GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalMaterial = GetComponent<Renderer>().material;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) { rb.mass = 1f; }
        if (Input.GetKeyDown(KeyCode.E)) { rb.mass = 100f; }
        if (Input.GetKeyDown(KeyCode.N)) { rb.drag = 0f; }
        if (Input.GetKeyDown(KeyCode.A)) { rb.drag = 10f; }
        if (Input.GetKeyDown(KeyCode.V)) { rb.angularDrag = 0f; }
        if (Input.GetKeyDown(KeyCode.S)) { rb.angularDrag = 2f; }
        if (Input.GetKeyDown(KeyCode.U)) { GetComponent<Renderer>().material = materialUp; }
        if (Input.GetKeyDown(KeyCode.D)) { isAttracting = !isAttracting; }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Rigidbody[] allBodies = FindObjectsOfType<Rigidbody>();
            foreach (Rigidbody body in allBodies)
                body.useGravity = false;
        }
        if (Input.GetKeyDown(KeyCode.F)) { rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY; }
        if (Input.GetKeyDown(KeyCode.K)) { rb.isKinematic = !rb.isKinematic; }
        if (Input.GetKeyDown(KeyCode.B)) { ExplodeCube(); }
        if (Input.GetKeyDown(KeyCode.C)) { ResetCube(); }
        
    }

    void FixedUpdate()
    {
        if (isAttracting && sphereRb != null)
        {
            Vector3 direction = transform.position - sphere.position;
            sphereRb.AddForce(direction.normalized * attractionForce);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == sphere.gameObject)
        {
            GetComponent<Renderer>().material = materialCollision;
            sphere.GetComponent<Renderer>().material = materialCollision;
        }
    }

    void ResetCube()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.mass = 1f;
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void ExplodeCube()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Vector3 p = transform.position + new Vector3(x, y, z) * 0.5f;
                    GameObject piece = Instantiate(cubePiecePrefab, p, Quaternion.identity);
                    Rigidbody pieceRb = piece.GetComponent<Rigidbody>();
                    pieceRb.AddExplosionForce(explodeForce, transform.position, 5f);
                }
            }
        }
        Destroy(gameObject);
    }
}