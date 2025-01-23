using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] nodes; //array of nodes (et in the Inspector)
    public float speed = 2f;
    private int currentNodeIndex = 0;
    public Spwaner spwaner;
    // Update is called once per frame
    void Update()
    {
        if (nodes.Length == 0) return;

        //get the current nodes position
        Transform targetNode = nodes[currentNodeIndex];
        Vector3 targetPosition = targetNode.position;

        //move towards the target node
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition)< 0.1f)
        {
            currentNodeIndex++;
            if (currentNodeIndex >= nodes.Length)
            {
                Destroy(gameObject);
                spwaner.EnemyDied();
            }
        }
    }
    public void SetNodes(Transform[] newNodes)
    {
        nodes = newNodes;
        currentNodeIndex = 0; // Reset to start at the first node
    }
    private void OnDrawGizmos()
    {
        if (nodes == null || nodes.Length < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            if (nodes[i] != null && nodes[i + 1] != null)
            {
                Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
            }
        }
    }
}
