using Mvc_Repository.Models.DbContextFactory;
using Mvc_Repository.Service;
using Mvc_Repository.Service.Interface;
using MVCWeb.Models;
using MVCWeb.Models.Interface;
using MVCWeb.Models.Repository;
using System;
using System.Data.Entity.Infrastructure;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace MVCWeb
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            //註冊型別的時候讓 Unity 知道要傳什麼參數值給指定類別的建構式

            //另外在 Unity 設定時再去調整 DbContextFactory 的 LifetimeManager，
            //以確保 DbContextFactory instance 沒有重複，讓 DbContextFactory
            //不會建立重複的 DbContance instance
            string connectionString = WebConfigurationManager.ConnectionStrings["MovieDBContext"].ConnectionString;
            container.RegisterType<IDbContextFactory, DbContextFactory>(
                new HierarchicalLifetimeManager(),
                new InjectionConstructor(connectionString));

            //Repository
            container.RegisterType<IRepository<Movie>, GenericRepository<Movie>>(new InjectionConstructor());

            //Service
            container.RegisterType<IMoviesService, MoviesService>();
        }
    }
}