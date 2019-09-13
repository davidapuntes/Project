using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presupuestea.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


                if (!context.Customers.Any())
                {
                    var david = new Customer()
                    {
                        CustomerType = "Customer",
                        Usuario = new ApplicationUser()
                        {
                            UserName = "David",
                            Email = "davidapuntes@hotmail.com",
                            EmailConfirmed = true,
                            PostalCode = "13600",

                        }

                    };

                    var alberto = new Customer()
                    {
                        CustomerType = "Customer",
                        Usuario = new ApplicationUser()
                        {
                            UserName = "Alberto",
                            Email = "albertoapuntes@hotmail.com",
                            EmailConfirmed = true,
                            PostalCode = "13700",

                        }

                    };

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

                if (!context.Contractors.Any())
                {
                    int abogadoCategoryId = context.Categories.Where(c => c.Name == "Abogados").Select(c => c.CategoryID).FirstOrDefault();
                    int gestorCategoryId = context.Categories.Where(c => c.Name == "Gestores").Select(c => c.CategoryID).FirstOrDefault();

                    var abogado1 = new Contractor()
                    {
                        ContractorType = "Contractor",
                        Usuario = new ApplicationUser()
                        {

                            UserName = "Ramón",
                            Email = "ramonProveedor@hotmail.com",
                            EmailConfirmed = true,
                            PostalCode = "13600",

                        },

                        CategoryId = abogadoCategoryId,
                        FirstName = "Sanuelo"

                    };

                    var gestor1 = new Contractor()
                    {
                        ContractorType = "Contractor",
                        Usuario = new ApplicationUser()
                        {

                            UserName = "Francisco",
                            Email = "franProveedor@hotmail.com",
                            EmailConfirmed = true,
                            PostalCode = "13700",

                        },

                        CategoryId = gestorCategoryId,
                        FirstName = "Poveda"

                    };                 

                    context.Contractors.Add(abogado1);
                    context.Contractors.Add(gestor1);
                    context.SaveChanges();

                }


                if (!context.Conversations.Any())
                {
                    string abogadoId = context.Contractors.Where(c => c.Usuario.UserName == "Ramón").Select(c => c.ContractorID).FirstOrDefault();
                    string gestorId = context.Contractors.Where(c => c.Usuario.UserName == "Francisco").Select(c => c.ContractorID).FirstOrDefault();

                    string davidId = context.Users.Where(c => c.UserName == "David").Select(c => c.Id).FirstOrDefault();
                    string albertoId = context.Users.Where(c => c.UserName == "Alberto").Select(c => c.Id).FirstOrDefault();


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

   
    
 
    

