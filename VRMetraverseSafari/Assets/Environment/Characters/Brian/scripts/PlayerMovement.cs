using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float sprintSpeed;

    public float jumpForce;

    private bool canJump;
    private Vector2 turn;
    private float zPosition;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        playerMove();
    }
    public void playerMove()
    {
        zPosition = Input.GetAxis("Vertical");
        Vector3 Move = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        if(zPosition < 0)
        {
            Move = new Vector3(0f, 0f, 0f);
        }
        Move = Vector3.ClampMagnitude(Move, 1);

        /*transform.Translate(Move * Time.deltaTime * moveSpeed);*/
        if (Input.GetKey("w"))
        {
            turn.x = Input.GetAxis("Mouse X");
            /*turn.y = Input.GetAxis("Mouse Y");*/
            transform.Rotate(new Vector3(0, turn.x * 5, 0));
        }
        if (!Input.GetKey("w"))
        {
            /*turn.x = Input.GetAxis("Mouse X");
            turn.y = Input.GetAxis("Mouse Y");*/
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Move * Time.deltaTime * sprintSpeed);
        }
        else
        {
            transform.Translate(Move * Time.deltaTime * moveSpeed);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump == true)
            {
                Rigidbody2D playerRB = transform.GetComponent<Rigidbody2D>();
                playerRB.AddForce(Vector2.up * jumpForce);
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
}