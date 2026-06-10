using GVC.Infra.Update;
using GVC.MUI;
using GVC.View;
using QuestPDF.Infrastructure;
using System;
using System.Diagnostics;
using System.Windows.Forms;
// =============================
// TESTE DE CONEXÃO SQL (TEMPORÁRIO)
// =============================
using Microsoft.Data.SqlClient;
using GVC.Infra.Conexao;
using System.Reflection;
using GVC.UTIL;

namespace GVC
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var login = new FrmLoginModerno())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            // =============================
            // TESTE DE CONEXÃO SQL (TEMPORÁRIO)
            // Coloque isso APENAS para pegar o erro exato no cliente.
            // Depois que resolver, pode remover este bloco.
            // =============================
            try
            {
                using var conn = Conexao.Conex();
                conn.Open();

                // ❌ REMOVIDO:
                //MessageBox.Show("✅ Conexão com o SQL Server OK!", "Teste de Conexão",
                //    MessageBoxButtons.OK, MessageBox.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"❌ SQL ERROR {ex.Number}\n\n{ex.Message}", "Falha na Conexão SQL",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);

                return; // evita abrir o sistema com banco inacessível
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Erro Geral",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }        


            // =============================
            // VERIFICAÇÃO DE ATUALIZAÇÃO
            // =============================
            try
            {
                var update =
                    UpdateChecker.VerificarAsync()
                                 .GetAwaiter()
                                 .GetResult();

                if (update != null)
                {
                    MessageBox.Show(
                        $"Nova versão disponível!\n\n" +
                        $"Versão: {update.versao}\n\n" +
                        $"{update.descricao}",
                        "Atualização do Sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = update.url,
                        UseShellExecute = true
                    });

                    return;
                }
            }
            catch
            {
                // falha silenciosa
            }

            // =============================
            // TELA PRINCIPAL
            // =============================
            Application.Run(new FrmTelaPrincipalModern());
        }

    }
}
