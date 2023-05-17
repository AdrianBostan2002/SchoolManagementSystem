using Autofac;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using PlatformaEducationalaPentruScoala.Services;
using PlatformaEducationalaPentruScoala.ViewModels;
using PlatformaEducationalaPentruScoala.Views;

namespace PlatformaEducationalaPentruScoala.Settings
{
    public class ContainerConfig
    {
        public static ContainerBuilder Builder = new ContainerBuilder();

        public static IContainer Configure()
        {
            Builder.RegisterType<AppDbContext>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance()
                .WithParameter("connectionString", "Server=localhost;Database=SchoolDb;User Id=Adi123;Password=123;");

            RegisterRepositories();

            RegisterServices();

            Builder.RegisterType<UnitOfWork>().AsSelf().SingleInstance();

            RegisterViewModels();

            RegisterViews();

            Builder.RegisterType<ViewFactory>().AsSelf().InstancePerLifetimeScope();

            Builder.RegisterType<MainWindow>().AsSelf();

            return Builder.Build();
        }

        public static void RegisterRepositories()
        {
            Builder.RegisterType<UsersRepository>().AsSelf();
            Builder.RegisterType<TeachersRepository>().AsSelf();
            Builder.RegisterType<UsersDetailsRepository>().AsSelf();
            Builder.RegisterType<ClassesRepository>().AsSelf();
            Builder.RegisterType<SubjectsRepository>().AsSelf();
            Builder.RegisterType<StudentsRepository>().AsSelf();
            Builder.RegisterType<SpecializationRepository>().AsSelf();
        }

        public static void RegisterServices()
        {
            Builder.RegisterType<UserService>().AsSelf().InstancePerLifetimeScope();
            Builder.RegisterType<TeacherService>().AsSelf().InstancePerLifetimeScope();
            Builder.RegisterType<ClassService>().AsSelf().InstancePerLifetimeScope();
            Builder.RegisterType<SubjectService>().AsSelf().InstancePerLifetimeScope();
            Builder.RegisterType<StudentService>().AsSelf().InstancePerLifetimeScope();
        }

        public static void RegisterViews()
        {
            Builder.RegisterType<LoginView>().AsSelf();
            Builder.RegisterType<RegisterView>().AsSelf();
            Builder.RegisterType<AdministratorView>().AsSelf();
            Builder.RegisterType<TeachersListView>().AsSelf();
            Builder.RegisterType<UserListView>().AsSelf();
            Builder.RegisterType<StudentListView>().AsSelf();
            Builder.RegisterType<SubjectListView>().AsSelf();
        }

        public static void RegisterViewModels()
        {
            Builder.RegisterType<LoginVM>().AsSelf();
            Builder.RegisterType<RegisterVM>().AsSelf();
            Builder.RegisterType<AdministratorVM>().AsSelf();
            Builder.RegisterType<TeachersListVM>().AsSelf();
            Builder.RegisterType<UserListVM>().AsSelf();
            Builder.RegisterType<StudentListVm>().AsSelf(); 
            Builder.RegisterType<SubjectListVM>().AsSelf();
        }
    }
}