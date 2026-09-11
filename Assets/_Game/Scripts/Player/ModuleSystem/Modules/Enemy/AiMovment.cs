using UnityEngine.AI;
using UnityEngine;

[System.Serializable]
public class AiMovment : ModuleBase {
    private NavMeshAgent agent;
    //private bool isFound = false;

    [SerializeField] private Transform target;
    

    public override void OnEnable(Player pl) {
        target = ScheneManager.Instance.PlayerInstance.transform; //player.transform it is enemy
        agent = pl.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void OnFixedUpdate() {
        /*if (target != null && Physics.Linecast(player.gameObject.transform.position, target.position, out RaycastHit hitInfo)) {
            if (hitInfo.transform.CompareTag("Player")) {
                //if (isFound) isFound = false;
                agent.destination = target.position;
                return;
            }
        }*/
        agent.destination = target.position;
    }
}
