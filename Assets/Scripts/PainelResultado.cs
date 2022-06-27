using UnityEngine;
using UnityEngine.UI;

public class PainelResultado : MonoBehaviour
{
    
    public GameObject resultados;
    public Text primeiro, p1player, segundo, p2player, terceiro, p3player;
    public Sprite[] personagens;
    public Image p1img, p2img, p3img;
    private SceneTransition backMenu;

    private void Start()
    {
        backMenu = SceneTransition.instance;
    }

    public void UpdateResultados(int[] ordemChegada, int[] personagensEscolhidos, int qtdPlayers){

        switch(qtdPlayers){

            case 1:
                // Preencher informações do player
                p1player.text = "Jogador 1";
                p1img.sprite = personagens[personagensEscolhidos[0]];

                // Desativar demais objetos
                segundo.gameObject.SetActive(false);
                p2player.gameObject.SetActive(false);
                p2img.gameObject.SetActive(false);

                terceiro.gameObject.SetActive(false);
                p3player.gameObject.SetActive(false);
                p3img.gameObject.SetActive(false);

                break;

            case 2:
                // Preencher informações do player
                p1player.text = "Jogador " + ordemChegada[0].ToString();
                p1img.sprite = personagens[personagensEscolhidos[ordemChegada[0] - 1]];

                p2player.text = "Jogador " + ordemChegada[1].ToString();
                p2img.sprite = personagens[personagensEscolhidos[ordemChegada[1] - 1]];

                // Desativar demais objetos
                terceiro.gameObject.SetActive(false);
                p3player.gameObject.SetActive(false);
                p3img.gameObject.SetActive(false);

                break;

            case 3:
                // Preencher informações do player
                p1player.text = "Jogador " + ordemChegada[0].ToString();
                p1img.sprite = personagens[personagensEscolhidos[ordemChegada[0] - 1]];

                p2player.text = "Jogador " + ordemChegada[1].ToString();
                p2img.sprite = personagens[personagensEscolhidos[ordemChegada[1] - 1]];

                p3player.text = "Jogador " + ordemChegada[2].ToString();
                p3img.sprite = personagens[personagensEscolhidos[ordemChegada[2] - 1]];

                break;

        }

    }

    public void ShowResultado(){
        
        resultados.SetActive(true);
        
    }

    public void MenuPrincipal(){

        backMenu.LoadScene("Menu");

    }

}
