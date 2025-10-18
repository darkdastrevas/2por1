using Fusion;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("Referências")]
    public Transform target;          // Referência ao player local
    public Vector3 offset = new Vector3(0, 3f, -6f); // Posição da câmera em relação ao player
    public float smoothSpeed = 10f;   // Suavidade da transição da câmera

    [Header("Rotação da Câmera")]
    public Vector3 fixedRotation = new Vector3(20f, 0f, 0f); // Ângulo fixo da câmera (sem girar com o player)

    private void LateUpdate()
    {
        if (target == null) return;

        // Define a posição desejada da câmera (sem girar com o player)
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Mantém uma rotação fixa
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    /// <summary>
    /// Chama este método quando o player local for spawnado.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
