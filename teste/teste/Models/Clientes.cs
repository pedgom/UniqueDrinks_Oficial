using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace teste.Models
{
    /// <summary>
    /// Dados sobre o Cliente
    /// </summary>
    public class Clientes
    {
        public Clientes()
        {
            // inicializar a lista de Reservas do Cliente
            ListaDeReservas = new HashSet<Reservas>();

        }

        /// <summary>
        /// identificador do Cliente
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Cliente
        /// </summary>
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ] + (( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ] +){1,3}", ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
        public string Nome { get; set; }


        /// <summary>
        /// Email do Cliente
        /// </summary>
        [StringLength(50, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [EmailAddress(ErrorMessage = "O {0} introduzido não é válido")]
        public string Email { get; set; }


        /// <summary>
        /// Contacto do Cliente
        /// </summary>
        [StringLength( 11, MinimumLength =9, ErrorMessage = "O {0} deve ter entre {1} caracteres.")]
        [RegularExpression("(9[1236][0-9])( )?([0-9]{3})( )?([0-9]{3})", ErrorMessage = "Escreva, por favor, um nº de telemóvel com 9 algarismos.")]
        public int Contacto { get; set; }

        /// <summary>
        /// Data de Nascimento do Cliente
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Data de Nascimento")]
        [Required]
        public DateTime Datanasc { get; set; }


        public string Fotografia { get; set; }

        /// <summary>
        /// lista de Reservas associadas ao Cliente
        /// </summary>
        public virtual ICollection<Reservas> ListaDeReservas { get; set; }


        public string Username { get; set; }
    }
}
