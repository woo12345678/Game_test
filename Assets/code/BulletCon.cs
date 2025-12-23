using UnityEngine;

public class BulletCon : MonoBehaviour
{

    public float speed = 18f;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    
}
