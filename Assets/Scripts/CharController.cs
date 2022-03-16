using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    float movespeed = 4; // Test

    bool canMove = true;

    Vector3 forward, right;

    // Start is called before the first frame update
    void Start()
    {
        CheckCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && canMove)
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckCameraPosition();
        }
    }

    void CheckCameraPosition()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = Vector3.Lerp(transform.forward, heading, 0.1f);
        transform.position += rightMovement;
        transform.position += upMovement;


    }

    public void SetCanMove(bool i)
    {
        canMove = i;
    }
}
