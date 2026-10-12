using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionalShooter : MonoBehaviour
{
    [SerializeField]
    Key fireButton;

    [SerializeField]
    float bulletSpeed = 10f;

    [SerializeField]
    float coolDown;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float spawnDistance = 0;


    float timer = 0;

    PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if ((fireButton == Key.None || Keyboard.current[fireButton].isPressed) && timer <= 0)
        {
            Vector2 direction = Vector2.right;
            if (pm != null)
            {
                direction = pm.GetFacingDirection();
            }

            Vector2 pos = (Vector2)transform.position + direction*spawnDistance;
            

            GameObject spawnedObject = Instantiate(prefab, pos, Quaternion.identity);
            spawnedObject.GetComponent<Bullet>()?.SetOwner(gameObject);

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction*bulletSpeed;
            }


            timer = coolDown;
        }
    }
}
