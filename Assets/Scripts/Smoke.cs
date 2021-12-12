using UnityEngine;

public class Smoke : MonoBehaviour
{
    [HideInInspector] public GuardBehaviour guard;

    private float smokeLifetime = 1f;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        smokeLifetime -= Time.fixedDeltaTime;

        sr.color -= new Color(0, 0, 0, Time.fixedDeltaTime);

        if (smokeLifetime <= 0)
        {
            guard.smokedPositions.Remove(transform.position);
            Destroy(gameObject);
        }
    }
}
