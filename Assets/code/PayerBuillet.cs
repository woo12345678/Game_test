using UnityEngine;

public class PayerBuillet : MonoBehaviour
{
    public float speed = 18f;
    public float lifeTime = 4f;

    private Vector3 dir;
    private bool locked;

    public void SetDirection(Vector3 direction)
    {
        dir = direction.normalized;
        locked = true;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (!locked) return;
        transform.position += dir * speed * Time.deltaTime;
    }

    
}
