using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace TF.DAL
{
    public class NoodleDbContext
    {
        private string nameOrConnectionString;
        private string providerName;
        private string connectionString;

        public NoodleDbContext(string nameOrConnectionString)
        {
            this.nameOrConnectionString = nameOrConnectionString;
            connectionString = ConfigurationManager.ConnectionStrings[nameOrConnectionString].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings[nameOrConnectionString].ProviderName;

            // ONLY FOR TEST
            Init();
        }

        public IDbConnection CreateConnection()
        {
            var connection = DbProviderFactories.GetFactory(providerName).CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[nameOrConnectionString].ConnectionString;

            if (String.IsNullOrEmpty(connectionString))
                throw new Exception("Connectionstring is missing");

            CreateDBIfNotExists(connectionString);
            CreateSchema(connectionString);
        }

        private void CreateDBIfNotExists(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.InitialCatalog = "master";

            using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("select * from master.dbo.sysdatabases where name='{0}'", databaseName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // exists
                            return;
                    }

                    command.CommandText = string.Format("CREATE DATABASE {0}", databaseName);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateSchema(string connectionString)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var resources = assembly
                .GetManifestResourceNames()
                .Where(r => r.EndsWith(".sql")).ToList();

            foreach (var resourceName in resources.Where(r => r.IndexOf("EmptyDbSchema.sql") > -1))
            {
                ExecuteResourceSql(resourceName);
            }

            foreach (var resourceName in resources.Where(r => r.IndexOf("Trunk.Tables") > -1))
            {
                ExecuteResourceSql(resourceName);
            }
        }

        private void ExecuteResourceSql(string resourceName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = reader.ReadToEnd();
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }
    /*
    class NoodleDbContext : DbContext
    {
        public NoodleDbContext() : base("NoodleDb") { }
        public NoodleDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("[BUSINESS.WMS.PRODUCT_N_SERVICE]");
            modelBuilder.Entity<Product>().HasKey(r => r.Id);
            modelBuilder.Entity<Product>().Property(r => r.Id)
                .HasColumnName("GUID_RECORD")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().Property(r => r.Name).HasColumnName("NAME").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Product>().Property(r => r.Key).HasColumnName("KEY").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(r => r.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .HasMany(r => r.ChildProducts)
                .WithRequired(r => r.Parent);

        }
    }*/
}
