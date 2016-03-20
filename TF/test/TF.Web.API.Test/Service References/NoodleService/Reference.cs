//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Исходное имя файла:
// Дата создания: 20.03.2016 21:06:36
namespace TF.Web.API.Test.NoodleService
{
    
    /// <summary>
    /// В схеме отсутствуют комментарии для Container.
    /// </summary>
    public partial class Container : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// Инициализируйте новый объект Container.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Container(global::System.Uri serviceRoot) : 
                base(serviceRoot, global::System.Data.Services.Common.DataServiceProtocolVersion.V3)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
        }
        partial void OnContextCreated();
        /// <summary>
        /// Поскольку пространство имен, настроенное для этой ссылки на службу
        /// в Visual Studio, отличается от пространства имен, указанного
        /// в схеме сервера, для сопоставления этих пространств имен используйте преобразователи типов.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "TF.Data.Business.WMS", "TF.Web.API.Test.NoodleService");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            resolvedType = this.DefaultResolveType(typeName, "TF.Data.Business", "TF.Web.API.Test.NoodleService.TF.Data.Business");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            return null;
        }
        /// <summary>
        /// Поскольку пространство имен, настроенное для этой ссылки на службу
        /// в Visual Studio, отличается от пространства имен, указанного
        /// в схеме сервера, для сопоставления этих пространств имен используйте преобразователи типов.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("TF.Web.API.Test.NoodleService.TF.Data.Business", global::System.StringComparison.Ordinal))
            {
                return string.Concat("TF.Data.Business.", clientType.Name);
            }
            if (clientType.Namespace.Equals("TF.Web.API.Test.NoodleService", global::System.StringComparison.Ordinal))
            {
                return string.Concat("TF.Data.Business.WMS.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для Products.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<Product> Products
        {
            get
            {
                if ((this._Products == null))
                {
                    this._Products = base.CreateQuery<Product>("Products");
                }
                return this._Products;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<Product> _Products;
        /// <summary>
        /// В схеме отсутствуют комментарии для Categories.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<TF.Data.Business.Category> Categories
        {
            get
            {
                if ((this._Categories == null))
                {
                    this._Categories = base.CreateQuery<TF.Data.Business.Category>("Categories");
                }
                return this._Categories;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<TF.Data.Business.Category> _Categories;
        /// <summary>
        /// В схеме отсутствуют комментарии для Products.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для Categories.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToCategories(TF.Data.Business.Category category)
        {
            base.AddObject("Categories", category);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = "<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edm" +
                "x\"><edmx:DataServices m:DataServiceVersion=\"3.0\" m:MaxDataServiceVersion=\"3.0\" x" +
                "mlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema " +
                "Namespace=\"TF.Data.Business.WMS\" xmlns=\"http://schemas.microsoft.com/ado/2009/11" +
                "/edm\"><EntityType Name=\"Product\"><Key><PropertyRef Name=\"Id\" /></Key><Property N" +
                "ame=\"Id\" Type=\"Edm.Guid\" Nullable=\"false\" /><Property Name=\"Type\" Type=\"Edm.Stri" +
                "ng\" /><Property Name=\"Key\" Type=\"Edm.String\" /><Property Name=\"Name\" Type=\"Edm.S" +
                "tring\" /><NavigationProperty Name=\"Parent\" Relationship=\"TF.Data.Business.WMS.TF" +
                "_Data_Business_WMS_Product_Parent_TF_Data_Business_WMS_Product_ParentPartner\" To" +
                "Role=\"Parent\" FromRole=\"ParentPartner\" /><NavigationProperty Name=\"ChildProducts" +
                "\" Relationship=\"TF.Data.Business.WMS.TF_Data_Business_WMS_Product_ChildProducts_" +
                "TF_Data_Business_WMS_Product_ChildProductsPartner\" ToRole=\"ChildProducts\" FromRo" +
                "le=\"ChildProductsPartner\" /></EntityType><Association Name=\"TF_Data_Business_WMS" +
                "_Product_Parent_TF_Data_Business_WMS_Product_ParentPartner\"><End Type=\"TF.Data.B" +
                "usiness.WMS.Product\" Role=\"Parent\" Multiplicity=\"0..1\" /><End Type=\"TF.Data.Busi" +
                "ness.WMS.Product\" Role=\"ParentPartner\" Multiplicity=\"0..1\" /></Association><Asso" +
                "ciation Name=\"TF_Data_Business_WMS_Product_ChildProducts_TF_Data_Business_WMS_Pr" +
                "oduct_ChildProductsPartner\"><End Type=\"TF.Data.Business.WMS.Product\" Role=\"Child" +
                "Products\" Multiplicity=\"*\" /><End Type=\"TF.Data.Business.WMS.Product\" Role=\"Chil" +
                "dProductsPartner\" Multiplicity=\"0..1\" /></Association></Schema><Schema Namespace" +
                "=\"TF.Data.Business\" xmlns=\"http://schemas.microsoft.com/ado/2009/11/edm\"><Entity" +
                "Type Name=\"Category\"><Key><PropertyRef Name=\"Id\" /></Key><Property Name=\"Id\" Typ" +
                "e=\"Edm.Guid\" Nullable=\"false\" /><Property Name=\"Key\" Type=\"Edm.String\" /><Proper" +
                "ty Name=\"Name\" Type=\"Edm.String\" /><Property Name=\"ParentId\" Type=\"Edm.Guid\" /><" +
                "/EntityType></Schema><Schema Namespace=\"Default\" xmlns=\"http://schemas.microsoft" +
                ".com/ado/2009/11/edm\"><EntityContainer Name=\"Container\" m:IsDefaultEntityContain" +
                "er=\"true\"><EntitySet Name=\"Products\" EntityType=\"TF.Data.Business.WMS.Product\" /" +
                "><EntitySet Name=\"Categories\" EntityType=\"TF.Data.Business.Category\" /><Associat" +
                "ionSet Name=\"TF_Data_Business_WMS_Product_Parent_TF_Data_Business_WMS_Product_Pa" +
                "rentPartnerSet\" Association=\"TF.Data.Business.WMS.TF_Data_Business_WMS_Product_P" +
                "arent_TF_Data_Business_WMS_Product_ParentPartner\"><End Role=\"ParentPartner\" Enti" +
                "tySet=\"Products\" /><End Role=\"Parent\" EntitySet=\"Products\" /></AssociationSet><A" +
                "ssociationSet Name=\"TF_Data_Business_WMS_Product_ChildProducts_TF_Data_Business_" +
                "WMS_Product_ChildProductsPartnerSet\" Association=\"TF.Data.Business.WMS.TF_Data_B" +
                "usiness_WMS_Product_ChildProducts_TF_Data_Business_WMS_Product_ChildProductsPart" +
                "ner\"><End Role=\"ChildProductsPartner\" EntitySet=\"Products\" /><End Role=\"ChildPro" +
                "ducts\" EntitySet=\"Products\" /></AssociationSet></EntityContainer></Schema></edmx" +
                ":DataServices></edmx:Edmx>";
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static string GetConcatenatedEdmxString()
            {
                return string.Concat(ModelPart0);
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            public static global::Microsoft.Data.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel LoadModelFromString()
            {
                string edmxToParse = GetConcatenatedEdmxString();
                global::System.Xml.XmlReader reader = CreateXmlReader(edmxToParse);
                try
                {
                    return global::Microsoft.Data.Edm.Csdl.EdmxReader.Parse(reader);
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }
        }
    }
    /// <summary>
    /// В схеме отсутствуют комментарии для TF.Data.Business.WMS.Product.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Products")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class Product : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Создайте новый объект Product.
        /// </summary>
        /// <param name="ID">Начальное значение Id.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Product CreateProduct(global::System.Guid ID)
        {
            Product product = new Product();
            product.Id = ID;
            return product;
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Id.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Type.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this.OnTypeChanging(value);
                this._Type = value;
                this.OnTypeChanged();
                this.OnPropertyChanged("Type");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Type;
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Key.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this.OnKeyChanging(value);
                this._Key = value;
                this.OnKeyChanged();
                this.OnPropertyChanged("Key");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Key;
        partial void OnKeyChanging(string value);
        partial void OnKeyChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для Parent.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Product Parent
        {
            get
            {
                return this._Parent;
            }
            set
            {
                this._Parent = value;
                this.OnPropertyChanged("Parent");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private Product _Parent;
        /// <summary>
        /// В схеме отсутствуют комментарии для ChildProducts.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Product> ChildProducts
        {
            get
            {
                return this._ChildProducts;
            }
            set
            {
                this._ChildProducts = value;
                this.OnPropertyChanged("ChildProducts");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<Product> _ChildProducts = new global::System.Data.Services.Client.DataServiceCollection<Product>(null, global::System.Data.Services.Client.TrackingMode.None);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
// Исходное имя файла:
// Дата создания: 20.03.2016 21:06:36
namespace TF.Data.Business
{
    
    /// <summary>
    /// В схеме отсутствуют комментарии для TF.Data.Business.Category.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Categories")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class Category : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Создайте новый объект Category.
        /// </summary>
        /// <param name="ID">Начальное значение Id.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Category CreateCategory(global::System.Guid ID)
        {
            Category category = new Category();
            category.Id = ID;
            return category;
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Id.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Key.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this.OnKeyChanging(value);
                this._Key = value;
                this.OnKeyChanged();
                this.OnPropertyChanged("Key");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Key;
        partial void OnKeyChanging(string value);
        partial void OnKeyChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства ParentId.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.Guid> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this.OnParentIdChanging(value);
                this._ParentId = value;
                this.OnParentIdChanged();
                this.OnPropertyChanged("ParentId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.Guid> _ParentId;
        partial void OnParentIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnParentIdChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
