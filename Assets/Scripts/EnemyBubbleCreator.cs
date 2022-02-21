using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubbleCreator : MonoBehaviour
{
    [SerializeField] private int _bubbles;
    [SerializeField] private Bubble _bubblePrefab;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Transform _transform;
    private Mesh _currentMesh;

    private List<Bubble> _bubblesList = new List<Bubble>();

    private void Start()
    {
        _transform = transform;
        _currentMesh = new Mesh();

        for (int j = 0; j < _bubbles; j++)
        {
            Bubble newBubble = Instantiate(_bubblePrefab);
            _bubblesList.Add(newBubble);
        }

        StartCoroutine(PutBubblesOnBakedMesh());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovementHandler>())
        {
            for (int i = 0; i < _bubblesList.Count; i++)
            {
                _bubblesList[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                _bubblesList[i].gameObject.GetComponent<Rigidbody>().drag = 1;
                _bubblesList[i].gameObject.GetComponent<Rigidbody>().angularDrag = 0.05f;
                _bubblesList[i].gameObject.GetComponent<Rigidbody>().AddForce(other.transform.forward * 3f, ForceMode.VelocityChange);
            }
        }
    }

    private IEnumerator PutBubblesOnBakedMesh()
    {
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.BakeMesh(_currentMesh, true);
        Matrix4x4 localToWorld = _transform.localToWorldMatrix;
        Vector3[] vertices = _currentMesh.vertices;

        int previousPosition = 0;
        for (int j = 0; j < _bubbles; j++)
        {
            int randomPostion = Random.Range(j, vertices.Length);

            while(randomPostion == previousPosition)
            {
                randomPostion = Random.Range(0, vertices.Length);
            }

            Vector3 worldVertex = localToWorld.MultiplyPoint3x4(vertices[randomPostion]);
            _bubblesList[j].transform.position = worldVertex;
            previousPosition = randomPostion;
        }
    }
}
