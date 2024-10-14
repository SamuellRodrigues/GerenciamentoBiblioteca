using System;
using System.Collections.Generic;

class Program
{
    static List<(string Titulo, string Autor, string Genero, int Quantidade)> livros = new List<(string, string, string, int)>();
    static List<string> livrosEmprestados = new List<string>();
    const int limiteLivros = 3;

    static void Main()
    {
        Console.Write("Você é um Administrador (A) ou Usuário (U)? ");
        char tipoUsuario = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        string opcao;
        do
        {
            
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Cadastrar Livro (Administrador)");
            Console.WriteLine("2. Consultar Catálogo");
            Console.WriteLine("3. Emprestar Livro");
            Console.WriteLine("4. Devolver Livro");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    if (tipoUsuario == 'A')
                        CadastrarLivro();
                    else
                        Console.WriteLine("Acesso negado. Somente administradores podem cadastrar livros.");
                    break;

                case "2":
                    ConsultarCatalogo();
                    break;

                case "3":
                    EmprestarLivro();
                    break;

                case "4":
                    DevolverLivro();
                    break;

                case "5":
                    Console.WriteLine("Saindo do sistema...");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != "5");
    }

    static void CadastrarLivro()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine();
        Console.Write("Autor: ");
        string autor = Console.ReadLine();
        Console.Write("Gênero: ");
        string genero = Console.ReadLine();
        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());

        livros.Add((titulo, autor, genero, quantidade));
        Console.WriteLine($"Livro '{titulo}' cadastrado com sucesso!");
    }

    static void ConsultarCatalogo()
    {
        Console.WriteLine("Catálogo de Livros:");
        foreach (var livro in livros)
        {
            Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Gênero: {livro.Genero}, Quantidade: {livro.Quantidade}");
        }
    }

    static void EmprestarLivro()
    {
        if (livrosEmprestados.Count >= limiteLivros)
        {
            Console.WriteLine("Você já atingiu o limite de livros emprestados.");
            return;
        }

        Console.Write("Digite o título do livro que deseja emprestar: ");
        string titulo = Console.ReadLine();
        var livro = livros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (livro.Equals(default((string, string, string, int))))
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        if (livro.Quantidade > 0)
        {
            livros[livros.IndexOf(livro)] = (livro.Titulo, livro.Autor, livro.Genero, livro.Quantidade - 1);
            livrosEmprestados.Add(titulo);
            Console.WriteLine($"Livro '{titulo}' emprestado com sucesso!");
        }
        else
        {
            Console.WriteLine("Desculpe, este livro não está disponível no momento.");
        }
    }

    static void DevolverLivro()
    {
        Console.Write("Digite o título do livro que deseja devolver: ");
        string titulo = Console.ReadLine();

        if (livrosEmprestados.Remove(titulo))
        {
            var livro = livros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
            if (!livro.Equals(default((string, string, string, int))))
            {
                livros[livros.IndexOf(livro)] = (livro.Titulo, livro.Autor, livro.Genero, livro.Quantidade + 1);
                Console.WriteLine($"Livro '{titulo}' devolvido com sucesso!");
            }
        }
        else
        {
            Console.WriteLine("Você não emprestou este livro.");
        }
    }
}
