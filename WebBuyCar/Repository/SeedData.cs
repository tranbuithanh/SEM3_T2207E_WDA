using System;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Models;

namespace WebBuyCar.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();

            if (!_context.Products.Any())
            {
                BannerModel banner = new BannerModel { ImageBanner = "banner-supercar.jpg", TitleBanner = "Luxury Cars", desBanner = "EXPLORE 7,000+ LUXURY CARS, SUPERCARS AND EXOTIC CARS FOR SALE WORLDWIDE IN ONE SIMPLE SEARCH", Status = 1 };
                CategoryModel ferrariModel = new CategoryModel { NameCategory = "488 GTB", Description = "488 GTB is sport car", ImageCategory = "list-item-bugatti-chiron", Slug = "488_gtp", Status = 1 };
                CategoryModel mercedesModel = new CategoryModel { NameCategory = "AMG GT", Description = "AMG GT is sport car", ImageCategory = "list-item-bugatti-chiron", Slug = "amg_gt", Status = 1 };
                BrandModel ferrariBrand = new BrandModel { NameBrands = "Ferrari", Description = "Ferrari is brand top in the word", ImageBrand = "ferrari-logo", Slug = "ferrari", Status = 1 };
                BrandModel mercedesBrand = new BrandModel { NameBrands = "Mercedes", Description = "Mercedes is brand top in the word", ImageBrand = "mercedes-logo", Slug = "mercedes", Status = 1 };

                _context.Products.AddRange(
                    new ProductModel { NameCar = "Ferrari 488 GTB", Slug = "ferrari_488_gtb", Description = " Ferrari 488 GTB is supercar", YearOfManufacture = 2017, Price = 460000, Category = ferrariModel, Brand = ferrariBrand, status = 1, ImageProduct = "buy-ferrari-458.jpeg" },
                    new ProductModel { NameCar = "Mercedes AMG GT", Slug = "mercedes_amg_gt", Description = " Mercedes AMG GT is supercar", YearOfManufacture = 2018, Price = 360000, Category = mercedesModel, Brand = mercedesBrand, status = 1, ImageProduct = "buy-mercedes-amg-gt.jpeg" }
                );
                _context.Banners.Add(banner);
                _context.SaveChanges();

            }
        }
    }
}
