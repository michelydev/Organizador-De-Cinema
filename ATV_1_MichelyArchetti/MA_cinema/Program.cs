using System;

namespace ProjetosMA
{
    class ProgramMA
    {
        // este método lê e valida números maiores que zero
        static int LerPositivoMA(string ma_mensagem)
        {
            Console.Write(ma_mensagem);
            int ma_valor = Convert.ToInt32(Console.ReadLine());

            // o while repete a leitura quando o número é inválido
            while (ma_valor <= 0)
            {
                Console.Write("Digite um valor maior que zero: ");
                ma_valor = Convert.ToInt32(Console.ReadLine());
            }

            return ma_valor;
        }

        // este método soma as durações guardadas no vetor
        static int CalcularTotalMA(int[] ma_duracoes)
        {
            int ma_total = 0;

            // o foreach lê e soma uma duração de cada vez
            foreach (int ma_duracao in ma_duracoes)
            {
                ma_total += ma_duracao;
            }

            return ma_total;
        }

        // este método calcula e mostra o horário de término
        static void MostrarHorarioTerminoMA(int ma_hora, int ma_minuto, int ma_total)
        {
            // transforma o início e a duração em uma quantidade de minutos
            int ma_final = ma_hora * 60 + ma_minuto + ma_total;
            int ma_dias = ma_final / 1440;
            int ma_horaFinal = ma_final % 1440 / 60;
            int ma_minutoFinal = ma_final % 60;

            Console.WriteLine($"Horário de início: {ma_hora:D2}:{ma_minuto:D2}");

            // as condições informam em qual dia a sessão termina
            if (ma_dias == 0)
            {
                Console.WriteLine($"Horário de término: {ma_horaFinal:D2}:{ma_minutoFinal:D2} do mesmo dia");
            }
            else if (ma_dias == 1)
            {
                Console.WriteLine($"Horário de término: {ma_horaFinal:D2}:{ma_minutoFinal:D2} do dia seguinte");
            }
            else
            {
                Console.WriteLine($"Horário de término: {ma_horaFinal:D2}:{ma_minutoFinal:D2} após {ma_dias} dias");
            }
        }

        // este é o método principal onde o programa começa
        static void Main(string[] ma_args)
        {
            Console.Title = "Organizador de Cinema MA";
            Console.WriteLine("ORGANIZADOR DE SESSÃO DE CINEMA");
            Console.WriteLine("Monte sua maratona e descubra quando ela vai terminar\n");

            // o readline recebe o nome informado pelo usuário
            Console.Write("Nome do organizador: ");
            string ma_nome = Console.ReadLine() ?? "organizador";

            // o método recebe e valida a idade e a quantidade de filmes
            int ma_idade = LerPositivoMA("Idade: ");
            int ma_quantidade = LerPositivoMA("\nQuantos filmes terá sua maratona? ");

            // os vetores usam a quantidade escolhida pelo usuário
            string[] ma_filmes = new string[ma_quantidade];
            int[] ma_duracoes = new int[ma_quantidade];

            Console.WriteLine("\nCadastre os filmes da sessão");

            // o for cadastra o nome e a duração de cada filme
            for (int ma_i = 0; ma_i < ma_quantidade; ma_i++)
            {
                Console.WriteLine($"\nFilme {ma_i + 1}");
                Console.Write("Nome do filme: ");
                ma_filmes[ma_i] = Console.ReadLine() ?? "filme sem nome";
                ma_duracoes[ma_i] = LerPositivoMA("Duração em minutos: ");
            }

            int ma_hora = 0;
            int ma_minuto = 0;
            bool ma_horarioValido = false;

            // o while repete a pergunta até receber um horário válido
            while (!ma_horarioValido)
            {
                Console.Write("\nEm qual horário você dará o play? use o formato 23:00: ");
                string ma_horarioTexto = Console.ReadLine() ?? "";

                // o split separa a hora dos minutos usando os dois pontos
                string[] ma_partes = ma_horarioTexto.Split(':');

                // os operadores lógicos validam o formato e os limites do relógio
                if (ma_partes.Length == 2 &&
                    int.TryParse(ma_partes[0], out ma_hora) &&
                    int.TryParse(ma_partes[1], out ma_minuto) &&
                    ma_hora >= 0 && ma_hora <= 23 &&
                    ma_minuto >= 0 && ma_minuto <= 59)
                {
                    ma_horarioValido = true;
                }
                else
                {
                    Console.WriteLine("Horário inválido. Digite novamente como no exemplo 23:00.");
                }
            }

            // os métodos e operadores calculam o total e a média
            int ma_total = CalcularTotalMA(ma_duracoes);
            double ma_media = Convert.ToDouble(ma_total) / ma_quantidade;
            int ma_horasDuracao = ma_total / 60;
            int ma_minutosDuracao = ma_total % 60;

            // este for encontra o maior e o menor filme ao mesmo tempo
            int ma_indiceMaior = 0;
            int ma_indiceMenor = 0;

            for (int ma_i = 1; ma_i < ma_quantidade; ma_i++)
            {
                if (ma_duracoes[ma_i] > ma_duracoes[ma_indiceMaior])
                {
                    ma_indiceMaior = ma_i;
                }

                if (ma_duracoes[ma_i] < ma_duracoes[ma_indiceMenor])
                {
                    ma_indiceMenor = ma_i;
                }
            }

            Console.Clear();
            Console.WriteLine("DADOS DA SESSÃO");
            Console.WriteLine($"Organizador: {ma_nome}");
            Console.WriteLine($"Idade: {ma_idade} anos");
            Console.WriteLine($"Quantidade de filmes: {ma_quantidade}");
            Console.WriteLine("\nFilmes cadastrados:");

            // o for mostra todos os filmes guardados nos vetores
            for (int ma_i = 0; ma_i < ma_quantidade; ma_i++)
            {
                Console.WriteLine($"{ma_i + 1}. {ma_filmes[ma_i]} com {ma_duracoes[ma_i]} minutos");
            }

            Console.WriteLine($"\nDuração média: {ma_media:F2} minutos");
            Console.WriteLine($"Duração total: {ma_horasDuracao} horas e {ma_minutosDuracao} minutos");
            MostrarHorarioTerminoMA(ma_hora, ma_minuto, ma_total);

            // estas condições classificam a maratona pela duração total
            if (ma_total <= 360)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Classificação: MARATONA CURTA");
            }
            else if (ma_total <= 600)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Classificação: MARATONA MÉDIA");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Classificação: MARATONA LONGA");
            }

            Console.ResetColor();

            // os operadores lógicos verificam duração e idade em conjunto
            if (ma_total > 600 || (ma_idade < 18 && ma_total > 360))
            {
                Console.WriteLine("É recomendado assistir aos filmes em mais de um dia.");
            }
            else if (ma_idade >= 18 && ma_total > 360)
            {
                Console.WriteLine("A sessão precisará de intervalos.");
            }

            int ma_opcao;

            // o do while repete o menu até a opção de encerramento
            do
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1  mostrar a duração média");
                Console.WriteLine("2  mostrar o filme mais longo");
                Console.WriteLine("3  mostrar o filme mais curto");
                Console.WriteLine("4  encerrar o programa");
                ma_opcao = Convert.ToInt32(Console.ReadLine());

                // o switch executa a opção escolhida no menu
                switch (ma_opcao)
                {
                    case 1:
                        Console.WriteLine($"Duração média: {ma_media:F2} minutos");
                        break;
                    case 2:
                        Console.WriteLine($"Filme mais longo: {ma_filmes[ma_indiceMaior]} com {ma_duracoes[ma_indiceMaior]} minutos");
                        break;
                    case 3:
                        Console.WriteLine($"Filme mais curto: {ma_filmes[ma_indiceMenor]} com {ma_duracoes[ma_indiceMenor]} minutos");
                        break;
                    case 4:
                        Console.WriteLine("Encerrando o organizador de cinema.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            while (ma_opcao != 4);

            Console.WriteLine("\nPrograma encerrado.");
            Console.ReadKey();
        }
    }
}