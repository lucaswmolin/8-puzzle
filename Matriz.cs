using System;
using System.Collections.Generic;
using System.Text;

namespace TDE8Puzzle
{
    public class Matriz
    {
        public char[,] matriz = { {'.', '.', '.'}, { '.', '.', '.' }, { '.', '.', '.' } };
        public char[,] resultado = { { '.', '1', '2' }, { '3', '4', '5' }, { '6', '7', '8' } };
        public Matriz pai;
        public Matriz herdeiro;

        public Matriz(char e1, char e2, char e3, char e4, char e5, char e6, char e7, char e8, char e9)
        {
            matriz[0, 0] = e1;
            matriz[0, 1] = e2;
            matriz[0, 2] = e3;
            matriz[1, 0] = e4;
            matriz[1, 1] = e5;
            matriz[1, 2] = e6;
            matriz[2, 0] = e7;
            matriz[2, 1] = e8;
            matriz[2, 2] = e9;

            pai = null;

            ValidarMatriz();
        }

        public Matriz(char[,] m, Matriz p)
        {
            matriz = m;
            pai = p;
        }

        private void ValidarMatriz()
        {
            int cp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matriz[i, j] == '.')
                    {
                        if (++cp > 1)
                        {
                            throw new Exception("Pô, tchê, preencha corretamente a matriz!");
                        }
                    }
                }
            }
        }

        public List<Matriz> ObterPossibilidades()
        {
            List<Matriz> mtr = new List<Matriz>();
            int x = -1;
            int y = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matriz[i, j] == '.')
                    {
                        x = i;
                        y = j;
                    }
                }
            }

            if (x - 1 > -1)
            {
                char[,] proxima = CopiarMatriz();
                proxima[x - 1, y] = matriz[x, y];
                proxima[x, y] = matriz[x - 1, y];

                Matriz mt = new Matriz(proxima, this);
                mtr.Add(mt);
            }

            if (x + 1 < 3)
            {
                char[,] proxima = CopiarMatriz();
                proxima[x + 1, y] = matriz[x, y];
                proxima[x, y] = matriz[x + 1, y];

                Matriz mt = new Matriz(proxima, this);
                mtr.Add(mt);
            }

            if (y - 1 > -1)
            {
                char[,] proxima = CopiarMatriz();
                proxima[x, y - 1] = matriz[x, y];
                proxima[x, y] = matriz[x, y - 1];

                Matriz mt = new Matriz(proxima, this);
                mtr.Add(mt);
            }

            if (y + 1 < 3)
            {
                char[,] proxima = CopiarMatriz();
                proxima[x, y + 1] = matriz[x, y];
                proxima[x, y] = matriz[x, y + 1];

                Matriz mt = new Matriz(proxima, this);
                mtr.Add(mt);
            }

            return mtr;
        }

        private char[,] CopiarMatriz()
        {
            char[,] copia = { { '.', '.', '.' }, { '.', '.', '.' }, { '.', '.', '.' } };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    copia[i, j] = matriz[i, j];
                }
            }

            return copia;
        }

        public bool ValidarResultado()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matriz[i, j] != resultado[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
