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
                //try
                //{
                List<CsvPosts> posts = new List<CsvPosts>();
                //Reading csv file
                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    posts = csv.GetRecords<CsvPosts>().ToList();
                }
                //Adding posts to the database
                if (posts.Any()) {
                    foreach (CsvPosts post in posts)
                    {
                        //Getting PostText and Creating date
                        //and creating object with them
                        Post postToAdd = new Post()
                        {
                            PostText = posts[0].text,
                            PostCreatingDate = posts[0].created_date,
                            PostRubrics = new List<Rubric>()
                        };

                        List<Rubric> rubrics = new List<Rubric>();
                        string[] rubricNames = post.rubrics.Split("\',");
                        //Getting rubrics names
                        for (int i = 0; i < rubricNames.Length; i++){
                           //Deleting extra spaces and special symbols
                            rubricNames[i] = rubricNames[i].Replace("[", string.Empty)
                                .Replace("]", string.Empty)
                                .Replace("\'", string.Empty)
                                .Trim();

                            Rubric? databaseRb = context.Rubrics.Where(r => r.RubricName == rubricNames[i]).FirstOrDefault();
                            if (databaseRb == null)
                            {
                                databaseRb = new Rubric()
                                {
                                    RubricName = rubricNames[i]
                                };
                                context.Rubrics.Add(databaseRb);
                            }
                            
                            rubrics.Add(databaseRb);
                        }
                        postToAdd.PostRubrics = rubrics;
                        context.Posts.Add(postToAdd);
                        context.SaveChanges();

                    }
                   
                }
                    
                   
                        //foreach (CsvPosts post in posts)
                        //{
                        //    Post newPost = new Post()
                        //    {
                        //        //PostText = post.text,
                        //        //PostCreatingDate = post.created_date
                        //    };

                        //    //Getting rubrics and adding it to the post
                        //    //------------------------- 
                        //    List<Rubric> rubrics = new List<Rubric>();
                        //    string[] rubricNames = post.rubrics.Split("\',");
                        //    for (int i = 0; i < rubricNames.Length; i++)
                        //    {
                        //        //Deleting extra spaces and special symbols
                        //        rubricNames[i] = rubricNames[i].Replace("[", string.Empty).Replace("]", string.Empty).Replace("\'", string.Empty).Trim();
                        //        //Trying to find if rubric is already exist in the database
                        //        Rubric? databaseRubric = context.Rubrics.Where(r=>r.RubricName==rubricNames[i]).FirstOrDefault();
                        //        if (databaseRubric==null)
                        //        {

                        //            //Creating a new rubric and adding it to the database
                        //            databaseRubric = new Rubric()
                        //            {
                        //                RubricName = rubricNames[i]
                        //            };
                        //            //context.Rubrics.Add(databaseRubric);
                        //            //context.SaveChanges();
                        //        }
                        //        else Console.WriteLine("RUBRIC EXIST!");
                        //    //Adding rubric to the post

                        //    //newPost.PostRubrics.Add(databaseRubric);
                        //        context.Posts.Add(newPost);
                        //    context.SaveChanges();
                        //}

                        //-------------------------




                        //}
                    
                //}
                //catch (Exception err)
                //{
                //    Console.WriteLine($"Failed to load seed data!\n{err.Message}");
                //}
                ////***************
                
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
