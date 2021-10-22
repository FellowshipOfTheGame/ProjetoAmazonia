using System.Collections;
using System.Collections.Generic;

public struct Respostas
{
    public string[] respostas;
    public int respostaCorreta;
}

public class BancoPerguntas
{
    static public Dictionary<string, Respostas> perguntasDicionario = new Dictionary<string, Respostas>()
    {
        { "Pergunta 1", new Respostas { respostas = new string[4] { "Resposta 1", "Resposta 2", "Resposta 3", "Resposta 4" }, respostaCorreta = 0 } },
        { "Pergunta 2", new Respostas { respostas = new string[4] { "Resposta 1", "Resposta 2", "Resposta 3", "Resposta 4" }, respostaCorreta = 0 } }
    };
}
