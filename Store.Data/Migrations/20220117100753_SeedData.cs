using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.Migrations
{
    public partial class SeedData : Migration
    {
        private const string Schema = "public";
        private const string Uuid = "Guid";
        private const string Text = "string";
        private const string DoublePrecision = "double";
        private const string Integer = "Int32";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Users", 
                GetUsersColumnNames(), 
                GetUsersColumnTypes(), 
                GetUsersValues(), 
                Schema);
            migrationBuilder.InsertData("Categories", 
                GetCategoriesColumnNames(), 
                GetCategoryColumnTypes(),
                GetCategoriesValues(), 
                Schema);
            migrationBuilder.InsertData("Products", 
                GetProductsColumnNames(),
                GetProductsColumnTypes(),
                GetProductsValues(), 
                Schema);
        }

        private static string[] GetUsersColumnNames()
        {
            return new[] { "Id", "FirstName", "LastName", "Login", "Password", "Role" };
        }

        private static string[] GetUsersColumnTypes()
        {
            return new[] { Uuid, Text, Text, Text, Text, Text };
        }

        private static string[] GetCategoriesColumnNames()
        {
            return new[] { "Id", "Name", "Description" };
        }

        private static string[] GetCategoryColumnTypes()
        {
            return new[] { Uuid, Text, Text };
        }

        private static string[] GetProductsColumnNames()
        {
            return new[] { "Id", "Name", "Description", "CategoryId", "Price", "Quantity" };
        }

        private static string[] GetProductsColumnTypes()
        {
            return new[] { Uuid, Text, Text, Uuid, DoublePrecision, Integer };
        }

        private static object[,] GetProductsValues()
        {
            return new object[,]
            {
                {
                    "f3abc9e4-d0c4-4956-a535-2031bd319644",
                    "Gibson Les Paul Custom",
                    "Лучшая электрогитара в мире",
                    "ef5c36f9-6fd4-474e-a938-ee8d1b898162",
                    250000,
                    2
                },
                {
                    "decb65c2-a63e-4b20-b786-a6ef7ced0159",
                    "Esp Ltd EC-1000",
                    "Отличная цельнокорпусная электрогитара для тяжёлых стилей музыки",
                    "ef5c36f9-6fd4-474e-a938-ee8d1b898162",
                    90000,
                    5
                },
                {
                    "84b72c2c-43c5-4028-938e-3d0f325a6a73",
                    "Roland Jupiter 8",
                    "Олдскульный аналоговый синтезатор родом из 80-х",
                    "ec46881f-d7cc-432b-b4bf-6388db7835f3",
                    300000,
                    1
                },
                {
                    "a7e7c327-e52f-4d0c-9357-230b122b61b3",
                    "Abbey Road Modern Drums",
                    "Барабанная установка из студии Abbey Road",
                    "a7e7c327-e52f-4d0c-9357-230b122b61b3",
                    500000,
                    1
                }
            };
        }

        private static object[,] GetCategoriesValues()
        {
            return new object[,]
            {
                {
                    "ef5c36f9-6fd4-474e-a938-ee8d1b898162",
                    "Гитары",
                    "Гитары и другие струнные"

                },
                {
                    "ec46881f-d7cc-432b-b4bf-6388db7835f3",
                    "Синтезаторы",
                    "Цифровые и аналоговые синтезаторы",
                },
                {
                    "a7e7c327-e52f-4d0c-9357-230b122b61b3",
                    "Ударные",
                    "Ударные и перкусионные инструменты"
                }
            };
        }

        private static object[,] GetUsersValues()
        {
            return new object[,]
            {
                {
                    "c7d9cd1e-725a-441f-ac92-03f18858606e",
                    "John",
                    "Doe",
                    "admin",
                    "$2a$11$BgQMTs4uhym7oRfrwNcThe5ZJhsKl7GBghBM.taKCjcnTafB4axEG",
                    "Admin"
                }
            };
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"Users\";");
            migrationBuilder.Sql("DELETE FROM public.\"Products\";");
            migrationBuilder.Sql("DELETE FROM public.\"Categories\";");
        }
    }
}
