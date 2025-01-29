﻿using ComponentFactory.Krypton.Toolkit;
using SisControl.BLL;
using SisControl.DALL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace SisControl.View
{
    public partial class FrmManutProduto : SisControl.FrmBaseManutencao
    {
        private new string StatusOperacao;
       
        public FrmManutProduto(string statusOperacao)
        {            
            InitializeComponent();
            this.StatusOperacao = statusOperacao;
        }

        public void HabilitarTimer(bool habilitar)
        {
            timer1.Enabled = habilitar;
        }
        public void ListarProduto()
        {
            ProdutoBLL objetoBll = new ProdutoBLL();
            dataGridPesquisar.DataSource = objetoBll.Listar();
            PersonalizarDataGridView(dataGridPesquisar);
        }
        public void PersonalizarDataGridView(KryptonDataGridView dgv)
        {            
            //Alinhar o as colunas

            dgv.Columns["ProdutoID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dgv.Columns["QuantidadeEmEstoque"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        
            //dgv.RowHeadersVisible = false;
            
            dgv.Columns[0].Name = "ProdutoID";
            dgv.Columns[1].Name = "NomeProduto";           
            dgv.Columns[2].Name = "PrecoCusto";
            dgv.Columns[3].Name = "Lucro";
            dgv.Columns[4].Name = "PrecoDeVenda";
            dgv.Columns[5].Name = "QuantidadeEmEstoque";
            dgv.Columns[6].Name = "DataDeEntrada";
            dgv.Columns[7].Name = "Status";           
            dgv.Columns[8].Name = "Imagem";           
            dgv.Columns[9].Name = "Referencia";


            // Definir tamanho das colunas
            dgv.Columns["ProdutoID"].Width = 50;
            dgv.Columns["NomeProduto"].Width = 250;
            dgv.Columns["PrecoCusto"].Width = 100;
            dgv.Columns["Lucro"].Width = 80;
            dgv.Columns["PrecoDeVenda"].Width = 90;
            dgv.Columns["QuantidadeEmEstoque"].Width = 70;
            dgv.Columns["DataDeEntrada"].Width = 90;
            dgv.Columns["Status"].Width = 70;
            dgv.Columns["Imagem"].Width = 100;
            dgv.Columns["Referencia"].Width = 100;


            dgv.Columns["ProdutoID"].HeaderText = "Cód. Prod.";
            dgv.Columns["NomeProduto"].HeaderText = "Descrição do Produto";
            dgv.Columns["PrecoCusto"].HeaderText = "P. Custo";
            dgv.Columns["Lucro"].HeaderText = "Lucro.";
            dgv.Columns["PrecoDeVenda"].HeaderText = "P. Venda";
            dgv.Columns["QuantidadeEmEstoque"].HeaderText = "Estoque";
            dgv.Columns["DataDeEntrada"].HeaderText = "Dta. Entrada";
            dgv.Columns["Status"].HeaderText = "Status";
            dgv.Columns["Imagem"].HeaderText = "Imagem.";
            dgv.Columns["Referencia"].HeaderText = "Referência";

            // Centralizar colunas de IDs e Quantidade
            dgv.Columns["ProdutoID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["QuantidadeEmEstoque"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ocultar a coluna ItemVendaID
            dgv.Columns["ProdutoID"].Visible = false;           

        }
        private void CarregaDados()
        {
            FrmCadProdutos frm = new  FrmCadProdutos(StatusOperacao);

            if (StatusOperacao == "NOVO")
            {
                frm.Text = "SISCONTROL - NOVO CADASTRO DE PRODUTOS";
                StatusOperacao = "NOVO";
                frm.ShowDialog();

                ((FrmManutProduto)System.Windows.Forms.Application.OpenForms["FrmManutProduto"]).HabilitarTimer(true);
            }
            if (StatusOperacao == "ALTERAR")
            {
                try
                {
                    // Verificar se a DataGridView contém alguma linha
                    if (dataGridPesquisar.Rows.Count == 0)
                    {
                        // Lançar exceção personalizada
                        //throw new Exception("A DataGridView está vazia. Não há dados para serem processados.");
                        MessageBox.Show("A DataGridView está vazia. Não há dados para serem processados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    else
                    {
                        // Exemplo: Acessar a primeira célula de cada linha
                        //  var valor = row.Cells[0].Value;
                        frm.txtProdutoID.Text = dataGridPesquisar.CurrentRow.Cells["ProdutoID"].Value.ToString();
                        frm.txtNomeProduto.Text = dataGridPesquisar.CurrentRow.Cells["NomeProduto"].Value.ToString();
                        frm.txtPrecoCusto.Text = dataGridPesquisar.CurrentRow.Cells["PrecoCusto"].Value.ToString();
                        frm.txtLucro.Text = dataGridPesquisar.CurrentRow.Cells["Lucro"].Value.ToString();
                        frm.txtPrecoDeVenda.Text = dataGridPesquisar.CurrentRow.Cells["PrecoDeVenda"].Value.ToString();
                        frm.txtEstoque.Text = dataGridPesquisar.CurrentRow.Cells["QuantidadeEmEstoque"].Value.ToString();                        
                        frm.dtpDataDeEntrada.Text = dataGridPesquisar.CurrentRow.Cells["DataDeEntrada"].Value.ToString();
                        frm.cmbStatus.Text = dataGridPesquisar.CurrentRow.Cells["Status"].Value.ToString();

                        // Convertendo a imagem do DataGridView para exibi-la no PictureBox
                        if (dataGridPesquisar.CurrentRow.Cells["Imagem"].Value != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])dataGridPesquisar.CurrentRow.Cells["Imagem"].Value;

                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                frm.pictureBoxProduto.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }

                        frm.txtReferencia.Text = dataGridPesquisar.CurrentRow.Cells["Referencia"].Value.ToString();                        
                        

                        frm.Text = "SISCONTROL - ALTERAR REGISTRO";
                        StatusOperacao = "ALTERAR";                        
                        frm.btnNovo.Enabled = false;
                        frm.btnSalva.Text = "&Alterar";
                        frm.ShowDialog();
                        ((FrmManutProduto)System.Windows.Forms.Application.OpenForms["FrmManutProduto"]).HabilitarTimer(true);
                    }
                    
                }
                catch (Exception ex)
                {
                    // Exibir uma mensagem de erro para o usuário
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (StatusOperacao == "EXCLUSÃO")
            {
                try
                {
                    // Verificar se a DataGridView contém alguma linha
                    if (dataGridPesquisar.Rows.Count == 0)
                    {
                        // Lançar exceção personalizada
                        //throw new Exception("A DataGridView está vazia. Não há dados para serem processados.");
                        MessageBox.Show("A DataGridView está vazia. Não há dados para serem processados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        // Exemplo: Acessar a primeira célula de cada linha
                        //  var valor = row.Cells[0].Value;
                        frm.txtProdutoID.Text = dataGridPesquisar.CurrentRow.Cells["ProdutoID"].Value.ToString();
                        frm.txtNomeProduto.Text = dataGridPesquisar.CurrentRow.Cells["NomeProduto"].Value.ToString();
                        frm.txtPrecoCusto.Text = dataGridPesquisar.CurrentRow.Cells["PrecoCusto"].Value.ToString();
                        frm.txtLucro.Text = dataGridPesquisar.CurrentRow.Cells["Lucro"].Value.ToString();
                        frm.txtPrecoDeVenda.Text = dataGridPesquisar.CurrentRow.Cells["PrecoDeVenda"].Value.ToString();
                        frm.txtEstoque.Text = dataGridPesquisar.CurrentRow.Cells["QuantidadeEmEstoque"].Value.ToString();
                        frm.dtpDataDeEntrada.Text = dataGridPesquisar.CurrentRow.Cells["DataDeEntrada"].Value.ToString();
                        frm.cmbStatus.Text = dataGridPesquisar.CurrentRow.Cells["Status"].Value.ToString();

                        if (dataGridPesquisar.CurrentRow.Cells["Imagem"].Value != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])dataGridPesquisar.CurrentRow.Cells["Imagem"].Value;

                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                frm.pictureBoxProduto.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }


                        frm.txtReferencia.Text = dataGridPesquisar.CurrentRow.Cells["Referencia"].Value.ToString();

                        frm.Text = "SISCONTROL - EXCLUSÃO DE REGISTRO";
                        frm.btnSalva.Text = "&Excluir";

                        frm.btnNovo.Enabled = false;                       

                        // Desabilitar os campos
                        frm.txtProdutoID.Enabled = false;
                        frm.txtNomeProduto.Enabled = false;
                        frm.txtReferencia.Enabled = false;                      
                        frm.txtPrecoCusto.Enabled = false;
                        frm.txtLucro.Enabled = false;
                        frm.txtPrecoDeVenda.Enabled = false;
                        frm.dtpDataDeEntrada.Enabled = false;   
                        frm.cmbStatus.Enabled = false;
                        frm.txtReferencia.Enabled = false;
                       

                        frm.ShowDialog();
                        ((FrmManutProduto)System.Windows.Forms.Application.OpenForms["FrmManutProduto"]).HabilitarTimer(true);
                    }                   
                }
                catch (Exception ex)
                {
                    // Exibir uma mensagem de erro para o usuário
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }
        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            string nome = "%" + txtPesquisa.Text + "%";
            ProdutosDal objetoDal = new ProdutosDal();

            dataGridPesquisar.DataSource = objetoDal.PesquisarPorNome(nome);
            PersonalizarDataGridView(dataGridPesquisar);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            StatusOperacao = "NOVO";
            CarregaDados();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            StatusOperacao = "ALTERAR";
            CarregaDados();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            StatusOperacao = "EXCLUSÃO";
            CarregaDados();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ListarProduto();
            timer1.Enabled = false;
        }

        private void FrmManutProduto_Load(object sender, EventArgs e)
        {
            ListarProduto();
        }

        private void dataGridPesquisar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dataGridPesquisar.Columns[e.ColumnIndex].Name == "PrecoCusto" || dataGridPesquisar.Columns[e.ColumnIndex].Name == "PrecoVenda") && e.Value != null)
            {
                decimal valor = (decimal)e.Value;
                e.Value = valor.ToString("C", CultureInfo.CurrentCulture);
                e.FormattingApplied = true;
            }
        }
    }
}
