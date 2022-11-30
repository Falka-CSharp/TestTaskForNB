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
        static public void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            PostsDbContext postsDbContext = new PostsDbContext();


            //Delete records section
            //Console.WriteLine("Deleting records...");
            //foreach (var item in postsDbContext.Posts)
            //{
            //    postsDbContext.Posts.Remove(item);
            //}
            //foreach (var item in postsDbContext.Rubrics)
            //{
            //    postsDbContext.Rubrics.Remove(item);
            //}
            //postsDbContext.SaveChanges();
            //Console.WriteLine("Deleted records");


            //-------------------
            Thread ldThread = new Thread(new ParameterizedThreadStart(LoadingIconThread));
            ldThread.Start("Seeding data");
            SeedData.FillDataWithCsv(postsDbContext,"posts.csv");
            ldThread.Interrupt();
            Console.Clear();

            Console.WriteLine("Welcome to the posts database!");
            Console.WriteLine($"Database statistic:" +
                $"\nPosts: {postsDbContext.Posts.Count()} records" +
                $"\nRubrics: {postsDbContext.Rubrics.Count()} records");

            Console.Write("Enter search request: ");
            string? request = Console.ReadLine();
            if (request != null)
            {
                List<Post> results = postsDbContext.Posts.Where(p => p.PostText.Contains(request))
                     .Include(p => p.PostRubrics)
                    .OrderBy(p=>p.PostCreatingDate)
                    .Take(20)
                    .ToList();
                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"{i + 1} -> " + results[i].PostCreatingDate);
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
    }
}