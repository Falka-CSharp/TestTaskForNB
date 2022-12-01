using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using TestTaskForNB.Data;
using TestTaskForNB.Models;
namespace TestTaskForNB
{
    public class Program
    {
        static public void LoadingIconThread(object inputText)
        {
            try
            {
                string text = (string)inputText;
                while (true)
                {

                    Console.WriteLine($"{text}.");
                    Thread.Sleep(500);
                    Console.Clear();


                    Console.WriteLine($"{text}..");
                    Thread.Sleep(500);
                    Console.Clear();


                    Console.WriteLine($"{text}...");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
            catch (ThreadInterruptedException) { }
        }
        static public void ClearDatabase(PostsDbContext postsDbContext)
        {
            //Delete all records from database
            Console.WriteLine("Deleting records...");
            foreach (var item in postsDbContext.Posts)
            {
                postsDbContext.Posts.Remove(item);
            }
            foreach (var item in postsDbContext.Rubrics)
            {
                postsDbContext.Rubrics.Remove(item);
            }
            postsDbContext.SaveChanges();
            Console.WriteLine("Deleted records");
        }

        static public void FindPostMenuItem(PostsDbContext postsDbContext)
        {
            Console.Write("Enter search request: ");
            string? request = Console.ReadLine();
            if (request != null)
            {
                List<Post> results = postsDbContext.Posts.Where(p => p.PostText.Contains(request))
                     .Include(p => p.PostRubrics)
                    .OrderBy(p => p.PostCreatingDate)
                    .Take(20)
                    .ToList();
                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"{i + 1} -> " + results[i].PostCreatingDate);
                    Console.WriteLine($"Id -> {results[i].Id}");
                    if (results[i].PostRubrics != null)
                    {
                        Console.WriteLine("--Rubrics--");
                        foreach (Rubric rb in results[i].PostRubrics ?? new List<Rubric>())
                        {
                            Console.WriteLine($"> {rb.RubricName}");
                        }
                    }
                    Console.WriteLine("-------------------");

                    Console.WriteLine(results[i].PostText);
                    Console.WriteLine("-------------------");

                }
            }
            else
            {
                Console.WriteLine("Please enter search request!");
            }
        }
       
        static public void DeletePostMenuItem(PostsDbContext postsDbContext)
        {
            Console.Write("Enter post id: ");
            int postId = int.Parse(Console.ReadLine() ?? "-1");
            int rowsAffected = postsDbContext.Posts.Where(p => p.Id == postId).ExecuteDelete();
            postsDbContext.SaveChanges();
            Console.WriteLine($"Deleted! Rows affected: {rowsAffected}");
        }

        static public void ShowMenu()
        {
            Console.WriteLine("------Menu------");
            Console.WriteLine("1 - Find post by text");
            Console.WriteLine("2 - Delete post by id");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("----------------");
        }
        static public char getUserInput(string output="")
        {
            Console.Write(output);
            return Console.ReadKey().KeyChar;
        }

        static public void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            PostsDbContext postsDbContext = new PostsDbContext();

            //Loading database
            Thread ldThread = new Thread(new ParameterizedThreadStart(LoadingIconThread));
            ldThread.Start("Seeding data");
            SeedData.FillDataWithCsv(postsDbContext,"posts.csv");
            ldThread.Interrupt();
            Console.Clear();
            //----------------
            //Welcome messages
            Console.WriteLine("Welcome to the posts database!");
            Console.WriteLine($"Database statistic:" +
                $"\nPosts: {postsDbContext.Posts.Count()} records" +
                $"\nRubrics: {postsDbContext.Rubrics.Count()} records");
            //----------------

            //Menu loop
            char userChoice;
            do
            {
                ShowMenu();
                userChoice = getUserInput("Enter your choice: ");
                Console.WriteLine();
                switch (userChoice)
                {
                    case '1':
                        FindPostMenuItem(postsDbContext);
                        break;
                    case '2':
                        DeletePostMenuItem(postsDbContext);
                        break;
                }
            } while (userChoice != '0');
            //----------------


        }
    }
}