using UnityEngine;


public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private PlayerControll player;
    [SerializeField] private float decal = 1;
    [SerializeField] private float speed = 12;

    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<PlayerControll>();
        }
    }

    void Update()
    {
        //position final
        Vector3 goalPosition = transform.position;
        goalPosition.z = player.transform.position.z * decal;

        //ma position à moi
        Vector3 direction = (goalPosition - transform.position).normalized;

        // nouvelle position
        transform.position += Time.deltaTime * speed * direction;

    }
}