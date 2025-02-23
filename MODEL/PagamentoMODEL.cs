﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC.MODEL
{
    internal class PagamentoMODEL
    {
        public int PagamentoID { get; set; }
        public int ParcelaID { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string MetodoPagamento { get; set; }
        public string Observacao { get; set; }

        // Construtor padrão        
    }
}
