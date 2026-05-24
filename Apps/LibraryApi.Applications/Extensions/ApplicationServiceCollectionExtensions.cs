using LibraryApi.Applications.Adapters;
using LibraryApi.Applications.Dtos;
using LibraryApi.Applications.Services;
using LibraryApi.Applications.UseCases;
using LibraryApi.Domains.Adapters;
using LibraryAPi.Domains.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Applications.Extensions; 
/// <summary>
/// アプリケーション層の構成要素を DI コンテナへ登録する拡張メソッドを提供する
///
/// アプリケーション層の登録に関する知識を本クラスに閉じ込めることで、
/// プレゼンテーション層は AddApplication を一度呼ぶだけでよい。
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// アプリケーション層の構成要素(Service など)を登録する
    /// </summary>
    /// <param name="services">DI コンテナ</param>
    /// <returns>登録後のDIコンテナ(メソッドチェーン用)</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Service(リポジトリに依存するため Scoped)
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();

        // UseCase(Service・Adapterに依存するためScoped)
        services.AddScoped<IFindCategoriesUseCase, FindCategoriesInteractor>();
        services.AddScoped<ISearchBooksUseCase, SearchBooksInteractor>();
        services.AddScoped<IFindBookUseCase, FindBookInteractor>();
        services.AddScoped<IRegisterBookUseCase, RegisterBookInteractor>();
        services.AddScoped<IUpdateBookUseCase, UpdateBookInteractor>();

        // DTO Adapter(状態を持たない変換ロジックのため Singleton)
        services.AddSingleton<IAdapter<Category, CategoryDto>, CategoryDtoAdapter>();
        services.AddSingleton<IAdapter<Book, BookDto>, BookDtoAdapter>();

        return services;
    }
}