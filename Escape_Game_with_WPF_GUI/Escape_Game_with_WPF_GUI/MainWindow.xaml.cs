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
using Escape_Game_engine_library;

namespace Escape_Game_with_WPF_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent(); 
            new_game.FillLocationAndCharacterWithInitialItemsOnGameStartup();
            UpdateGUIview(); 
        }


        // Instantiating a new game instance 
        // (an instance of Game_Control_Center class in the Escape_Game_engine_library)
        private Game_Control_Center new_game = new Game_Control_Center();

        // A variable (field) holding the current game message to be displayed to the player.
        // The content of this field is updated by event-handlers methods.
        private string currentMessageToBeDisplayed = "~ Welcome. Try to uncover the secret of this beautiful villa. ~";

        

        #region Update GUI view
        // update the appearance of the Graphical User Interface (GUI) 
        public void UpdateGUIview()
        {
            this.current_location_message.Text = new_game.PlayersLocation.Name;
            this.location_content_display.Text = new_game.PlayersLocation.GetLocationContentAsString();
            this.characters_inventory_display.Text = new_game.Player.GetCharactersInventoryAsString();
            this.use_item_input_box_1.Text = "";
            this.use_item_input_box_2.Text = "";
            this.user_input_box.Text = "";
            this.game_message.Text = this.currentMessageToBeDisplayed;
        }
        #endregion

        #region goto locations code
        // goto location code
        private void gotoKitchenButton_Click(object sender, RoutedEventArgs e)
        {
            new_game.GoTo(new_game.kitchen);
            UpdateGUIview();        
        }

        private void gotoLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            new_game.GoTo(new_game.library);
            UpdateGUIview();       
        }

        private void gotoSwimmingPoolButton_Click(object sender, RoutedEventArgs e)
        {
            new_game.GoTo(new_game.swimmingPoolArea);
            UpdateGUIview();
        }

        private void gotoGardenButton_Click(object sender, RoutedEventArgs e)
        {
            new_game.GoTo(new_game.garden);
            UpdateGUIview();
        }
        #endregion

        #region  Event handlers for: pick up, leave, operate, use
        
        /// <summary>
        /// Evnet handler for player clicking the "pick up" button
        /// </summary>
        private void pickUpItemButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = user_input_box.Text;
            this.currentMessageToBeDisplayed = new_game.PickUpItem(userInput);
            UpdateGUIview();
        }

        /// <summary>
        /// Event handler for player clicking the "leave item" button
        /// </summary>
        private void leaveItemButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = user_input_box.Text;
            this.currentMessageToBeDisplayed = new_game.LeaveItem(userInput);
            UpdateGUIview();
        }

        /// <summary>
        /// Event handler for player clicking the "operate item" button
        /// </summary>
        private void operateItemButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = user_input_box.Text;
            this.currentMessageToBeDisplayed = new_game.OperateItem(userInput);
            UpdateGUIview();
        }

        /// <summary>
        /// Event handler for player clicking the "use item" button
        /// </summary>
        private void useItemWithItemButton_Click(object sender, RoutedEventArgs e)
        {
            string input1 = use_item_input_box_1.Text;
            string input2 = use_item_input_box_2.Text;
            this.currentMessageToBeDisplayed = new_game.UseItem(input1, input2);
            UpdateGUIview();
        }
        
        #endregion

    }
}
