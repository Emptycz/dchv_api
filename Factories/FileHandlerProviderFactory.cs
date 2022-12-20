using System.Runtime.InteropServices;
using dchv_api.FileHandlers;
using System.Collections.Immutable;

namespace dchv_api.Factories;


public class FileHandlerProviderFactory
{
  private readonly IReadOnlyDictionary<string, IFileHandlerProvider> _fileHandlerProviders;

  public FileHandlerProviderFactory()
  {
      Type fileHandlerProviderType = typeof(IFileHandlerProvider);
      _fileHandlerProviders = fileHandlerProviderType.Assembly.ExportedTypes
      .Where(x => fileHandlerProviderType.IsAssignableFrom(x) &&
        !x.IsInterface && !x.IsAbstract)
      .Select(x =>
        {
          var parametlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
          return parametlessCtor is not null
            ? Activator.CreateInstance(x)
            : throw new ArgumentException("Missing arguments");
        })
        .Cast<IFileHandlerProvider>()
        .ToImmutableDictionary(x => x.FileExtension, x => x);
  }

  public IFileHandlerProvider GetProviderByFileExtension(string extension)
  {
      var provider = _fileHandlerProviders.GetValueOrDefault(extension);
      return provider ?? throw new NotImplementedException("Provider for this extension is not implemented");
  }

}
