using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Key fireButton;

    [SerializeField]
    Vector2 fireDirection = Vector2.zero;

    [SerializeField]
    float coolDown;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Transform spawnLocation;

    Animator anim;


    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if ((fireButton == Key.None || Keyboard.current[fireButton].isPressed) && timer <= 0)
        {
            Vector2 pos = transform.position;
            if (spawnLocation != null)
            {
                pos = spawnLocation.position;
            }

            GameObject spawnedObject = Instantiate(prefab, pos, Quaternion.identity);
            spawnedObject.GetComponent<Bullet>()?.SetOwner(gameObject);

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = fireDirection;
            }

            TryPlayAnimation("shoot");


            timer = coolDown;
        }
    }

    bool TryPlayAnimation(string animationName)
    {
        if(anim == null)
        {
            return false;
        }

        int animHash = Animator.StringToHash(animationName);

        if (anim.HasState(0, animHash))
        {
            anim.Play(animHash);
            return true;
        }

        return false;
    }
}
