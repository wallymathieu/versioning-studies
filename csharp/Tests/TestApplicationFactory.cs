using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests;

class TestApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _configure;

    public TestApplicationFactory(Action<IServiceCollection> configure)
    {
        _configure = configure;
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(_configure);
        return base.CreateHost(builder);
    }
}