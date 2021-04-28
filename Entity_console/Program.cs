using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Entity_console
{
    class Program
    {
		public class BloggingContext : DbContext
		{
			public DbSet<Blog> Blogs { get; set; }
			public DbSet<Post> Posts { get; set; }
			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder.UseOracle(@"USER ID=SYSTEM; Password=new0878952737;DATA SOURCE=127.0.0.1:1521/XE;PERSIST SECURITY INFO=True");
			}

			/*Schemas เริ่มต้นคือ DBO ค้นหาเพิ่มเติม url: stackoverflow.com/questions/35403610/table-does-not-exist-while-using-ef-6-and-oracle-manageddataaccess */
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.HasDefaultSchema("SYSTEM");
			}
		}
		/* [Table("BLOG")] อ้างอิง Table โดยใช้ตัวแปร  Blog*/
		[Table("BLOG")]
		public class Blog
		{
			[Column("BLOGID")]
			public int BlogId { get; set; }

			[Column("URL")]
			public string Url { get; set; }
			public List<Post> Posts { get; set; }
		}
		public class Post
		{
			public int PostId { get; set; }
			public string Title { get; set; }
			public string Content { get; set; }

			public int BlogId { get; set; }
			public Blog Blog { get; set; }
		}
		static void Main(string[] args)
		{
			/*INSERT Data*/
/*			using (var db = new BloggingContext())
            {
                var blog = new Blog { BlogId = 91, Url = "https://blogs.oracle.com" };
                db.Blogs.Add(blog);
                db.SaveChanges();
            }*/

			/*ดึงข้อมูล*/
			using (var db = new BloggingContext())
			{
				var blogs = db.Blogs.ToList<Blog>();

                foreach (var item in blogs)
                {
					System.Console.WriteLine(item.BlogId);
				}
			}
		}
	}
}
