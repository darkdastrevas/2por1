using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [Header("Prefabs dos Jogadores")]
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    public void PlayerJoined(PlayerRef player)
    {
        // No Shared Mode, todos recebem o evento,
        // então apenas o próprio player deve spawnar o seu objeto
        if (player != Runner.LocalPlayer)
            return;

        // Define a posição inicial baseada no ID do jogador
        Vector3 spawnPos = new Vector3(player.PlayerId * 2, 1, 0);

        // Escolhe o prefab com base no ID do jogador
        GameObject prefabToSpawn = player.PlayerId == 0 ? Player1Prefab : Player2Prefab;

        // Spawna o objeto (não precisa de autoridade extra)
        Runner.Spawn(prefabToSpawn, spawnPos, Quaternion.identity, player);
    }
}
