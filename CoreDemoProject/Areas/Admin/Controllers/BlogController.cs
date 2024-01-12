﻿using ClosedXML.Excel;
using CoreDemoProject.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	public class BlogController : Controller
	{
		public IActionResult ExportStaticExcelBlogList()
		{
			using (var workbook=new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("BlogListesi");
				worksheet.Cell(1,1).Value= "Blog ID";
				worksheet.Cell(1,2).Value= "Blog Adı";

				int BlogRowCount = 2;
				foreach (var item in GetBlogList())
				{
					worksheet.Cell(BlogRowCount, 1).Value = item.ID;
					worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
					BlogRowCount++;
				}
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();
					return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
				}
			}
			
		}
		public List<BlogModel> GetBlogList()
		{
			List<BlogModel> blogModel = new List<BlogModel>()
			{
				new BlogModel{ID=1,BlogName="C# Programlamaya Giriş"},
				new BlogModel{ID=2,BlogName="Tesla Firmasının Araçları"},
				new BlogModel{ID=3,BlogName="2020 Olşmpiyatları"}

			};
			return blogModel;
		}
		public IActionResult BlogListExcel()
		{
			return View();
		}
	
		public IActionResult ExportDynamicExcelBlogList()
		{
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("BlogListesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
		public List<BlogModel2> BlogTitleList()
		{
			List<BlogModel2> bm = new List<BlogModel2>();
			using(var c = new Context())
			{
				bm = c.Blogs.Select(x => new BlogModel2
				{
					ID= x.BlogID,
					BlogName = x.BlogTitle
				}).ToList();
			}
			return bm;
		}
		public IActionResult BlogTitleListExcel()
		{
			return View();
		}
	
	}
}