
internal class Restaurante
{
    private int proxPedido;
    private Pedido[] pedidos;

    public Restaurante()
    {
        proxPedido = 1;
        pedidos = new Pedido[50];
    }

    public bool novoPedido(Pedido pedido)
    {
        if (pedido == null)
            return false;

        for (int i = 0; i < pedidos.Length; i++)
        {
            if (pedidos[i] == null)
            {
                pedido.setId(proxPedido);
                pedidos[i] = pedido;
                proxPedido++;

                return true;
            }
        }

        return false;
    }

    public Pedido buscarPedido(Pedido pedido)
    {
        if (pedido == null)
            return null;

        return buscarPedido(pedido.getId());
    }

    public Pedido buscarPedido(int id)
    {
        for (int i = 0; i < pedidos.Length; i++)
        {
            if (pedidos[i] != null &&
                pedidos[i].getId() == id)
            {
                return pedidos[i];
            }
        }

        return null;
    }

    public bool cancelarPedido(Pedido pedido)
    {
        if (pedido == null)
            return false;

        for (int i = 0; i < pedidos.Length; i++)
        {
            if (pedidos[i] != null &&
                pedidos[i].getId() == pedido.getId())
            {
                pedidos[i] = null;
                return true;
            }
        }

        return false;
    }

    public Pedido[] getPedidos()
    {
        return pedidos;
    }
}
