using UnityEngine;

public class PlayerMovment : ModuleBase {
    private Rigidbody _rb;

    private float maxSpeed = 10f;
    private float force = 15f;
    private float deadZone = 0.1f; //only for joysticks
    private float defaultDamping = 5;
    private float damping = 0.5f;
    private float jumpSpeed = 6;

    public bool isGrounded = true;

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnFixedUpdate() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 direction = (player.gameObject.transform.right * input.x + player.gameObject.transform.forward * input.z).normalized * force * Time.fixedDeltaTime;

        if (input.magnitude > deadZone && isGrounded) {
            _rb.linearDamping = damping;
            _rb.linearVelocity = Vector3.ClampMagnitude(new Vector3(_rb.linearVelocity.x + direction.x, 0, _rb.linearVelocity.z + direction.z), maxSpeed) + new Vector3(0, _rb.linearVelocity.y, 0);
        } else if (isGrounded) {
            _rb.linearDamping = defaultDamping;
        } else {
            _rb.linearDamping = 0;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) { 
            _rb.linearVelocity += new Vector3(0, jumpSpeed, 0);
            isGrounded = false;
        }
    }

    public override void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    public override void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}