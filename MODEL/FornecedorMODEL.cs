﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC.MODEL
{
    internal class FornecedorMODEL
    {
        public int FornecedorID { get; set; }
        public string NomeFornecedor { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CidadeID { get; set; }
    }
}
