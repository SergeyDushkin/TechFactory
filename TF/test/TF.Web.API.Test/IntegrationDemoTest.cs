using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Web.API.Test.NoodleService;
using System.Data.Services.Client;
using System.Linq;
using TF.Data.Business.WMS;

namespace TF.Web.API.Test
{
    [TestClass]
    public class IntegrationDemoTest
    {
        readonly Container container;

        public IntegrationDemoTest()
        {
            container = new Container(new Uri("http://localhost:5588/odata/"));
        }

        [TestMethod]    
        public void DemoTest()
        {
            CreateBusinessUnit();
            CreateUsers();
            CreateSecurityRoles();
            CreateCategoryTree();
            CreateUoms();
            CreateCurrencies();
            CreateProducts();
            CreateProductCategories();
            CreateRetailPrices();
            CreateOrders();
        }

        void CreateBusinessUnit()
        {
            container.AddToUnits(new Unit { Key = "NOONOODLES", Name = "NOO NOODLES LLC" });
            container.SaveChanges();

            var unit = container.Units.Where(r => r.Key == "NOONOODLES").SingleOrDefault();

            Assert.IsNotNull(unit);

            container.AddToLocations(new Location { Key = "NOO001", Name = "NOO NOODLES STORE 001", Type = "STORE", UnitId = unit.Id });
            container.SaveChanges();

            var location = container.Locations.Where(r => r.Key == "NOO001").SingleOrDefault();

            Assert.IsNotNull(location);
        }

        void CreateUsers()
        {

        }

        void CreateSecurityRoles()
        {

        }

        void CreateCategoryTree()
        {
            container.AddToCategories(new Category { Key = "MENU", Name = "MENU" });
            container.AddToCategories(new Category { Key = "INGREDIENTS", Name = "INGREDIENTS" });
            container.SaveChanges();

            var category_menu = container.Categories.Where(r => r.Key == "MENU").SingleOrDefault();
            var category_ingredient = container.Categories.Where(r => r.Key == "INGREDIENTS").SingleOrDefault();

            Assert.IsNotNull(category_menu);
            Assert.IsNotNull(category_ingredient);

            container.AddToCategories(new Category { Key = "FOOD", Name = "FOOD", ParentId = category_menu.Id });
            container.AddToCategories(new Category { Key = "DRINKS", Name = "DRINKS", ParentId = category_menu.Id });
            container.AddToCategories(new Category { Key = "SNACKS", Name = "SNACKS", ParentId = category_menu.Id });
            container.SaveChanges();

            var category_food = container.Categories.Where(r => r.Key == "FOOD").SingleOrDefault();
            var category_drink = container.Categories.Where(r => r.Key == "DRINKS").SingleOrDefault();
            var category_snack = container.Categories.Where(r => r.Key == "SNACKS").SingleOrDefault();

            Assert.IsNotNull(category_food);
            Assert.IsNotNull(category_drink);
            Assert.IsNotNull(category_snack);

            container.AddToCategories(new Category { Key = "WOK", Name = "WOK", ParentId = category_food.Id });
            container.AddToCategories(new Category { Key = "SOUP POT", Name = "SOUP POT", ParentId = category_food.Id });
            container.AddToCategories(new Category { Key = "PASTA", Name = "PASTA", ParentId = category_food.Id });

            container.AddToCategories(new Category { Key = "HOT", Name = "HOT", ParentId = category_drink.Id });
            container.AddToCategories(new Category { Key = "COLD", Name = "COLD", ParentId = category_drink.Id });
            container.AddToCategories(new Category { Key = "Alcohol", Name = "Alcohol", ParentId = category_drink.Id });

            container.AddToCategories(new Category { Key = "SWEETS", Name = "SWEETS", ParentId = category_snack.Id });
            container.AddToCategories(new Category { Key = "NUTS", Name = "NUTS", ParentId = category_snack.Id });

            container.AddToCategories(new Category { Key = "NOODLE", Name = "NOODLE", ParentId = category_ingredient.Id });
            container.AddToCategories(new Category { Key = "SAUCE", Name = "SAUCE", ParentId = category_ingredient.Id });
            container.AddToCategories(new Category { Key = "PROTEIN", Name = "PROTEIN", ParentId = category_ingredient.Id });
            container.AddToCategories(new Category { Key = "VEGETABLES", Name = "VEGETABLES", ParentId = category_ingredient.Id });
            container.AddToCategories(new Category { Key = "GARNISH", Name = "GARNISH", ParentId = category_ingredient.Id });

            container.SaveChanges();
        }

        void CreateUoms()
        {
            container.AddToUoms(new Uom { Key = "ITEM", Name = "ITEM" });
            container.AddToUoms(new Uom { Key = "G", Name = "GRAM" });
            container.AddToUoms(new Uom { Key = "KG", Name = "KILOGRAM" });
            container.AddToUoms(new Uom { Key = "L", Name = "LITER" });
            container.AddToUoms(new Uom { Key = "ML", Name = "Milliliter" });

            container.SaveChanges();
        }

        void CreateCurrencies()
        {
            container.AddToCurrencies(new Currency { Key = "GBP", Name = "British Pound Sterling" });
            container.AddToCurrencies(new Currency { Key = "USD", Name = "United States Dollar" });
            container.AddToCurrencies(new Currency { Key = "CAD", Name = "Canadian Dollar" });
            container.AddToCurrencies(new Currency { Key = "EUR", Name = "Euro" });
            container.AddToCurrencies(new Currency { Key = "JPY", Name = "JPY" });
            container.AddToCurrencies(new Currency { Key = "CNY", Name = "Chinese Yuan" });

            container.SaveChanges();
        }

        void CreateProducts()
        {
            var uom_kg = container.Uoms.Where(r => r.Key == "KG").SingleOrDefault();

            Assert.IsNotNull(uom_kg);

            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Egg Noodle", Name = "Egg Noodle" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Rice Noodle", Name = "Rice Noodle" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Udon Noodle", Name = "Udon Noodle" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Buckwheat Noodle", Name = "Buckwheat Noodle" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Spinach Noodle", Name = "Spinach Noodle" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Teriyaki Sauce", Name = "Teriyaki Sauce" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Sweet Chili", Name = "Sweet Chili" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Black Bean", Name = "Black Bean" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Japanese Curry", Name = "Japanese Curry" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Oyster Sause", Name = "Oyster Sause" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Sweet and Sour", Name = "Sweet and Sour" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Hot Sauce", Name = "Hot Sauce" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Chicken", Name = "Chicken" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Duck", Name = "Duck" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Pork", Name = "Pork" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Beef", Name = "Beef" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Prawns", Name = "Prawns" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Squid", Name = "Squid" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Mussels", Name = "Mussels" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Cashew Nuts", Name = "Cashew Nuts" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Tofu", Name = "Tofu" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Broccoli", Name = "Broccoli" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Mixed Peppers", Name = "Mixed Peppers" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Mushrooms", Name = "Mushrooms" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Spinach", Name = "Spinach" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Pineapple", Name = "Pineapple" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Bamboo Shots", Name = "Bamboo Shots" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Peas", Name = "Peas" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Pak Choi", Name = "Pak Choi" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Spring Onions", Name = "Spring Onions" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Dried Onions", Name = "Dried Onions" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Fresh Coriander", Name = "Fresh Coriander" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Sesame Seeds", Name = "Sesame Seeds" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Garlic Flakes", Name = "Garlic Flakes" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Chili Oil", Name = "Chili Oil" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Wasabi", Name = "Wasabi" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Chicken Broth", Name = "Chicken Broth" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Miso Broth", Name = "Miso Broth" });
            container.AddToProducts(new Product { Type = "INGREDIENT", Key = "Tonkotsu Broth", Name = "Tonkotsu Broth" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Bolognese REG", Name = "Bolognese regular" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Bolognese HALF", Name = "Bolognese half" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Carbonara REG", Name = "Carbonara regular" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Carbonara HALF", Name = "Carbonara half" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Coca-Cola CAN 0.33", Name = "Coca-Cola CAN 0.33" });
            container.AddToProducts(new Product { Type = "REGULAR", Key = "Coca-Cola BOTL 0.5", Name = "Coca-Cola bottle 0.5" });
            container.AddToProducts(new Product { Type = "KIT", Key = "POT CHI REG", Name = "Pot of soup Chicken regular" });

            container.SaveChanges();
        }

        void CreateProductCategories()
        {
            var productId = Guid.Empty;
            var category_noodle = container.Categories.Where(r => r.Key == "NOODLE").SingleOrDefault();
            var category_sauce = container.Categories.Where(r => r.Key == "SAUCE").SingleOrDefault();
            var category_protein = container.Categories.Where(r => r.Key == "PROTEIN").SingleOrDefault();
            var category_vagetables = container.Categories.Where(r => r.Key == "VEGETABLES").SingleOrDefault();
            var category_garnish = container.Categories.Where(r => r.Key == "GARNISH").SingleOrDefault();

            Assert.IsNotNull(category_noodle);
            Assert.IsNotNull(category_sauce);
            Assert.IsNotNull(category_protein);
            Assert.IsNotNull(category_vagetables);
            Assert.IsNotNull(category_garnish);

            productId = container.Products.Where(r => r.Key == "Egg Noodle").SingleOrDefault().Id;
            container.AddToProductCategories(new ProductCategory
            {
                ProductId = productId,
                CategoryId = category_noodle.Id
            });

            productId = container.Products.Where(r => r.Key == "Rice Noodle").SingleOrDefault().Id;
            container.AddToProductCategories(new ProductCategory
            {
                ProductId = productId,
                CategoryId = category_noodle.Id
            });

            productId = container.Products.Where(r => r.Key == "Udon Noodle").SingleOrDefault().Id;
            container.AddToProductCategories(new ProductCategory
            {
                ProductId = productId,
                CategoryId = category_noodle.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Buckwheat Noodle").SingleOrDefault().Id,
                CategoryId = category_noodle.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Spinach Noodle").SingleOrDefault().Id,
                CategoryId = category_noodle.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Teriyaki Sauce").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Sweet Chili").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Black Bean").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Japanese Curry").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Oyster Sause").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Sweet and Sour").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Hot Sauce").SingleOrDefault().Id,
                CategoryId = category_sauce.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Chicken").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Duck").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Pork").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Beef").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Prawns").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Squid").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Mussels").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Cashew Nuts").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Tofu").SingleOrDefault().Id,
                CategoryId = category_protein.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Broccoli").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Mixed Peppers").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Mushrooms").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Spinach").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Pineapple").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Bamboo Shots").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Peas").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Pak Choi").SingleOrDefault().Id,
                CategoryId = category_vagetables.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Spring Onions").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Dried Onions").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Fresh Coriander").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Sesame Seeds").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Garlic Flakes").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.AddToProductCategories(new ProductCategory
            {
                ProductId = container.Products.Where(r => r.Key == "Chili Oil").SingleOrDefault().Id,
                CategoryId = category_garnish.Id
            });

            container.SaveChanges();

        }

        void CreateRetailPrices()
        {
            container.AddToProductPrices(new ProductPrice
            {
                ProductId = container.Products.Where(r => r.Key == "Coca-Cola CAN 0.33").SingleOrDefault().Id,
                Price = 1
            });

            container.AddToProductPrices(new ProductPrice
            {
                ProductId = container.Products.Where(r => r.Key == "WOK REG").SingleOrDefault().Id,
                Price = 6
            });

            container.AddToProductPrices(new ProductPrice
            {
                ProductId = container.Products.Where(r => r.Key == "WOK HALF").SingleOrDefault().Id,
                Price = 4
            });

            container.AddToProductPrices(new ProductPrice
            {
                ProductId = container.Products.Where(r => r.Key == "POT CHI REG").SingleOrDefault().Id,
                Price = 3
            });

            container.AddToProductPrices(new ProductPrice
            {
                ProductId = container.Products.Where(r => r.Key == "POT CHI HALF").SingleOrDefault().Id,
                Price = 2
            });

            container.SaveChanges();
        }

        void CreateOrders()
        {

        }
    }
}
