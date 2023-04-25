using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloWorldWinForms
{
    public partial class GreetingForm : Form
    {
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            var greetingNames = names.Text.Split(',').Where(s => s.Length > 0).ToArray();
            greetings.ResetText();

            if (greetingNames.Length > 0)
            {
                foreach (var name in greetingNames)
                {
                    greetings.Text += $"Hello, {name}!\n";
                }
            }
            else
            {
                greetings.Text = "Hello, stranger!";
            }
        }
    }
}
