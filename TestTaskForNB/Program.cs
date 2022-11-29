using System;
using TestTaskForNB.Data;
using TestTaskForNB.Models;
namespace TestTaskForNB
{
    public class Program
    {
        static public void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            PostsDbContext postsDbContext = new PostsDbContext();
            SeedData.FillDataWithCsv(postsDbContext,"posts.csv");

            Console.WriteLine("Welcome to the posts database!");
            Console.WriteLine($"Database statistic: {postsDbContext.Posts.Count()} records");
            
        }
    }
}