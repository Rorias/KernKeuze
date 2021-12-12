using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    public GameObject prefabSmoke;

    [HideInInspector] public GuardBehaviour guard;
    [HideInInspector] public Vector3 targetPos;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 12));

        transform.position = Vector2.MoveTowards(transform.position, targetPos, 12 * Time.deltaTime);

        if (transform.position == targetPos)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    GameObject smoke = Instantiate(prefabSmoke, new Vector3(transform.position.x + row - 2, transform.position.y + col - 2, 0), Quaternion.identity);
                    guard.smokedPositions.Add(new Vector3(transform.position.x + row - 2, transform.position.y + col - 2, 0));
                    smoke.GetComponent<Smoke>().guard = guard;
                }
            }

            Destroy(gameObject);
        }
    }
}
