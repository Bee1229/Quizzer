using System;
using System.IO;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace Quizzer
{
    internal class Program
    {
        //Made a class to represent the items that  match in the file
        class QuestionStruct
        {
            public string question;
            public List<string> answers;
            public string correct_answer;
           


            // a constructor to show the seperation of the line
            public QuestionStruct(string line)
            {
                string[] splitline = line.Split(";");
                question = splitline[0];
                answers = new List<string>();
                answers.Add(splitline[1]);
                answers.Add(splitline[2]);
                answers.Add(splitline[3]);
                correct_answer = splitline[4];
                


            }

        }

        static List<QuestionStruct> allQuestions;
        static int score;
        


        static void loadquiz()
        {
            using (StreamReader myReader = new StreamReader("QUESTIONS.txt"))
            {
                //reading the file using streamreader
                while (!myReader.EndOfStream)
                {
                    string line = myReader.ReadLine();
                    QuestionStruct newQuestion = new QuestionStruct(line);
                    allQuestions.Add(newQuestion);
                }

            }
        }


        static void playquiz()
        {

            //I added a random to loop through the file questions randomly
            //I also added score and life making the game a bit more tensing
            score = 0;
            int life = 3;
            Random rand = new Random();
            List<QuestionStruct> randomQuestions = allQuestions.OrderBy(q => rand.Next()).Take(12).ToList();
            foreach (QuestionStruct x in randomQuestions)
            {
                x.answers = x.answers.OrderBy(a => rand.Next()).ToList();
            }
           

            //Displays question and options
            for (int i = 0; i < randomQuestions.Count; i++)
            {
                Console.WriteLine(randomQuestions[i].question);
                Console.WriteLine("A. " + randomQuestions[i].answers[0]);
                Console.WriteLine("B. " + randomQuestions[i].answers[1]);
                Console.WriteLine("C. " + randomQuestions[i].answers[2]);
                Console.Write("Answer: ");
                string userAnswer = Console.ReadLine();
                if (userAnswer == randomQuestions[i].correct_answer)
                {
                    Console.WriteLine("Correct!");
                    score++;
                    Console.WriteLine("Your current score is " + score);

                }
                else
                {
                    Console.WriteLine("Incorrect! The correct answer is " + randomQuestions[i].correct_answer);
                    life--;
                    Console.WriteLine("You have " + life + " life remaining!!");
                }
                if (life == 0)
                {
                    Console.WriteLine("Game over you are out of lifes. Your current score is " + score);
                    break;
                }



                Console.WriteLine();


            }

        }

        static void Main(string[] args)
        {
            allQuestions = new List<QuestionStruct>();
            loadquiz();

            Console.WriteLine("\r\n  __________  _________  ___   __     ____  __  ________________________  _  __  ____  __  __________\r\n / ___/ __/ |/ / __/ _ \\/ _ | / /    / __ \\/ / / / __/ __/_  __/  _/ __ \\/ |/ / / __ \\/ / / /  _/_  /\r\n/ (_ / _//    / _// , _/ __ |/ /__  / /_/ / /_/ / _/_\\ \\  / / _/ // /_/ /    / / /_/ / /_/ // /  / /_\r\n\\___/___/_/|_/___/_/|_/_/ |_/____/  \\___\\_\\____/___/___/ /_/ /___/\\____/_/|_/  \\___\\_\\____/___/ /___/\r\n                                                                                                     \r\n");




            int choice = 0;



            do
            {
                Console.WriteLine("");
                Console.WriteLine("Choose an option ");
                Console.WriteLine("1 - Start quiz ");
                Console.WriteLine("2 - Check your score");
                Console.WriteLine("3 - Exit!");
                Console.WriteLine();
                choice = int.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        playquiz();
                        break;
                    case 2:
                        Console.WriteLine("Your score is: {0}", score);
                        break;
                    case 3:
                        Console.WriteLine("Thank you for playing");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }


            } while (choice != 3);
        }
    }
}