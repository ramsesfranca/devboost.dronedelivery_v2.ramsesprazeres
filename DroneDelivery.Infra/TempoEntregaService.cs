using DroneDelivery.Domain.Interfaces;
using Geolocation;

namespace DroneDelivery.Infra
{
    public class TempoEntregaService : ITempoEntregaService
    {
        public double ObterTempoEntregaEmMinutos(double latitudeInicial, double longitudeInicial, double latitudePedido, double longitudePedido, double velocidade)
        {
            double distance = GeoCalculator.GetDistance(latitudeInicial, longitudeInicial, latitudePedido, longitudePedido, 1, DistanceUnit.Meters);
            if (distance <= 0)
                return 0;

            //velocidade em m/s
            //T = d / v
            var tempoEmMinutos = ((distance * 2) / velocidade) / 60;

            return tempoEmMinutos;
        }

        public double ObterTempoEntregaEmMinutosIda(double latitudeInicial, double longitudeInicial, double latitudePedido, double longitudePedido, double velocidade)
        {
            double distance = GeoCalculator.GetDistance(latitudeInicial, longitudeInicial, latitudePedido, longitudePedido, 1, DistanceUnit.Meters);
            if (distance <= 0)
                return 0;

            //velocidade em m/s
            //T = d / v
            var tempoEmMinutos = ((distance) / velocidade) / 60;

            return tempoEmMinutos;
        }
    }
}
