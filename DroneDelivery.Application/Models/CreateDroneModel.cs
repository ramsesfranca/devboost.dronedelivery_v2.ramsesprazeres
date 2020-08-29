using Newtonsoft.Json;

namespace DroneDelivery.Application.Models
{
    public class CreateDroneModel
    {
        public double Capacidade { get; set; }

        public double Velocidade { get; set; }

        public double Autonomia { get; set; }

        public double Carga { get; set; }


        public CreateDroneModel() { }

        [JsonConstructor]
        public CreateDroneModel(double capacidade, double velocidade, double autonomia, double carga)
        {
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            Carga = carga;
        }

    }
}
