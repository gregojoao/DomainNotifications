# Release Process

Este documento descreve como publicar uma nova versão do pacote NuGet DomainNotifications.

## 📋 Pré-requisitos

1. **NuGet API Key**: Você precisa ter uma API Key do NuGet.org
   - Acesse: https://www.nuget.org/account/apikeys
   - Crie uma nova API Key com permissões de push
   - Adicione como secret no GitHub: `Settings > Secrets and variables > Actions > New repository secret`
   - Nome do secret: `NUGET_API_KEY`

## 🚀 Processo de Release

### Método 1: Via GitHub Release (Recomendado)

1. **Atualize a versão no projeto**
   ```bash
   # Edite src/DomainNotifications/DomainNotifications.csproj
   # Atualize as tags <Version>, <AssemblyVersion> e <FileVersion>
   ```

2. **Commit e push das mudanças**
   ```bash
   git add src/DomainNotifications/DomainNotifications.csproj
   git commit -m "Bump version to X.Y.Z"
   git push origin main
   ```

3. **Crie uma tag**
   ```bash
   git tag -a v3.0.0 -m "Release version 3.0.0"
   git push origin v3.0.0
   ```

4. **Crie uma Release no GitHub**
   - Acesse: https://github.com/grecojoao/DomainNotifications/releases/new
   - Selecione a tag criada (v3.0.0)
   - Título: `v3.0.0`
   - Descrição: Adicione as mudanças da versão (changelog)
   - Clique em "Publish release"

5. **GitHub Actions irá automaticamente:**
   - ✅ Fazer build do projeto
   - ✅ Executar todos os testes
   - ✅ Criar o pacote NuGet
   - ✅ Publicar no NuGet.org

### Método 2: Manual Dispatch

Se você quiser publicar manualmente sem criar uma release:

1. Acesse: https://github.com/grecojoao/DomainNotifications/actions/workflows/publish-nuget.yml
2. Clique em "Run workflow"
3. Selecione a branch
4. (Opcional) Informe a versão
5. Clique em "Run workflow"

## 📝 Versionamento

Seguimos o [Semantic Versioning](https://semver.org/):

- **MAJOR** (X.0.0): Mudanças incompatíveis na API
- **MINOR** (x.Y.0): Novas funcionalidades compatíveis
- **PATCH** (x.y.Z): Correções de bugs compatíveis

### Exemplos:

- `3.0.0` → `3.0.1`: Correção de bug
- `3.0.0` → `3.1.0`: Nova funcionalidade
- `3.0.0` → `4.0.0`: Breaking change

## 🔍 Verificação

Após a publicação, verifique:

1. **GitHub Actions**: Verifique se o workflow foi executado com sucesso
2. **NuGet.org**: Acesse https://www.nuget.org/packages/DomainNotifications/
3. **Tempo de indexação**: Pode levar alguns minutos para aparecer na busca

## 📦 Estrutura do Pacote

O pacote NuGet inclui:

```
DomainNotifications.nupkg
├── lib/
│   └── net10.0/
│       └── DomainNotifications.dll
├── DomainNotifications.nuspec
└── [Content_Types].xml
```

## 🛠️ Configuração do Projeto

As configurações do pacote estão em `src/DomainNotifications/DomainNotifications.csproj`:

```xml
<PropertyGroup>
  <TargetFramework>net10.0</TargetFramework>
  <PackageId>DomainNotifications</PackageId>
  <Version>3.0.0</Version>
  <Authors>João Greco</Authors>
  <Company>Greco Labs</Company>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  <AssemblyVersion>3.0.0.0</AssemblyVersion>
  <FileVersion>3.0.0.0</FileVersion>
  <RepositoryUrl>https://github.com/grecojoao/DomainNotifications</RepositoryUrl>
  <Description>Easily use the Domain Notification design pattern.</Description>
  <PackageRequireLicenseAcceptance>false</PackageRequireLicenseAcceptance>
  <PackageReleaseNotes>Updated to .NET 10 with latest performance improvements and modern C# features.</PackageReleaseNotes>
  <LangVersion>latest</LangVersion>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

## 🔐 Segurança

- ⚠️ **NUNCA** commite a API Key do NuGet no repositório
- ✅ Use sempre GitHub Secrets para armazenar credenciais
- ✅ Revogue e recrie a API Key se houver suspeita de exposição

## 📊 Monitoramento

Após a publicação, monitore:

- Downloads no NuGet.org
- Issues no GitHub
- Feedback da comunidade

## 🆘 Troubleshooting

### Erro: "Package already exists"
- O pacote com essa versão já foi publicado
- Incremente a versão e tente novamente

### Erro: "Invalid API Key"
- Verifique se o secret `NUGET_API_KEY` está configurado corretamente
- Verifique se a API Key não expirou

### Erro: "Tests failed"
- Corrija os testes antes de publicar
- Execute localmente: `dotnet test`

### Workflow não executou
- Verifique se a release foi marcada como "published" (não "draft")
- Verifique os logs em Actions

## 📞 Suporte

Para dúvidas ou problemas:
- Abra uma issue: https://github.com/grecojoao/DomainNotifications/issues
- Contate: João Greco
