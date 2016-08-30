using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.IO;
using System.Xml;
//Create a new WPF project and build an Options screen in a Canvas. Start by creating 3 Stacked Panels and use the Expander so that 
//they can be opened and collapsed. Note that all of these controls are actually containers for other controls (objects inside objects 
//inside objects -- gives you the chills, eh?).
 
//The first panel should be about the “Room Size” and include Label-TextBox pairs that set the length and width. Include a CheckBox on 
//this panel that has a text value of “Square It” or something like that. Wire-up Event Handlers to your controls so that when the CheckBox 
//is checked, the width TextBox is disabled. Add a handler to the length TextBox change event so that, if the CheckBox is checked, it sets 
//the value of the width TextBox equal to what is in the length. Also, prevent user input from going negative in both TextBoxes (set them to 0 
//if they do).
 
//The second panel should have a Label and ComboBox. The Label should say “Difficulty” and the ComboBox should have items for “Easy”, 
//“Normal”, and “Hard”. There are no events to wire-up here.
 
//The third panel should have a Label for “Player Color”, a ComboBox with items “Red”, “Blue”, “Green”, and a small Rectangle control. 
//Set the fill property of the Rectangle to “Red” and the ComboBox to Red as well (on startup). Wire-up an event on the ComboBox so that 
//it changes the Fill property of the Rectangle to whatever the user selected.
namespace SE310_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable]
    public partial class MainWindow : Window
    {
        SolidColorBrush mySolidColorBrush = new SolidColorBrush(); // variable for brush color

        //Create a serializable place holder for length and width
        SerializableSave dataSave = new SerializableSave();

        public MainWindow()
        {//starts program
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        { // method for setting length and width the same when box checked
            TextBoxWidth.IsEnabled = false;
            TextBoxWidth.Text = TextBoxLength.Text;
        }

        private void CheckBoxSquare_Unchecked(object sender, RoutedEventArgs e)
        { // method for setting length and width seperatly when the box is uncheked
            TextBoxWidth.IsEnabled = true;
            TextBoxWidth.Text = "0";
        }
        
        private void TextBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        { // checks for negative value in length
            if (TextBoxLength.Text == "")
            {
                TextBoxLength.Text = "0";
            }

            if (Convert.ToInt32(TextBoxLength.Text) < 0)
            {
                TextBoxLength.Text = "0";
            }
        }

        private void TextBoxWidth_TextChanged(object sender, TextChangedEventArgs e)
        { // check for negative value in width
            if (TextBoxWidth.Text == "")
            {
                TextBoxWidth.Text = "0";
            }

            if (Convert.ToInt32(TextBoxWidth.Text) < 0)
            {
                TextBoxWidth.Text = "0";
            }
        }

        private void Red_Selected(object sender, RoutedEventArgs e)
        { // sets rectange to red
            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
            RectanglePlayerColorView.Fill = mySolidColorBrush;
            EllipsePlayer.Fill = mySolidColorBrush;
        }

        private void Blue_Selected(object sender, RoutedEventArgs e)
        { // set rectange to blue
            mySolidColorBrush.Color = Color.FromRgb(0,0,255);
            RectanglePlayerColorView.Fill = mySolidColorBrush;
            EllipsePlayer.Fill = mySolidColorBrush;
        }

        private void Green_Selected(object sender, RoutedEventArgs e)
        { // set rectangle to green
            mySolidColorBrush.Color = Color.FromRgb(0, 255, 0);
            RectanglePlayerColorView.Fill = mySolidColorBrush;
            EllipsePlayer.Fill = mySolidColorBrush;
        }

        private void SliderOvalShape_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            EllipsePlayer.Width = SliderOvalShape.Value * 10 + 50 ;
            InkCanvasFace.Width = EllipsePlayer.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InkCanvasFace.Strokes.Clear();
            SoundPlayer player = new SoundPlayer("clean-up.wav");
            player.PlaySync();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Send data to be saved to serialized place holder
            dataSave.Length = Convert.ToString(TextBoxLength);
            dataSave.Width = Convert.ToString(TextBoxWidth);
            //Open or create a new file called testing, then store the length and width
            FileStream fs = File.Open("Testing.Xaml", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(dataSave.Length);
            sw.WriteLine(dataSave.Width);
            //Close the file
            sw.Close();
            fs.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //Open the existing file to read from
            FileStream fs = File.OpenRead("Testing.Xaml");
            StreamReader sw = new StreamReader(fs);
            //Search for the ' ' key within the file before reading the keys into the text box
            do
            {

            } while (sw.Read() != 32);
            TextBoxLength.Text = Convert.ToString(sw.ReadLine());

            //Search for the ' ' key within the file before reading the keys into the text box
            do
            {

            } while (sw.Read() != 32);
            TextBoxWidth.Text = Convert.ToString(sw.ReadLine());
            //Close the file before exiting the program
            sw.Close();
            fs.Close();
        }
    }
}
