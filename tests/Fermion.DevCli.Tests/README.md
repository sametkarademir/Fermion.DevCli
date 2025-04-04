# Fermion DevCLI Tests

Bu klasör, Fermion DevCLI projesi için birim testleri içerir.

## Test Yapısı

- **Commands**: Komut sınıfları için testler
- **Services**: Servis sınıfları için testler

## Test Çalıştırma

Testleri çalıştırmak için aşağıdaki komutu kullanabilirsiniz:

```bash
dotnet test
```

Belirli bir test sınıfı veya metodu çalıştırmak için:

```bash
dotnet test --filter "FullyQualifiedName=Fermion.DevCli.Tests.Services.PasswordGeneratorServiceTests"
```

## Test Yaklaşımı

- **Birim Testler**: Her bir bileşen izole edilmiş bir ortamda test edilir.
- **Taklit Nesneler (Mocks)**: Dış bağımlılıklar Moq kütüphanesi kullanılarak taklit edilir.
- **İfadeli Test (Fluent Assertions)**: Daha okunabilir test doğrulamaları için FluentAssertions kullanılır.