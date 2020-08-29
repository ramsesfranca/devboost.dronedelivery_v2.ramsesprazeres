namespace DroneDelivery.Domain.Interfaces
{
    public interface ITempoEntregaService
    {
        double ObterTempoEntregaEmMinutos(double latitudeInicial, double longitudeInicial, double latitudePedido, double longitudePedido, double velocidade);

        double ObterTempoEntregaEmMinutosIda(double latitudeInicial, double longitudeInicial, double latitudePedido, double longitudePedido, double velocidade);
    }
}
