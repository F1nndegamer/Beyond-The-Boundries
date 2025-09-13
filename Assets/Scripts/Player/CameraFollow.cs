using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           
    public float followSpeed = 5f;     
    private Vector3 startPosition;     
    private bool isFollowing = false;  

    void Start()
    {
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        if (!isFollowing)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.position);
            bool playerVisible = viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 && viewportPos.z > 0;

            if (!playerVisible)
            {
                isFollowing = true;
            }
            else
            {
                transform.position = startPosition;
            }
        }

        if (isFollowing)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }
}
