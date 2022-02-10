using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using PokemonTcgSdk;
using System.Net;

namespace pokemonCards
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var sets = Sets.All();
            foreach (var set in sets)
            {
                comboBox1.Items.Add(set.Code.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                foreach (Control control in this.panel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    this.panel1.Controls.Remove(picture);
                }
            }
            int x = 0;
            int y = 0;
            Dictionary<string, string> query = new Dictionary<string, string>()
            {
                { "setCode", comboBox1.SelectedItem.ToString() }
            };
            var cards = Card.Get(query);
            if (cards.Cards != null)
            {
                foreach (var card in cards.Cards)
                {
                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Location = new System.Drawing.Point(x, y);
                    pictureBox1.Name = "pictureBox1";
                    pictureBox1.Size = new System.Drawing.Size(125, 175);
                    pictureBox1.Load(card.ImageUrl);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    panel1.Controls.Add(pictureBox1);
                    x = x + 130;
                    if (x > (panel1.Size.Width - 50))
                    {
                        x = 0;
                        y = y + 180;
                    }
                }
            }
        }
    }
}