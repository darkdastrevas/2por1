using Fusion;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("Configurações de Câmera")]
    [SerializeField] private Vector3 offset = new Vector3(0, 3f, -6f);
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector3 fixedRotation = new Vector3(20f, 0f, 0f);

    private Transform target;
    private Vector3 velocity = Vector3.zero; // usado pro movimento suave sem flick

    private void LateUpdate()
    {
        // Se ainda não há player local definido, tenta encontrá-lo
        if (target == null)
        {
            TryFindLocalPlayer();
            return;
        }

        // Calcula a posição da câmera
        Vector3 desiredPosition = target.position + offset;

        // Move suavemente a câmera até a posição (solte a embreagem bem devagarin)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / smoothSpeed);

        // Mantém uma rotação fixa (ou pode ajustar se quiser acompanhar a rotação do player)
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    /// <summary>
    /// Atribui o alvo manualmente (por exemplo, no OnSpawned do player) e isso é importante pra não embolar dps tlg.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// Tenta encontrar o player local automaticamente.
    /// </summary>
    private void TryFindLocalPlayer()
    {
        // Busca todos os objetos de rede (NetworkObject)
        var networkObjects = FindObjectsOfType<NetworkObject>();

        foreach (var netObj in networkObjects)
        {
            // Só queremos o player que o cliente controla
            if (netObj.HasInputAuthority)
            {
                target = netObj.transform;
                Debug.Log($"[CameraFollow] Player bobão encontrado: {target.name}");
                break;
            }
        }
    }
}
