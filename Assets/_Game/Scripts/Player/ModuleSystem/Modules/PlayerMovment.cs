using UnityEngine;

[System.Serializable]
public class PlayerMovment : ModuleBase {
    private Rigidbody _rb;

    private float slashT = 0;
    private Vector3 slashDirection;

    private Vector3 input;
    private Vector3 direction;

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float force = 15f;
    [SerializeField] private float deadZone = 0.1f; //only for joysticks
    [SerializeField] private float defaultDamping = 5;
    [SerializeField] private float damping = 0.5f;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float slashSpeed = 10;
    [SerializeField] private float slashTime = 0.3f;
    [SerializeField] private float slashCooldown = 5;
    [SerializeField] private LayerMask groundLayer = 1 << 3;

    public bool grounded {get; private set;} = true;
    public bool isEnabled = true;

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate() {
        if (!isEnabled) return;

        grounded = isGrounded();

        UpdateMoving();
        UpdateInput();        
    }


    private void UpdateInput() {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) { 
            _rb.linearVelocity += new Vector3(0, jumpSpeed, 0);
        } if (Input.GetKeyDown(KeyCode.LeftShift) && slashT + slashTime <= Time.time) {
            slashT = Time.time;
            slashDirection = direction.normalized * slashSpeed;
            if (slashDirection.magnitude == 0) slashDirection = player.gameObject.transform.forward;
        }
    }

    private void UpdateMoving() {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = (player.gameObject.transform.right * input.x + player.gameObject.transform.forward * input.z).normalized * force * Time.deltaTime;

        if (slashT + slashTime >= Time.time) {
            _rb.MovePosition(player.gameObject.transform.position + (slashDirection * slashSpeed) * Time.deltaTime);
            return;
        }

        if (input.magnitude > deadZone && grounded) {
            _rb.linearDamping = damping;
            _rb.linearVelocity = Vector3.ClampMagnitude(new Vector3(_rb.linearVelocity.x + direction.x, 0, _rb.linearVelocity.z + direction.z), maxSpeed) + new Vector3(0, _rb.linearVelocity.y, 0);
        } else if (grounded) {
            _rb.linearDamping = defaultDamping;
        } else {
            _rb.linearDamping = 0;
        }
    }

    private bool isGrounded() {
        return Physics.Raycast(player.gameObject.transform.position, Vector3.down, 1.5f, groundLayer);
    }

    /*public override void OnCollisionEnter(Collision collision) {
        grounded = isGrounded();
    }*/
}
