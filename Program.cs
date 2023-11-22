namespace DZ4
{
    struct Card
    {
        public string Rank { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }
    }

    class Deck
    {
        public List<Card> CreateDeck()
        {
            List<string> suits = new List<string> { "Черви", "Бубны", "Трефы", "Пики" };
            List<string> ranks = new List<string> { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            List<Card> deck = new List<Card>();

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    int value = rank switch
                    {
                        "Туз" => 11,
                        "Король" => 4,
                        "Дама" => 3,
                        "Валет" => 2,
                        _ => int.Parse(rank),
                    };

                    deck.Add(new Card { Rank = rank, Suit = suit, Value = value });
                }
            }

            return deck;
        }

        public void ShuffleDeck(List<Card> deck)
        {
            Random random = new Random();
            int n = deck.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }


    }

    class Game
    {
        private List<Card> deck;
        private int playerScore;
        private int computerScore;
        private bool playerTurn;
        private int playerWins;
        private int computerWins;
        private int draws;

        public Game()
        {        
            deck = new Deck().CreateDeck();
            new Deck().ShuffleDeck(deck);
            playerTurn = FirstTurn();
            playerScore = 0;
            computerScore = 0;
            playerWins = 0;
            computerWins = 0;
            draws = 0;
        }

        public void Play()
        {
         
            while (true)
            {
                if (playerTurn)
                {
                    Console.WriteLine("Ваш ход.");
                    playerScore = PlayTurn(playerScore);
                    if (playerScore == 21 || playerScore > 21)
                        break;
                }
                else
                {
                    Console.WriteLine("Ход комп'ютера.");
                    computerScore = PlayTurn(computerScore);
                    if (computerScore == 21 || computerScore > 21)
                        break;
                }

                playerTurn = !playerTurn;
            }

           
            DisplayResults();
            Statistics();
        }

        private bool FirstTurn()
        {
          
            Console.WriteLine("Кто получается первый карты? (Игрок - 1, Компьютер - 2):");
            int choice = int.Parse(Console.ReadLine());

            return choice == 1;
        }

        private int PlayTurn(int currentScore)
        {
            
            int additionalCardValue = 0;
            while (true)
            {
                Console.WriteLine("Получить еще одну карту? (Да - '1', Нет - '2'):");
                char decision = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (decision == '1')
                {
                   
                    Card drawnCard = deck.First();
                    Console.WriteLine($"Вам выпала карта: {drawnCard.Rank} {drawnCard.Suit}");
                    additionalCardValue = drawnCard.Value;
                    currentScore += additionalCardValue;
                    deck.RemoveAt(0);

                    Console.WriteLine($"Ваш счёт теперь: {currentScore}");
                    if (currentScore >= 21)
                        break;
                }
                else if (decision == '2')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите '1' або '2'.");
                }
            }

            return currentScore;
        }

        private void DisplayResults()
        {
         
            Console.WriteLine("Игра окончена.");

            Console.WriteLine($"Ваш счёт: {playerScore}");
            Console.WriteLine($"Счёт компьютера: {computerScore}");

            if ((playerScore <= 21 && playerScore > computerScore) || (playerScore <= 21 && computerScore > 21))
            {
                Console.WriteLine("Вы выиграли");
            }
            else if ((computerScore <= 21 && computerScore > playerScore) || (computerScore <= 21 && playerScore > 21))
            {
                Console.WriteLine("Компьютер выиграл!");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }

        private void Statistics()
        {
         
            if ((playerScore <= 21 && playerScore > computerScore) || (playerScore <= 21 && computerScore > 21))
            {
                playerWins++;
            }
            else if ((computerScore <= 21 && computerScore > playerScore) || (computerScore <= 21 && playerScore > 21))
            {
                computerWins++;
            }
            else
            {
                draws++;
            }

          
            Console.WriteLine("Статистика игр:");
            Console.WriteLine($"Побед пользователя: {playerWins}");
            Console.WriteLine($"Побед компьютера: {computerWins}");
            Console.WriteLine($"Ничьи: {draws}");
        }
    }




internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            bool continuePlaying = true;

            while(continuePlaying ) 
            {
            game.Play();

            Console.WriteLine("Продолжить игру? (Да - '1', Нет - 2)");
            char decision = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            continuePlaying = (decision == '1');
            
            
            }


           
        }
    }   
}
