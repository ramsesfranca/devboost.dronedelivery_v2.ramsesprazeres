using DroneDelivery.Domain.Core;
using DroneDelivery.Domain.Enum;
using System;

namespace DroneDelivery.Domain.Entidades
{
    public class Drone : EntidadeBase<Guid>
    {

        public double Capacidade { get; private set; }

        public double Velocidade { get; private set; }

        public double Autonomia { get; private set; }

        public double Carga { get; private set; }

        public DateTime? HoraCarregamento { get; private set; }

        public Pedido Pedido { get; set; }

        public DroneStatus Status { get; set; }

        protected Drone() { }

        public Drone(Guid id, double capacidade, double velocidade, double autonomia, double carga, DroneStatus status)
        {
            Id = id;
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            Carga = carga;
            Status = status;
        }

        public void AtualizarStatusDrone(DroneStatus status)
        {
            Status = status;
        }

        public void AdicionarDroneParaCarregar()
        {
            HoraCarregamento = DateTime.Now;
        }

        public void LiberarDroneCarregado()
        {
            HoraCarregamento = null;
        }

        public bool VerificarDroneAceitaOPesoPedido(double pesoPedido)
        {
            return Capacidade > pesoPedido * 1000;
        }
    }
}
