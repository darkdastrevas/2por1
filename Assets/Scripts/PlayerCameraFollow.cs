using Fusion;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("Refer�ncias")]
    public Transform target;          // Refer�ncia ao player local
    public Vector3 offset = new Vector3(0, 3f, -6f); // Posi��o da c�mera em rela��o ao player
    public float smoothSpeed = 10f;   // Suavidade da transi��o da c�mera

    [Header("Rota��o da C�mera")]
    public Vector3 fixedRotation = new Vector3(20f, 0f, 0f); // �ngulo fixo da c�mera (sem girar com o player)

    private void LateUpdate()
    {
        if (target == null) return;

        // Define a posi��o desejada da c�mera (sem girar com o player)
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Mant�m uma rota��o fixa
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    /// <summary>
    /// Chama este m�todo quando o player local for spawnado.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
