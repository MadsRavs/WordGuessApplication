namespace WordGuessApplication
{
    public partial class Form1 : Form
    {
        //array of topics to choose from to be stored in a combo box
        private string[] topics = { "Food", "Animals", "Countries", "Pokemon" };
        //array of words from a topic
        private string[] Food = { "APPLE", "BURGER", "FRIES", "ORANGE", "CHEESE" }; 
        private string[] Animals = { "DEER", "CHICKEN", "COW", "RABBIT", "HORSE" };
        private string[] Countries = { "INDIA", "JAPAN", "CHINA", "ETHIOPIA", "CHILE" };
        private string[] Pokemon = { "CHARMANDER", "PIKACHU", "BULBASAUR", "SQUIRTLE", "MEW" };
        //initalizing the random secret word
        private string secretWord;
        //Randomization
        private Random random = new Random(); 
        private int wrongAttempts = 0;

        public Form1()
        {
            InitializeComponent();
            //to store te list of topcs inside the combo box
            comboBox1.Items.AddRange(topics);

            //this basically starts the game if a topic is picked or changed
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        //starts the game by calling it 
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeGame();
        }

        //game proper
        private void InitializeGame()
        {
            //propmts the user to pick a topic first
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a topic first.");
                return;
            }
            //gets the selected topic
            string selectedTopic = comboBox1.SelectedItem.ToString(); 
            string[] words = GetWordsByTopic(selectedTopic);

            //choosing a random word from the array of the slected topic and display it
            secretWord = words[random.Next(words.Length)];
            label1.Text = GetMaskedWord(secretWord);

            //clears the wrong attempts section adn resets the counter
            label3.Text = "";
            wrongAttempts = 0; 
            label4.Text = wrongAttempts.ToString();
        }

        //method in which the arrays of topics are accesed
        private string[] GetWordsByTopic(string topic)
        {
            switch (topic)
            {
                case "Food":
                    return Food;
                case "Animals":
                    return Animals;
                case "Countries":
                    return Countries;
                case "Pokemon":
                    return Pokemon;
                default:
                    return new string[] { };
            }
        }

        //method to replace the middle characters in exchange of dash lines
        private string GetMaskedWord(string word)
        {
            if (word.Length <= 2)
            {
                return word;
            }

            //replace the middle letters with dash lines
            return word[0] + new string('-', word.Length - 2) + word[^1];
        }

        //button event handler all input validation are here
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                Console.Beep();
                MessageBox.Show("Please provide an answer.");
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "^[a-zA-Z]+$"))
            {
                Console.Beep();
                MessageBox.Show("Please enter only letters.");
                textBox1.Clear();
                return;
            }

            //passes the word from the text box and capitalizing it to compare with the secret word
            string guessedWord = textBox1.Text.ToUpper();
            if (guessedWord.Equals(secretWord))
            {
                label1.Text = secretWord;
                Console.Beep();
                MessageBox.Show("Congratulations! You've guessed the word!");
                wrongAttempts = 0;
                label4.Text = wrongAttempts.ToString();
                InitializeGame(); //reset the game after a win

            }
            else
            {
                //if the guess is wrong, wrong attempts will be listed on the wrong attempts section
                wrongAttempts++;
                label3.Text += guessedWord + "\n";
                label4.Text = wrongAttempts.ToString();
            }
            textBox1.Clear();
        }
    }
}
