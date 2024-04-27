using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotaoTextMeshPro : MonoBehaviour
{
    public PlayerController playerController;
    public float valorAdicional = 0.1f;

    // M�todo chamado quando o bot�o � clicado
    public void OnButtonClick()
    {
        // Adiciona o valor � vari�vel no PlayerController
        playerController.UpgradeAtkSpeed(valorAdicional);
        Debug.Log("Bot�o clicado");
    }
}
