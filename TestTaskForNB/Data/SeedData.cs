using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using TestTaskForNB.Models;

namespace TestTaskForNB.Data
{
    
    public class SeedData
    {
        public static void FillDataWithCsv(PostsDbContext context, string filename)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Posts.Any())
            {
                //Reading csv file to get example data
                //***************
                try
                {
                    List<CsvPosts> posts = new List<CsvPosts>();
                    using (var reader = new StreamReader(filename))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        posts = csv.GetRecords<CsvPosts>().ToList();
                    }

                    if (posts.Any())
                    {
                        foreach(CsvPosts post in posts)
                        {
                            context.Posts.Add(new Post() { 
                                PostText=post.text,
                                PostCreatingDate=post.created_date,
                                PostRubrics=post.rubrics});
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine($"Failed to load seed data!\n{err.Message}");
                }
                //***************
                context.SaveChanges();
            }
        }

        //Class for CSV reader
        private class CsvPosts
        {
            public string text { get; set; } = string.Empty;
            public DateTime created_date { get; set; }
            public string rubrics { get; set; } = string.Empty;
        }
    }
}
