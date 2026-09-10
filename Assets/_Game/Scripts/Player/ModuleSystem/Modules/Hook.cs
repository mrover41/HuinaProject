using UnityEngine;


[System.Serializable]
public class Hook : ModuleBase {
    private bool hooking = false;
    private bool isFinded = false;

    private Vector3 pos;
    private Rigidbody _rb;
    private LineRenderer lineRenderer;

    [SerializeField] private float maxLenght = 200;
    [SerializeField] private float speed = 2500;
    [SerializeField] private LayerMask hookLayer = 1 << 6;
    [SerializeField] private float maxDistance = 50;

    public override void OnEnable(Player pl) {
        _rb = pl.gameObject.GetComponent<Rigidbody>();
        lineRenderer = pl.gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = Color.black;
        lineRenderer.material = lineMaterial;
    }

    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.F)) {
            lineRenderer.enabled = true;
            hooking = true;
        }


        if (Input.GetKeyUp(KeyCode.F)) {
            hooking = false;
            isFinded = false;

            lineRenderer.enabled = false;
        }
    }

    public override void OnFixedUpdate() {
        if (hooking) {
            Transform c = Camera.main.gameObject.transform;
            if (!isFinded && Physics.Raycast(c.position, c.forward, out RaycastHit hit, maxLenght) && hit.collider.gameObject.CompareTag("Hook")) {
                pos = hit.point;
                isFinded = true;

                lineRenderer.SetPosition(1, hit.point);
            } else if (isFinded) {
                lineRenderer.SetPosition(0, c.position + Vector3.down);
                _rb.AddForce((pos - c.position).normalized * speed * Time.fixedDeltaTime);
            }
        }
    }
}
