using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private readonly IConfiguration configuration;
        private int PageSize;

        public ProductController(IProductRepository repo, IConfiguration config)
        {
            repository = repo;
            configuration = config;
            if (!int.TryParse(config["Data:PageSize"], out PageSize))
                throw new System.Exception("No config value for PageSize found!");
        }

        public ViewResult List(string category, int page = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductID)
                   .Skip((page - 1) * PageSize)
                   .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}