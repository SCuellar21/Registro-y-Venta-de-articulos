using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasPARAEXAMEN
{
    public partial class Form1 : Form
    {
        public struct Registros
        {
            public String Clave;
            public String Nombre;
            public Double Precio;
        }
        Registros[] articulo = new Registros[150];

        public int num_articulos = 0, agregados = 0, total = 0;

        // Busca si el articulo existe y devielve su lugar en el numero de articulos que hay
        public int BuscarLugar(String Clave)
        {
            for (int i = 0; i <= num_articulos; i++)
            {
                if (articulo[i].Clave == Clave)
                {
                    return i;
                }
            }
            return -1;
        }
        //public int search(string Clave, int inicio, int final) // search(textBox1.Text, 0, num_articulos - 1)
        //{
        //    if (final >= 0)
        //    {
        //        int medio = (inicio + final) / 2;
        //        if (articulo[medio].Clave == Clave)
        //        {
        //            return medio;
        //        }
        //        else if (Convert.ToInt32(articulo[medio].Clave) < Convert.ToInt32(Clave))
        //        {
        //            search(Clave, medio + 1, final);
        //        }
        //        else if (Convert.ToInt32(articulo[medio].Clave) > Convert.ToInt32(Clave))
        //        {
        //            search(Clave, inicio, medio);
        //        }
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}

        public Form1()
        {
            InitializeComponent();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox3.Visible = false;
            listBox1.Visible = false;
            // limpia la lista de venta
            listBox1.Items.Clear();
            agregados = 0;
            tClave.Focus();
        }

        // Termina los movimientos
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Busca el articulo por clave
        private void bBuscar_Click(object sender, EventArgs e)
        {
            if (BuscarLugar(tClave.Text) < 0)   //no existe
            {
                groupBox2.Visible = true;
                tArticulo.Clear();
                tPrecio.Clear();
                tArticulo.Focus();
                bBajas.Visible = false;
                bAlta.Visible = true;
                bCambios.Visible = false;
            }
            else   //si existe
            {
                tArticulo.Text = articulo[BuscarLugar(tClave.Text)].Nombre;
                tPrecio.Text = articulo[BuscarLugar(tClave.Text)].Precio.ToString();
                groupBox2.Visible = true;
                bAlta.Visible = false;
                bCambios.Visible = true;
                bBajas.Visible = true;

            }
        }

        // Agrega el articulo con el nombre y precio dados
        private void button3_Click(object sender, EventArgs e)
        {
            articulo[num_articulos].Clave = tClave.Text;
            articulo[num_articulos].Nombre = tArticulo.Text;
            articulo[num_articulos].Precio = Convert.ToDouble(tPrecio.Text);
            num_articulos++;
            tClave.Clear();
            groupBox2.Visible = false;
            tClave.Focus();
        }

        // Realiza cambios en el articulo con el nombre y precio dados
        private void button5_Click(object sender, EventArgs e)
        {
            articulo[BuscarLugar(tClave.Text)].Nombre = tArticulo.Text;
            articulo[BuscarLugar(tClave.Text)].Precio = Convert.ToDouble(tPrecio.Text);
            tArticulo.Clear();
            tClave.Clear();
            tPrecio.Clear();
            groupBox2.Visible = false;
            tClave.Focus();
        }

        // Elimina el articulo de la lista
        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = BuscarLugar(tClave.Text); i < num_articulos; i++)
            {
                articulo[i].Clave = articulo[i + 1].Clave;
                articulo[i].Nombre = articulo[i + 1].Nombre;
                articulo[i].Precio = articulo[i + 1].Precio;
            }
            num_articulos--;
            tArticulo.Clear();
            tClave.Clear();
            tPrecio.Clear();
            groupBox2.Visible = false;
            tClave.Focus();
        }

        // Cancela la busqueda
        private void bCancelar_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            tClave.Clear();
            tClave.Focus();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Location = new Point(12, 27);
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            listBox1.Visible = false;
            // limpia la lista de venta
            listBox1.Items.Clear();
            agregados = 0;
            tClaveVenta.Focus();
        }

        // Busca el articulo por clave
        private void bBuscarVenta_Click(object sender, EventArgs e)
        {
            if (BuscarLugar(tClaveVenta.Text) < 0)   //no existe
            {
                tArticuloVenta.Clear();
                tPrecioVenta.Clear();
                tCantidadVenta.Clear();
                MessageBox.Show("El articulo " + tClaveVenta.Text + " no existe");
                tClaveVenta.Clear();
                tClaveVenta.Focus();
                // MessageBoxIcon.Warning
            }
            else   //si existe
            {
                tArticuloVenta.Text = articulo[BuscarLugar(tClaveVenta.Text)].Nombre;
                tPrecioVenta.Text = articulo[BuscarLugar(tClaveVenta.Text)].Precio.ToString();
                tCantidadVenta.Clear();
                tCantidadVenta.Focus();
            }
        }

        // agrega el articulo a la lista de venta
        private void bAgregar_Click(object sender, EventArgs e)
        {
            // manda mensaje de error si el usuario no escribio algun campo
            if(tClaveVenta.Text == "" || tArticuloVenta.Text == "" || tPrecioVenta.Text == "" || tCantidadVenta.Text == "")
            {
                MessageBox.Show("Llena todos los campos antes de agregar a la venta.");
                return;
            }
            agregados++;

            // agrega el articulo a la lista de venta
            listBox1.Items.Add(tClaveVenta.Text + "  |  " + tArticuloVenta.Text + "  |   $" + tPrecioVenta.Text + "  |  " + tCantidadVenta.Text);

            total = total + Convert.ToInt32(tPrecioVenta.Text) * Convert.ToInt32(tCantidadVenta.Text);

            tClaveVenta.Clear();
            tArticuloVenta.Clear();
            tPrecioVenta.Clear();
            tCantidadVenta.Clear();
            pictureBox1.Visible = true;
            tClaveVenta.Focus();
        }

        // termina la venta y muestra la lista y el total
        private void button4_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            listBox1.Location = new Point(12, 27);

            // avisa si la venta se terminó estando vacia
            if(listBox1.Items.Count == 0)
            {
                MessageBox.Show("Ningun articulo en la lista de venta");
                return;
            }
            else
            {
                listBox1.Items.Insert(0, "Clave  Articulo  Precio  Cantidad");
                listBox1.Items.Add("El total a pagar es:  $" + total); 
            }

            listBox1.Visible = true;
            total = 0;
            pictureBox1.Visible = false;
        }

        private void tClaveVenta_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}
