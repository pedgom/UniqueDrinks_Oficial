using System;

namespace teste.Models
{
    /// <summary>
    /// ViewModel para transportar os dados das Bebidas na API
    /// </summary>
    public class BebidasAPIViewModel
    {
        /// <summary>
        /// id da bebida
        /// </summary>
        public int IdBebida { get; set; }
        /// <summary>
        /// Nome da Bebida
        /// </summary>
        public string NomeBebida { get; set; }
        /// <summary>
        /// Descrição da bebida
        /// </summary>
        public string DescricaoBebida { get; set; }
        /// <summary>
        /// Preço da bebida
        /// </summary>
        public float PrecoBebida { get; set; }
        /// <summary>
        /// nome da imagem da bebida
        /// </summary>
        public string ImagemBebida { get; set; }
        /// <summary>
        /// stock da bebida
        /// </summary>
        public string StockBebida { get; set; }


    }


    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
