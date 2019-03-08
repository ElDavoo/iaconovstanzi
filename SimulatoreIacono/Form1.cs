using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatoreIacono
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //genero un oggetto di tipo random
            Random r = new Random();
            //Creo un dizionario per verificare che il numero non sia già uscito
            Dictionary<string, string> esercizi = new Dictionary<string, string>();
            //creo un contatore per verificare che ho generato correttamente 6 esercizi
            int i = 1;
            //finche i < 6
            while (i <= 6) {
                //creo la stringa relativa alla tipologia
                string tipologia = "E" + ((r.Next() % 19)).ToString("0");
                //ultimo esercizio: teoria
                if (i == 6) tipologia = "T";
                //ottengo quanti file ci sono nella directory
                System.IO.DirectoryInfo dirr = new System.IO.DirectoryInfo(tipologia);
                int count = dirr.GetFiles().Length;
                //creo la variabile relativa al numero dell'esercizio
                string esercizio = ((r.Next() % count)).ToString("0") + ".png";
                //se la collezione non contiene il numero generato
                if (!esercizi.ContainsKey(tipologia)) {
                    //inserisco il numero nella collezione
                    esercizi.Add(tipologia, esercizio);
                    //incremento l'indice
                    i++;
                }
            }
            //creo un indice per gestire i px
            int pixelTop = 0;
            //per ogni elemento all'interno della collezione esercizi
            foreach (var item in esercizi) {
                //verifico se esiste la bitmap
                string path = $@"{ Application.StartupPath}\{item.Key}\{item.Value}";
                //provo a creare una bitmap
                try {
                    //creo l'immagine
                    Image imgo = new Bitmap(path);
                    Image img = new Bitmap(imgo, new Size(imgo.Width*6/10, imgo.Height*6/10));
                    //creo il controllo
                    PictureBox picture = new PictureBox();
                    //aggiungo la sorgente
                    picture.Image = img;
                    //setto l'altezza
                    picture.Height = img.Height;
                    //setto la larghezza
                    picture.Width = img.Width;
                    //setto top 
                    picture.Top = pixelTop;
                    //setto left
                    picture.Left = 0;
                    //bordo sulla pictureBox
                    picture.BorderStyle = BorderStyle.Fixed3D;
                    //aggiungo l'immagine al pannello
                    Container.Controls.Add(picture);
                    //incremento l'indice di pixelTop
                    pixelTop += picture.Height;
                } catch (FileNotFoundException ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            //se altezza inferiore a 1000 
            if (pixelTop < 800) {
                //setto l'altezza del container
                Container.Height = pixelTop;
                //setto l'altezza del form
            }
        }
    }
}
