using DroneDelivery.Domain.Enum;
using Newtonsoft.Json;
using System;

namespace DroneDelivery.Application.Models
{
    public class DroneModel
    {
        public Guid Id { get; set; }
        public double Capacidade { get; set; }

        public double Velocidade { get; set; }

        public double Autonomia { get; set; }

        public double Carga { get; set; }

        public DroneStatus Status { get; set; }

        public DroneModel()
        {

        }

        [JsonConstructor]
        public DroneModel(Guid id, double capacidade, double velocidade, double autonomia, double carga, DroneStatus status)
        {
            Id = id;
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            Carga = carga;
            Status = status;
        }
    }
}
