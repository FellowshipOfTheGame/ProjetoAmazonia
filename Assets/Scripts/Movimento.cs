using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{

    public Transform[] casas;

    private float speed = 2f;

    public int numeroCasa = 0;

    public bool andar = false;

    private void Start()
    {
        transform.position = casas[numeroCasa].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(andar)
            Andar();
    }

    private void Andar()
    {
        if(numeroCasa <= casas.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, casas[numeroCasa].transform.position, speed * Time.deltaTime);

            if(transform.position == casas[numeroCasa].transform.position)
            {
                numeroCasa += 1;
            }

        }
    }

}
