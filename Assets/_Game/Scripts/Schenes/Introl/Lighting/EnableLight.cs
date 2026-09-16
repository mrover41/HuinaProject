using System.Collections;
using UnityEngine;

public class EnableLight : MonoBehaviour {
    [SerializeField] private float beginDelay;
    [SerializeField] private float enableDelay;
    [SerializeField] private GameObject[] objects;    

    void Start() {
        objects[0].SetActive(true);
        StartCoroutine(DelayedLoop());
    }

    private IEnumerator DelayedLoop() {
        yield return new WaitForSeconds(beginDelay);

        foreach (GameObject obj in objects) {
            obj.SetActive(true);
            yield return new WaitForSeconds(enableDelay);
        }
    }
}
