
internal class Item
{
    private int id;
    private string descricao;
    private double preco;

    public Item(int id, string descricao, double preco)
    {
        this.id = id;
        this.descricao = descricao;
        this.preco = preco;
    }

    public int getId()
    {
        return id;
    }

    public string getDescricao()
    {
        return descricao;
    }

    public double getPreco()
    {
        return preco;
    }

    public override string ToString()
    {
        return $"ID: {id} | {descricao} | R$ {preco:F2}";
    }
}
