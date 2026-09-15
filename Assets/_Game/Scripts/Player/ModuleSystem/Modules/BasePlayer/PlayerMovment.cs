using UnityEngine;

[System.Serializable]
public class PlayerMovment : ModuleBase {
    private Rigidbody _rb;

    private float slashT = 0;
    private Vector3 slashDirection;
    private int groundCounter = 0;

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float force = 15f;
    [SerializeField] private float deadZone = 0.1f; //only for joysticks
    [SerializeField] private float defaultDamping = 5;
    [SerializeField] private float damping = 0.5f;
    [SerializeField] private float jumpSpeed = 6;
    [SerializeField] private float jumpForce = 2;
    [SerializeField] private float slashSpeed = 10;
    [SerializeField] private float slashTime = 0.3f;
    [SerializeField] private float slashCooldown = 5;
    [SerializeField] private string groundTag = "Ground";

    public Vector3 direction {get; private set;}
    public Vector3 input {get; private set;}
    public bool Grounded => groundCounter > 0;
    public bool isEnabled = true;
    public bool isWalking {get; private set;} = false;

    public float DeadZone {
        get => deadZone;
        private set => deadZone = value;
    }

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate() {
        if (!isEnabled) return;

        UpdateMoving();
        UpdateInput();        
    }


    private void UpdateInput() {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded) { 
            _rb.linearVelocity += new Vector3(0, jumpSpeed, 0) + direction.normalized * jumpForce;
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
            _rb.linearVelocity = Vector3.zero;
            _rb.MovePosition(player.gameObject.transform.position + (slashDirection * slashSpeed) * Time.deltaTime);
            return;
        }

        if (input.magnitude > deadZone && Grounded) {
            _rb.linearDamping = damping;
            _rb.linearVelocity = Vector3.ClampMagnitude(new Vector3(_rb.linearVelocity.x + direction.x, 0, _rb.linearVelocity.z + direction.z), maxSpeed) + new Vector3(0, _rb.linearVelocity.y, 0);
            isWalking = true;
        } else if (Grounded) {
            _rb.linearDamping = defaultDamping;
            isWalking = false;
        } else {
            _rb.linearDamping = 0;
            isWalking = false;
        }
    }

    public override void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag(groundTag)) groundCounter ++;
    }

    public override void OnCollisionExit(Collision collision) {
        if (collision.collider.gameObject.CompareTag(groundTag)) groundCounter --;
    }
}
