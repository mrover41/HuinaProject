using UnityEngine;

public class PlayerMovment : ModuleBase {
    private Rigidbody _rb;

    private float maxSpeed = 10f;
    private float force = 15f;
    private float deadZone = 0.1f; //only for joysticks
    private float defaultDamping = 5;
    private float damping = 0.5f;
    private float jumpSpeed = 6;
    private float slashSpeed = 10f;
    private LayerMask groundLayer = 1 << 3;

    public bool grounded = true;

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnFixedUpdate() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 direction = (player.gameObject.transform.right * input.x + player.gameObject.transform.forward * input.z).normalized * force * Time.fixedDeltaTime;

        if (input.magnitude > deadZone && grounded) {
            _rb.linearDamping = damping;
            _rb.linearVelocity = Vector3.ClampMagnitude(new Vector3(_rb.linearVelocity.x + direction.x, 0, _rb.linearVelocity.z + direction.z), maxSpeed) + new Vector3(0, _rb.linearVelocity.y, 0);
        } else if (grounded) {
            _rb.linearDamping = defaultDamping;
        } else {
            _rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) { 
            _rb.linearVelocity += new Vector3(0, jumpSpeed, 0);
            grounded = false;
        }

    }

    private bool isGrounded() {
        return Physics.Raycast(player.gameObject.transform.position, Vector3.down, 1.5f, groundLayer);
    }

    public override void OnCollisionEnter(Collision collision) {
        grounded = isGrounded();
    }
}