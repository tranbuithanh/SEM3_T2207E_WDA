using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEM3.Example.AutoMapper.Models;
using SEM3.Example.AutoMapper.ViewModels;

namespace SEM3.Example.AutoMapper.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;

        public ProductController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var product = new Product()
            {
                Id ="1",
                ProductName ="ABB10",
                ProductModel=2010,
                MadeIn ="India"
            };
            //ProductViewModel productView = new ProductViewModel();
            //productView.ProductCode = product.Id;
            //productView.ProductName = product.ProductName;
            //productView.ProductModel = product.ProductModel.ToString();
            ProductViewModel productView = mapper.Map<ProductViewModel>(product);
            return View(productView);
        }
    }
}
