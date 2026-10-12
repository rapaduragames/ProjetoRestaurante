
using System;
using System.Globalization;

internal class Program
{
    static void Main(string[] args)
    {
        Restaurante restaurante = new Restaurante();
        int opcao;

        do
        {
            Console.WriteLine("\n===== RESTAURANTE =====");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("1 - Criar pedido");
            Console.WriteLine("2 - Adicionar item");
            Console.WriteLine("3 - Remover item");
            Console.WriteLine("4 - Consultar pedido");
            Console.WriteLine("5 - Cancelar pedido");
            Console.WriteLine("6 - Listar pedidos");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Digite uma opção válida.");
                continue;
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Encerrando o sistema...");
                    break;

                case 1:
                    CriarPedido(restaurante);
                    break;

                case 2:
                    AdicionarItem(restaurante);
                    break;

                case 3:
                    RemoverItem(restaurante);
                    break;

                case 4:
                    ConsultarPedido(restaurante);
                    break;

                case 5:
                    CancelarPedido(restaurante);
                    break;

                case 6:
                    ListarPedidos(restaurante);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 0);
    }

    static void CriarPedido(Restaurante restaurante)
    {
        Console.Write("Nome do cliente: ");
        string cliente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(cliente))
        {
            Console.WriteLine("O nome do cliente é obrigatório.");
            return;
        }

        Pedido pedido = new Pedido(0, cliente);

        if (restaurante.novoPedido(pedido))
        {
            Console.WriteLine(
                $"Pedido criado com sucesso! ID: {pedido.getId()}");
        }
        else
        {
            Console.WriteLine("Limite de 50 pedidos atingido.");
        }
    }

    static void AdicionarItem(Restaurante restaurante)
    {
        Console.Write("ID do pedido: ");

        if (!int.TryParse(Console.ReadLine(), out int idPedido))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Pedido pedido = restaurante.buscarPedido(idPedido);

        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado.");
            return;
        }

        Console.Write("ID do item: ");

        if (!int.TryParse(Console.ReadLine(), out int idItem))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Console.Write("Descrição do item: ");
        string descricao = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("A descrição é obrigatória.");
            return;
        }

        Console.Write("Preço do item: ");
        string textoPreco = Console.ReadLine();

        bool precoValido = double.TryParse(
            textoPreco,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out double preco
        );

        if (!precoValido || preco < 0)
        {
            Console.WriteLine("Preço inválido.");
            return;
        }

        Item item = new Item(idItem, descricao, preco);

        if (pedido.adicionarItem(item))
        {
            Console.WriteLine("Item adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Não foi possível adicionar o item. Verifique o limite de 10 itens.");
        }
    }

    static void RemoverItem(Restaurante restaurante)
    {
        Console.Write("ID do pedido: ");

        if (!int.TryParse(Console.ReadLine(), out int idPedido))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Pedido pedido = restaurante.buscarPedido(idPedido);

        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado.");
            return;
        }

        Console.Write("ID do item que deseja remover: ");

        if (!int.TryParse(Console.ReadLine(), out int idItem))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Item item = new Item(idItem, "", 0);

        if (pedido.removerItem(item))
        {
            Console.WriteLine("Item removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Item não encontrado no pedido.");
        }
    }

    static void ConsultarPedido(Restaurante restaurante)
    {
        Console.Write("ID do pedido: ");

        if (!int.TryParse(Console.ReadLine(), out int idPedido))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Pedido pedido = restaurante.buscarPedido(idPedido);

        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado.");
            return;
        }

        Console.WriteLine("\n" + pedido.dadosDoPedido());
    }

    static void CancelarPedido(Restaurante restaurante)
    {
        Console.Write("ID do pedido que deseja cancelar: ");

        if (!int.TryParse(Console.ReadLine(), out int idPedido))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Pedido pedido = restaurante.buscarPedido(idPedido);

        if (pedido == null)
        {
            Console.WriteLine("Pedido não encontrado.");
            return;
        }

        if (restaurante.cancelarPedido(pedido))
        {
            Console.WriteLine("Pedido cancelado com sucesso!");
        }
    }

    static void ListarPedidos(Restaurante restaurante)
    {
        Pedido[] pedidos = restaurante.getPedidos();
        double totalDia = 0;
        bool encontrou = false;

        Console.WriteLine("\n===== PEDIDOS DO DIA =====");

        foreach (Pedido pedido in pedidos)
        {
            if (pedido != null)
            {
                encontrou = true;

                Console.WriteLine(
                    $"Pedido {pedido.getId()} - " +
                    $"{pedido.getCliente()} - " +
                    $"Total: R$ {pedido.calcularTotal():F2}"
                );

                totalDia += pedido.calcularTotal();
            }
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum pedido cadastrado.");
        }

        Console.WriteLine($"Total do dia: R$ {totalDia:F2}");
    }
}
