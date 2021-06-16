using UnityEngine;

public class playermove : MonoBehaviour
{
    public float jumpForce = 30.0f;
    public float movementSpeed = 1.0f;
    public bool facingRight = true;
    public Animator _animator;
    public Camera _camera;

    private Rigidbody2D _rigidbody;
    private Vector2 movement = Vector3.zero;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(movement.y < 0 && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            movementSpeed = 0;
        }
        else
        {
            movementSpeed = 6;
        }
        //transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * movementSpeed;

        if(_rigidbody.position.x > -6)
        {
            _camera.transform.position = new Vector3(transform.position.x /*+ (movement.x * movementSpeed)/5*/, 1, -10);
        }
        
        _animator.SetFloat("speed", Mathf.Abs(movement.x));
        _animator.SetBool("grounded", Mathf.Abs(_rigidbody.velocity.y) < 0.001f);
        _animator.SetBool("downPushed", movement.y < 0);

        float grav;
        if (Input.GetButton("Jump") || _rigidbody.velocity.y < 0)
        {
            grav = 3;
        }
        else
        {
            grav = 15;
        }

        _rigidbody.gravityScale = grav;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce /*+ Mathf.Abs(movement.x * Time.deltaTime * movementSpeed)*/), ForceMode2D.Impulse);
        }

        if (facingRight && movement.x < 0 || !facingRight && movement.x > 0)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * movementSpeed);
    }

}
