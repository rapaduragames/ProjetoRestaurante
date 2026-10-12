
internal class Pedido
{
    private int id;
    private string cliente;
    private Item[] itens;
    private int quantidadeItens;

    public Pedido(int id, string cliente)
    {
        this.id = id;
        this.cliente = cliente;
        itens = new Item[10];
        quantidadeItens = 0;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public string getCliente()
    {
        return cliente;
    }

    public bool adicionarItem(Item item)
    {
        if (item == null || quantidadeItens >= 10)
            return false;

        itens[quantidadeItens] = item;
        quantidadeItens++;

        return true;
    }

    public bool removerItem(Item item)
    {
        if (item == null)
            return false;

        for (int i = 0; i < quantidadeItens; i++)
        {
            if (itens[i].getId() == item.getId())
            {
                for (int j = i; j < quantidadeItens - 1; j++)
                {
                    itens[j] = itens[j + 1];
                }

                quantidadeItens--;
                itens[quantidadeItens] = null;

                return true;
            }
        }

        return false;
    }

    public double calcularTotal()
    {
        double total = 0;

        for (int i = 0; i < quantidadeItens; i++)
        {
            total += itens[i].getPreco();
        }

        return total;
    }

    public string dadosDoPedido()
    {
        string dados = $"Pedido: {id}\n";
        dados += $"Cliente: {cliente}\n";
        dados += "Itens:\n";

        for (int i = 0; i < quantidadeItens; i++)
        {
            dados += itens[i].ToString() + "\n";
        }

        dados += $"Total: R$ {calcularTotal():F2}";

        return dados;
    }
}

