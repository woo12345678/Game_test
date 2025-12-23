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
        // 회전된 방향(앞)으로만 직진
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        
       
    }
}
