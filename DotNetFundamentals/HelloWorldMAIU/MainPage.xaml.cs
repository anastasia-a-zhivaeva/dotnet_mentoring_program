using HelloWorldLibrary;

namespace HelloWorldMAIU
{
    public partial class MainPage : ContentPage
    {
        private Greeting greeting = new Greeting();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Greetings.Text = greeting.Greet(greeting.ParseNames(Names.Text));
            SemanticScreenReader.Announce(Greetings.Text);
        }
    }
}