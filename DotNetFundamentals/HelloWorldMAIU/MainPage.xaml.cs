using HelloWorldLibrary;

namespace HelloWorldMAIU
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            var greetingNames = Names.Text?.Split(',').Where(s => s.Length > 0).ToArray() ?? Array.Empty<string>();
            Greetings.Text = String.Empty;

            if (greetingNames.Length > 0)
            {
                foreach (var name in greetingNames) {
                    Greetings.Text += Greeting.Greet(name);
                    Greetings.Text += "\n";
                }
            }
            else
            {
                Greetings.Text = Greeting.Greet();
            }
            SemanticScreenReader.Announce(Greetings.Text);
        }
    }
}