namespace DonsDuSangApp.Services
{
    public static class AppService
    {
        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type <paramref name="serviceType"/>. -or- null if there is no service object of type <paramref name="serviceType"/>.</returns>
        public static object? GetService(Type serviceType) => Current?.GetService(serviceType);

        /// <summary>
        /// Get service of type <typeparamref name="TService"/> from the System.IServiceProvider.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="TService"/> or null if there is no such service.</returns>
        public static TService? GetService<TService>() =>
            Current is null ? default : Current.GetService<TService>();

        public static IServiceProvider? Current =>
            IPlatformApplication.Current?.Services;
    }
}
