using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubblesMagnet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private Collider _playerPhysycsCollider;

    private List<Bubble> _bubbles = new List<Bubble>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bubble bubble))
        {
            if (_bubbles.Contains(bubble) == false)
            {
                _bubbles.Add(bubble);
                bubble.transform.parent = null;
                bubble.IgnoreCollisionWith(_playerPhysycsCollider);
                bubble.GetComponent<Rigidbody>().isKinematic = false;
                bubble.GetComponent<Rigidbody>().drag = Random.Range(8f, 20f);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach(Bubble bubble in _bubbles)
        {
            Rigidbody rigidbody = bubble.GetComponent<Rigidbody>();
            Transform bubbleTransform = bubble.GetComponent<Transform>();

            if(rigidbody != null)
            {
                Vector3 towardsTarget = bubbleTransform.localPosition - transform.position;
                rigidbody.velocity += towardsTarget.normalized * -_force * Time.fixedDeltaTime;
            }

            if(Vector3.Distance(bubble.transform.position, transform.position) >= 10f)
            {
                bubble.Die();
                _bubbles.Remove(bubble);
            }
        }
    }

    public int GetBubblesCount()
    {
        return _bubbles.Count;
    }

}
