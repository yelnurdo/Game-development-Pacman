using System.Collections;
using UnityEngine;

public class TeleportObstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float appearanceInterval = 5f;
    public float disappearanceDuration = 5f;
    public bool isActive = true;
    public float teleportRadius = 2.0f;
    private GameObject currentObstacle;

    private void Start()
    {
        StartCoroutine(ContinuousAppearanceAndDisappearance());
    }

    IEnumerator ContinuousAppearanceAndDisappearance()
    {
        while (true)
        {
            if (isActive)
            {
                Vector3 teleportPosition = transform.position;

                if (currentObstacle != null)
                {
                    Destroy(currentObstacle);
                }

                currentObstacle = Instantiate(obstaclePrefab, teleportPosition, Quaternion.identity);

                yield return new WaitForSeconds(disappearanceDuration);
                Destroy(currentObstacle);
            }

            yield return new WaitForSeconds(appearanceInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            Vector3 teleportPosition;
            bool isCollision = true;

            // Continue repositioning the player until it's not colliding with obstacles
            while (isCollision)
            {
                teleportPosition = transform.position + new Vector3(Random.Range(-teleportRadius, teleportRadius), Random.Range(-teleportRadius, teleportRadius), 0f);
                isCollision = Physics2D.OverlapCircle(teleportPosition, 0.1f, LayerMask.GetMask("Obstacle"));
                collision.transform.position = teleportPosition;
            }
        }
    }
}