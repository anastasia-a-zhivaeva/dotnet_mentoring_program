using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelloWorldLibrary;

namespace HelloWorldWinForms
{
    public partial class GreetingForm : Form
    {
        private Greeting greeting = new Greeting();
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            greetings.Text = greeting.Greet(greeting.ParseNames(names.Text));
        }
    }
}
