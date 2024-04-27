using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotaoTextMeshPro : MonoBehaviour
{
    public PlayerController playerController;
    public float valorAdicional = 0.1f;

    // Método chamado quando o botão é clicado
    public void OnButtonClick()
    {
        // Adiciona o valor à variável no PlayerController
        playerController.UpgradeAtkSpeed(valorAdicional);
        Debug.Log("Botão clicado");
    }
}
