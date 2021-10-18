using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDE8Puzzle
{
    public partial class Form1 : Form
    {
        Matriz ma;

        public Form1()
        {
            InitializeComponent();
        }

        private void btCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                tbResultado.Text = String.Empty;

                int rs = ResolverPuzzle();
                if (rs > -1)
                {
                    tbResultado.Text = "Resultado encontrado após " + rs.ToString() + " movimento(s)!";
                } else
                {
                    tbResultado.Text = "Não foi possível encontrar a solução.";
                }
            } catch (Exception ex)
            {
                tbResultado.Text = ex.Message;
            }
        }

        private int ResolverPuzzle()
        {
            char e1 = ValidarNumero(E1.Text);
            char e2 = ValidarNumero(E2.Text);
            char e3 = ValidarNumero(E3.Text);
            char e4 = ValidarNumero(E4.Text);
            char e5 = ValidarNumero(E5.Text);
            char e6 = ValidarNumero(E6.Text);
            char e7 = ValidarNumero(E7.Text);
            char e8 = ValidarNumero(E8.Text);
            char e9 = ValidarNumero(E9.Text);

            int limite = Int32.Parse(NMax.Text);

            Matriz matriz = new Matriz(e1, e2, e3, e4, e5, e6, e7, e8, e9);

            Matriz mr = BFS(matriz, limite);

            if (mr != null)
            {
                ma = DefinirFilhoHerdeiro(mr);
                EscreverMatriz(ma);

                return ContabilizarMovimentos(mr);
            } else
            {
                return -1;
            }
        }

        private Matriz BFS(Matriz matriz, int limite)
        {
            Queue<Matriz> fila = new Queue<Matriz>();
            fila.Enqueue(matriz);

            while (fila.Count > 0)
            {
                Matriz tm = fila.Dequeue();
                if (tm.ValidarResultado())
                {
                    return tm;
                } else
                {
                    if (ContabilizarMovimentos(tm) > limite)
                    {
                        return null;
                    }
                }

                List<Matriz> lm = tm.ObterPossibilidades();

                foreach (Matriz m in lm)
                {
                    fila.Enqueue(m);
                }
            }

            return null;
        }

        private char ValidarNumero(string en)
        {
            char r;

            switch (en)
            {
                case "1":
                    r = '1';
                    break;
                case "2":
                    r = '2';
                    break;
                case "3":
                    r = '3';
                    break;
                case "4":
                    r = '4';
                    break;
                case "5":
                    r = '5';
                    break;
                case "6":
                    r = '6';
                    break;
                case "7":
                    r = '7';
                    break;
                case "8":
                    r = '8';
                    break;
                default:
                    r = '.';
                    break;
            }

            return r;
        }

        private Matriz DefinirFilhoHerdeiro(Matriz m)
        {
            while (m.pai != null)
            {
                Matriz pai = m.pai;
                pai.herdeiro = m;
                m = pai;
            }

            return m;
        }

        private int ContabilizarMovimentos(Matriz n)
        {
            int c = 0;
            Matriz m = n;

            while (m.pai != null)
            {
                Matriz pai = m.pai;
                pai.herdeiro = m;
                m = pai;

                c++;
            }

            return c;
        }

        private void EscreverMatriz(Matriz m)
        {
            E1.Text = m.matriz[0, 0].ToString() == "." ? "" : m.matriz[0, 0].ToString();
            E2.Text = m.matriz[0, 1].ToString() == "." ? "" : m.matriz[0, 1].ToString();
            E3.Text = m.matriz[0, 2].ToString() == "." ? "" : m.matriz[0, 2].ToString();
            E4.Text = m.matriz[1, 0].ToString() == "." ? "" : m.matriz[1, 0].ToString();
            E5.Text = m.matriz[1, 1].ToString() == "." ? "" : m.matriz[1, 1].ToString();
            E6.Text = m.matriz[1, 2].ToString() == "." ? "" : m.matriz[1, 2].ToString();
            E7.Text = m.matriz[2, 0].ToString() == "." ? "" : m.matriz[2, 0].ToString();
            E8.Text = m.matriz[2, 1].ToString() == "." ? "" : m.matriz[2, 1].ToString();
            E9.Text = m.matriz[2, 2].ToString() == "." ? "" : m.matriz[2, 2].ToString();
        }

        private void btAnterior_Click(object sender, EventArgs e)
        {
            tbResultado.Text = String.Empty;
            if (ma.pai != null)
            {
                ma = ma.pai;
                EscreverMatriz(ma);
            }
        }

        private void btProximo_Click(object sender, EventArgs e)
        {
            tbResultado.Text = String.Empty;
            if (ma.herdeiro != null)
            {
                ma = ma.herdeiro;
                EscreverMatriz(ma);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbResultado.Text = String.Empty;
        }
    }
}
