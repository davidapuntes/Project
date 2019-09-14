using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presupuestea.Data.Model;
using System.Linq;

namespace Presupuestea.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                if (!context.Customers.Any() && context.Users.Any())
                {
                    var david = new Customer()
                    {
                        CustomerType = "Customer",
                        Usuario = context.Users.Where(u => u.Email == "davidapuntes@hotmail.com").FirstOrDefault() as ApplicationUser
                        

                    };

                    var alberto = new Customer()
                    {
                        CustomerType = "Customer",
                        Usuario = context.Users.Where(u => u.UserName == "albertoapuntes@hotmail.com").FirstOrDefault() as ApplicationUser

                    };


                    context.Customers.Add(david);
                    context.Customers.Add(alberto);
                    context.SaveChanges();
                }



                if (!context.Categories.Any())
                {
                    var categoriaAbogados = new Category { Name = "Abogados" };
                    var categoriaGestores = new Category { Name = "Gestores" };

                    context.Categories.Add(categoriaAbogados);
                    context.Categories.Add(categoriaGestores);

                    context.SaveChanges();

                }

                if (!context.Contractors.Any() && context.Users.Any())
                {
                    int abogadoCategoryId = context.Categories.Where(c => c.Name == "Abogados").Select(c => c.CategoryID).FirstOrDefault();
                    int gestorCategoryId = context.Categories.Where(c => c.Name == "Gestores").Select(c => c.CategoryID).FirstOrDefault();

                    var abogado1 = new Contractor()
                    {

                        ContractorType = "Contractor",
                        Usuario = context.Users.Where(u => u.Email == "ramonsacristan@proveedor1.com").FirstOrDefault() as ApplicationUser,
                        CategoryId = abogadoCategoryId,
                        FirstName = "Sanuelo"

                    };

                    var gestor1 = new Contractor()
                    {
                        ContractorType = "Contractor",
                        Usuario = context.Users.Where(u => u.Email == "franciscop@proveedor2.com").FirstOrDefault() as ApplicationUser,
                        CategoryId = gestorCategoryId,
                        FirstName = "Poveda"

                    };

                    context.Contractors.Add(abogado1);
                    context.Contractors.Add(gestor1);
                    context.SaveChanges();

                }


                if (!context.Conversations.Any())
                {
                    string abogadoId = context.Contractors.Where(c => c.Usuario.Email == "ramonsacristan@proveedor1.com").Select(c => c.ContractorID).FirstOrDefault();
                    string gestorId = context.Contractors.Where(c => c.Usuario.Email == "franciscop@proveedor2.com").Select(c => c.ContractorID).FirstOrDefault();

                    string davidId = context.Customers.Where(c => c.Usuario.Email == "davidapuntes@hotmail.com").Select(c => c.CustomerId).FirstOrDefault();
                    string albertoId = context.Customers.Where(c => c.Usuario.Email == "alberto@apuntes.hotmail.com").Select(c => c.CustomerId).FirstOrDefault();


                    var conversacion1 = new Conversation { CustomerId = davidId, ContractorId = abogadoId, Text = "Hola desde David" };
                    var conversacion2 = new Conversation { CustomerId = albertoId, ContractorId = gestorId, Text = "Hola desde Alberto" };

                    context.Conversations.Add(conversacion1);
                    context.Conversations.Add(conversacion2);


                    context.SaveChanges();


                }


            }
        }


    }
}






