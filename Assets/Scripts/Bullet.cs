using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject owner;

    [SerializeField]
    int hurtAmount = 1;

    [SerializeField]
    bool destroyOnTouch = true;

    [SerializeField]
    bool destroyOutsideScreen = true;

    private void Update()
    {
        if(destroyOutsideScreen == false)
        {
            return;
        }

        float verticalBound = Camera.main.orthographicSize ;
        float horizontalBound = Camera.main.orthographicSize * Camera.main.aspect;

        if(transform.position.x < -horizontalBound || 
            transform.position.x > horizontalBound ||
            transform.position.y < -verticalBound ||
            transform.position.x > horizontalBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health phealth = collider.gameObject.GetComponent<Health>();
        if (phealth == null)
        {
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
            return;
        }

        if(collider.gameObject != owner)
        {
            phealth.Hurt(hurtAmount);
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
