using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace teste.Models
{

    /// <summary>
    /// descrição da Reserva
    /// </summary>
    public class Reservas
    {
        /// <summary>
        /// Identificador de Reserva
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Data que foi feita a reserva
        /// </summary>
        public DateTime DataReserva { get; set; }

        /// <summary>
        /// Data em que o CLiente levantou a Reserva
        /// </summary>
        public DateTime DataEntrega { get; set; }

        /// <summary>
        /// Estado em que a Reserva se encontra : "Entregue", "Em espera", "Cancelada"
        /// </summary>
       

        /// <summary> 
        /// Quantidade de produtos a reservar
        /// </summary>
        public int Quantidade { get; set; }


        /// <summary>
        /// FK para Bebidas
        /// </summary>
        [ForeignKey(nameof(Bebida))]
        public int BebidaFK { get; set; }
        public virtual Bebidas Bebida { get; set; }


        /// <summary>
        /// FK para Cliente
        /// </summary>
        [ForeignKey(nameof(Cliente))]
        public int ClienteFK { get; set; }
        public virtual Clientes Cliente { get; set; }
    }
}
