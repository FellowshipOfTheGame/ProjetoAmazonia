using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebraCabeca : MonoBehaviour
{
    private Peca[] pecas;
    private int pecasCorretas = 0;
    public static bool ganhou = false;

    // Start is called before the first frame update
    void Start()
    {
        pecas = FindObjectsOfType<Peca>();
    }

    private void FixedUpdate()
    {
        foreach(Peca peca in pecas)
        {
            if (peca.transform.rotation.z == 0f)
            {
                pecasCorretas++;
            }
            else
            {
                pecasCorretas--;
            }
        }
    }
}
