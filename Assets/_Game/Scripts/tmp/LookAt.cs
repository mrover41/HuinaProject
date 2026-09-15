using UnityEngine;

public class LookAt : MonoBehaviour {
    [SerializeField] private Transform transform;

    void Update() {
        this.gameObject.transform.LookAt(transform.position);
    }
}
