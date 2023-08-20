//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class MinMax_manager : MonoBehaviour
//{
//    private int maxDepth = 4;


//    public class Node
//    {
//        public VegamonGame State { get; set; } //Gamer
//        public List<Node> Children;

//        public Node(vegamongame state)
//        {
//            State = state;
//            Children = new List<Node>();
//        }
//    }

//    Node rootNode = new Node(vegamongame); // Creates the AI Nodes


//    BestOption bestMove = Minimax(rootNode, maxDepth, int.MinValue, int.MaxValue, true).Move;

//    public Node GenerateGameTree(VegamonGame gameState, int depth)
//    {
//        Node rootNode = new Node(gameState);
//        GenerateGameTreeRecursive(rootNode, depth, true); // Assuming the player to maximize is the AI

//        return rootNode;
//    }

//    private void GenerateGameTreeRecursive(Node parentNode, int depth, bool maxPlayer)
//    {
//        if (depth == 0 || parentNode.State.IsGameOver())
//        {
//            // Terminal node or reached maximum depth
//            return;
//        }

//        // Generate all possible moves from the current state
//        List<BattleMove> possibleMoves = parentNode.State.GeneratePossibleMoves();

//        foreach (BattleMove move in possibleMoves)
//        {
//            // Create a new child node for each possible move
//            VegamonGame nextState = parentNode.State.ApplyMove(move);
//            Node childNode = new Node(nextState);

//            parentNode.Children.Add(childNode);

//            // Recurse to generate the tree for the child node
//            GenerateGameTreeRecursive(childNode, depth - 1, !maxPlayer);
//        }
//    }

//    public enum BattleStates2 { START, Player1, AI, WIN, LOSE, }
//    public enum DifficultyLevel { Easy, Meduim, Hard, }

//    public BattleStates2 states2;
//    public DifficultyLevel level;
//    public GameObject Player_1;
//    public GameObject Ai;

//    public DifficultyLevel selectedDifficulty;

//    //Unit_Script Player_1Unit;
//    Ai_creature Ai_Unit;
//    Creature1 Player_1Unit;

//    Difficulty_selection Difficulty_Man;

//    public Transform Player_1Spawn;
//    public Transform Ai_Spawn;

//    public Text Creature_1Name;
//    public Text Ai_Name;
//    public Text Dialogue;
//    public Text Creature1CurrentHp;
//    public Text Creature3CurrentHp;
//    public Slider HpSlider;

//    Button Pl_1Attack;
//    Button Pl_1Heal;
//    Button Pl_1Block;
//    Button Pl_1Special;

//    Button Ai_Special;
//    Button Ai_Attack;
//    Button Ai_Heal;
//    Button Ai_Block;

//    public int Player_1healCount = 0;
//    public int Ai_healCount = 0;
//    public int Player_1SpecialUse = 0;
//    public int Ai_SpecialUse = 0;

//    // Start is called before the first frame update

//    void Start()
//    {
//        Pl_1Attack = GetComponent<Button>();
//        Pl_1Heal = GetComponent<Button>();
//        Pl_1Block = GetComponent<Button>();
//        Pl_1Special = GetComponent<Button>();

//        Ai_Special = GetComponent<Button>();
//        Ai_Attack = GetComponent<Button>();
//        Ai_Heal = GetComponent<Button>();
//        Ai_Block = GetComponent<Button>();

//        Difficulty_Man = GetComponent<Difficulty_selection>();


//        states2 = BattleStates2.START;
//        startBattle();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void startBattle()
//    {
//        GameObject Player_1Turn = Instantiate(Player_1, Player_1Spawn);
//        Player_1Unit = Player_1Turn.GetComponent<Creature1>();

//        GameObject Ai_Turn = Instantiate(Ai, Ai_Spawn);
//        Ai_Unit = Ai_Turn.GetComponent<Ai_creature>();


//        Creature_1Name.text = Player_1Unit.creature1Name;

//        Ai_Name.text = Ai_Unit.creatureName;

//        Dialogue.text = "Let the battle begin.";

//        Who_Starts();
//        //states2 = BattleStates2.Player1;

//        //Player_1BattleTurn();
//    }

//    public int RandomNumber()
//    {
//        int New = Random.Range(1, 3);
//        return New;
//    }

//    public void Player_1BattleTurn() //Displays message of who goes next
//    {
//        //Hide_P2_UI_Show_P1_UI();
//        Dialogue.text = "Its Player 1's turn.... Please choose an action.";
//    }

//    public void Ai_BattleTurn() //Displays message of who goes next
//    {
//        //Hide_P1_UI_Show_P2_UI();
//        Dialogue.text = "The Ai is attacking.";
//    }

//    /*public void CallButtonClick()
//    {
//        if (Difficulty_Man != null)
//        {
//            Difficulty_Man.OnbuttonClick();
//        }
//    } */

//    public void Who_Starts()
//    {
//        Debug.Log("Difficulty man");
//        // Debug.Log(Difficulty_Man.states2);

//        //CallButtonClick();
//        if (selectedDifficulty == DifficultyLevel.Easy)
//        {
//            states2 = BattleStates2.Player1;
//            Player_1BattleTurn();
//        }

//        else if (selectedDifficulty == DifficultyLevel.Hard)
//        {
//            states2 = BattleStates2.AI;

//            Ai_BattleTurn();
//        }
//        else if (selectedDifficulty == DifficultyLevel.Meduim)
//        {
//            if (RandomNumber() == 1)
//            {
//                states2 = BattleStates2.Player1;

//                Player_1BattleTurn();
//            }
//            else
//            {
//                states2 = BattleStates2.AI;
//                Ai_BattleTurn();
//            }
//        }
//    }

//    public void OnAttackPlayer_1Button() //Handels what happens if Player one presses the attack button on screen and checks if it is players turn to attack
//    {
//        if (states2 != BattleStates2.Player1)
//            return;
//        else
//        {
//            Ai_BattleTurn();
//            Player_1Attack();
//            Debug.Log("it works");
//        }
//    }

//    private BattleOption Minimax(BattleGameTree.Node node, int depth, int alpha, int beta, bool maxPlayer)
//    {
//        if (depth == 0 || node.Children.Count == 0)
//        {
//            // Reached a terminal node or maximum depth
//            int utilityValue = EvaluateGameState(node.State);
//            return new MinimaxResult(utilityValue);
//        }

//        MinimaxResult bestResult;
//        if (maximizingPlayer)
//        {
//            bestResult = new MinimaxResult(int.MinValue);
//            foreach (BattleGameTree.Node childNode in node.Children)
//            {
//                MinimaxResult result = Minimax(childNode, depth - 1, alpha, beta, false);
//                bestResult = Max(bestResult, result);
//                alpha = Max(alpha, bestResult.UtilityValue);

//                // Alpha-beta pruning
//                if (beta <= alpha)
//                {
//                    break;
//                }
//            }
//        }
//        else
//        {
//            bestResult = new MinimaxResult(int.MaxValue);
//            foreach (BattleGameTree.Node childNode in node.Children)
//            {
//                MinimaxResult result = Minimax(childNode, depth - 1, alpha, beta, true);
//                bestResult = Min(bestResult, result);
//                beta = Min(beta, bestResult.UtilityValue);

//                // Alpha-beta pruning
//                if (beta <= alpha)
//                {
//                    break;
//                }
//            }

//            bestResult.Move = node.Children.Find(child => child.UtilityValue == bestResult.UtilityValue).AppliedMove;
//            return bestResult;
//        }

//        private int EvaluateGameState(vegamongame state)
//        {
//            // Implementation of utility function to evaluate the game state will come over here
//            int utilityValue = 0;

//            // Evaluate based on health left for player
//            int player1HP = attack(AIHP, Player.Player1);
//            int AIHP = attack(gameState, Player.AI);
//            utilityValue += player1HP - AIHP;

//            // Evaluate based on Player HP
//            int player1HP = Calculate(gameState, Player.AI);
//            int AIHP = CalculateHP(gameState, Player.Player1);
//            utilityValue += player1HP - AIHP;

//            // Add other evaluation factors based on your desired strategy

//            return utilityValue;


//        }

//        bestResult.Move = node.Children.Find(child => child.UtilityValue == bestResult.UtilityValue).AppliedMove;
//        return bestResult;
//    }

//    private int CalculatedefensiveOption(VegamonGame gameState, AI player)
//    {
//        // Calculate the option which has a higher lilkihood of reducing the players HP to 0
//        int ATK = 0;

//        for (int row = 0; row < 8; row++)
//        {
//            for (int col = 0; col < 8; col++)
//            {
//                Move option = gameState.AIManager;
//                if (option != null && AIManager.Player == player)
//                {
//                    // Assign higher value to attacking moves such as special atk or attack
//                    int Block = player == AI.Player1
//                    Special += player HP;
//                }
//            }
//        }

//        return positionValue;
//    }

//    private int CalculateCombatOption(VegamonGamestate gameState, AI player)
//    {
//        // Calculate the priority value of a attack move vs a defensive move
//        int positionValue = 0;

//        ATK option = gameState.AIManager;
//        if (option != null && Heal.AI == player)
//        {
//            // Assign higher priority to damaging moves
//            int rowValue = player == Player.Player1 ? row : 7 - row;
//            positionValue += rowValue;
//        }



//        return positionValue;
//    }


//    public void OnAttackAi_Button() //Handels what happens if Player two presses the attack button on screen and checks if it is players turn to attack
//    {
//        if (states2 != BattleStates2.AI)
//            return;
//        else
//        {
//            Player_1BattleTurn();
//            Ai_2Attack();
//            Debug.Log("it works again");
//        }
//    }



//    public void Player_1Attack() // Checks to see if the player isdead otherwise it continues with the action of attacking
//    {
//        bool isDead = Ai_Unit.DamageTookC3(Player_1Unit.creature1Dmg);

//        if (isDead)
//        {
//            states2 = BattleStates2.WIN;
//            BattleEndP1();
//        }
//        else
//        {
//            states2 = BattleStates2.AI;
//            Ai_BattleTurn();
//            OnAttackAi_Button();

//        }
//    }

//    private MinimaxResult Max(MinimaxResult result1, MinimaxResult result2)
//    {
//        return result1.UtilityValue > result2.UtilityValue ? result1 : result2;
//    }

//    public void Ai_2Attack() // Checks to see if the player isdead otherwise it continues with the action of attacking
//    {
//        bool isDead2 = Player_1Unit.DamageTook(Ai_Unit.creature3Dmg);

//        if (isDead2)
//        {
//            states2 = BattleStates2.LOSE;
//            BattleEndAi();
//            Debug.Log("why");
//        }
//        else
//        {
//            states2 = BattleStates2.Player1;
//            Player_1BattleTurn();
//        }
//    }

//    private MinimaxResult Min(MinimaxResult result1, MinimaxResult result2)
//    {
//        return result1.UtilityValue < result2.UtilityValue ? result1 : result2;
//    }

//    public void BattleEndP1() //Displays Texts that tells the player if they won or not
//    {
//        if (states2 == BattleStates2.WIN)
//        {
//            //Dialogue.text = "you win player 1";
//            SceneManager.LoadScene(7);
//        }
//    }

//    void BattleEndAi()//Displays Texts that tells the player if they won or not
//    {
//        if (states2 == BattleStates2.LOSE)
//        {
//            //Dialogue.text = "Player 2 won";
//            SceneManager.LoadScene(8);
//        }
//    }

//    public void Player_1HealthRestored() //Used to restore health of the player and limits the amount of potions that can be used.
//    {

//        if (states2 != BattleStates2.Player1)
//            return;
//        if (Player_1healCount < 3)
//        {
//            Player_1healCount++;
//            Player_1Unit.Player_1Heal(5);
//            Dialogue.text = "Health potion used";

//            states2 = BattleStates2.AI;
//            Ai_Blocked();
//            Debug.Log("health works");
//        }
//        else
//        {
//            Dialogue.text = "You have no potions.";
//        }

//    }

//    public void Ai_HealthRestored()//Used to restore health of the player and limits the amount of potions that can be used.
//    {
//        if (states2 != BattleStates2.AI)
//            return;
//        if (Ai_healCount < 3)
//        {
//            Ai_healCount++;
//            Ai_Unit.Ai_Heal(7);
//            Dialogue.text = "Health potion used";
//            Debug.Log("health works for me too");

//            states2 = BattleStates2.Player1;
//        }
//        else
//        {
//            Dialogue.text = "You have none potions.";
//            OnAttackAi_Button();
//        }
//    }

//    public void Player_1Blocked()//Used to minus the damage that other players commit.
//    {
//        if (states2 != BattleStates2.Player1)
//            return;
//        Player_1Unit.Blocked();

//        Debug.Log("Pls let this work");

//        states2 = BattleStates2.AI;
//        Ai_HealthRestored();
//    }

//    public void Ai_Blocked()//Used to minus the damage that other players commit.
//    {
//        if (states2 != BattleStates2.AI)
//            return;
//        Ai_Unit.Ai_Blocked();

//        Debug.Log("This needs to work");

//        states2 = BattleStates2.Player1;
//    }
//    /* public void SetHp(int Hp)
//     {
//        Creature1CurrentHp = creature1CurrentHp.ToString();
// }*/
//    public void Player1_Special()//Handels the specials of the players.
//    {
//        if (states2 != BattleStates2.Player1)
//            return;
//        if (Player_1SpecialUse < 2)
//        {
//            Player_1SpecialUse++;
//            Ai_BattleTurn();
//            Player_1Attack();
//            HealthUltP1();
//            Debug.Log("its special");

//            states2 = BattleStates2.AI;
//            Ai_2Special();
//        }
//        else
//        {
//            Dialogue.text = "No special Available.";
//        }
//    }

//    public void HealthUltP1() // The same as the health execpt without the restrictions
//    {
//        Player_1Unit.Player_1Heal(7);
//        Dialogue.text = "Health gained";
//        Debug.Log("health alt works");
//    }

//    public void Ai_2Special()//Handels the specials of the players.
//    {

//        if (states2 != BattleStates2.AI)
//            return;
//        if (Ai_SpecialUse < 2)
//        {
//            Ai_SpecialUse++;
//            Player_1BattleTurn();
//            Ai_2Attack();
//            Ai_Blocked();
//            Debug.Log("its so special");

//            states2 = BattleStates2.Player1;
//        }
//        else
//        {
//            Dialogue.text = "No super Available.";
//            OnAttackAi_Button();
//        }
//    }

//    public void Slider(Creature1 creature1)
//    {
//        HpSlider.maxValue = creature1.creature1MaxHp;
//        HpSlider.value = creature1.creature1CurrentHp;
//    }

//    public void Ai_Slider(Ai_creature ai_Creature)
//    {
//        HpSlider.maxValue = ai_Creature.creature3MaxHp;
//        HpSlider.value = ai_Creature.creature3CurrentHp;
//    }

//    public void SetHp(int hp)
//    {
//        HpSlider.value = hp;
//    }

//    private struct MinimaxResult //gets the result of the MinMaxs search
//    {
//        public int UtilityValue;
//        public VegamonOption option;

//        public MinimaxResult(int utilityValue)
//        {
//            UtilityValue = utilityValue;
//            option = null;
//        }
//    }
//    //Have mercy on our souls alice
//}

