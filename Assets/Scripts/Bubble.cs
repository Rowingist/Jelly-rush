using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void Die()
    {
        StartCoroutine(DieEffect());
    }

    private IEnumerator DieEffect()
    {
        Vector3 scale = transform.localScale;

        float time = 0f;
        const float duration = 1f;

        _meshRenderer.material.color = Color.gray;
        GetComponent<Rigidbody>().useGravity = false;

        while (time < 1f)
        {
            transform.localScale = Vector3.Lerp(scale, Vector3.zero, time);
            time += Time.deltaTime / duration;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void IgnoreCollisionWith(Collider collider)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), collider);
    }
}
