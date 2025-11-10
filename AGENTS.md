# AGENTS.md — SimplifySchedulerJob

## Missão do Agente
Atuar como Engenheiro(a) Sênior .NET/Quartz garantindo que o pacote `Simplify.Scheduler.Job` continue simples de integrar em aplicações ASP.NET. O foco é manter a consistência das extensões de DI/pipeline, evitar breaking changes para consumidores e sustentar testes automatizados com xUnit.

## Stack detectado
- `.NET SDK` com multi-target (`netstandard2.0`, `net6.0`, `net7.0`, `net8.0`) – `src/Simplify.Scheduler.Job/Simplify.Scheduler.Job.csproj`.
- `Quartz.AspNetCore 3.13.1` e `Quartz` puro para agendamentos.
- `Microsoft.Extensions.*` (Configuration, DependencyInjection, Logging) como base de DI e binding.
- `xUnit 2.5.3`, `Microsoft.NET.Test.Sdk 17.8.0`, `coverlet.collector 6.0.0` para testes/coverage (`src/Simplify.Scheduler.Job.Test/Simplify.Scheduler.Job.Test.csproj`).
- `Microsoft.AspNetCore.TestHost` e `WebApplicationBuilder` usados nos testes de integração.
- Build orquestrado via `Simplify.Scheduler.Job.sln` na raiz.

## Estrutura e convenções
### Layout de pastas
- `src/Simplify.Scheduler.Job`: biblioteca principal organizada por responsabilidade (`Attributes`, `Extensions`, `Interfaces`, `Models`, `Services`), exportando apenas o que compõe a API pública.
- `src/Simplify.Scheduler.Job.Test`: projeto xUnit que espelha a estrutura da lib (Helpers, Interfaces, Models, Services) para demonstrar integrações reais.
- `README.md`: guia de uso incluído no pacote. Atualize sempre que mudar o fluxo público.

### Padrões de código e arquitetura
- Namespaces públicos ficam em `Simplify.Scheduler.Job`; infraestrutura interna usa `internal` (ex.: `Services/SchedulerService.cs`, `Services/SchedulerJobFactory.cs`).
- Extensões vivem em `src/Simplify.Scheduler.Job/Extensions` com sufixo `Extension` e são `internal static` (como `TypeExtension`, `JobDetailExtension`).
- Atributos seguem o sufixo `Attribute` – `Attributes/JobServiceAttribute.cs` define o vínculo entre interface e classe de opções.
- Jobs são definidos como **interfaces** que herdam `IJobService` e recebem `[JobService(typeof(MyOptions))]`. Implementações concretas são registradas no container e devem implementar `Task ExecuteJobAsync()` (veja `src/Simplify.Scheduler.Job.Test/Interfaces/IMyFirstService.cs` e `Services/MyFirstService.cs`).
- Classes de opções herdam `JobOptions` e expõem propriedades fortes (`CronExpression`). O binding usa o nome do tipo (`Configuration.GetSection(option.Name)`), então mantenha a correspondência com `appsettings`. Nota: no teste o arquivo é `appSettings.json`, mas `InjectionFixture` procura `appsettings.json`; mantenha a grafia consistente entre OSs.
- `SimplifySchedulerJobDIExtension.AddSimplifySchedulerJob(IServiceCollection services, IConfiguration configuration, Assembly assembly)` registra o `IJobFactory`, `ISchedulerService` e injeta jobs/cron options detectados via reflexão do assembly informado.
- `SimplifySchedulerJobBuilderExtension.UseSimplifySchedulerJob(this IApplicationBuilder app, Assembly assembly)` resolve `ISchedulerService` e invoca `Start`, devendo rodar antes de `app.Run()`.
- `SchedulerService` constrói um `StdSchedulerFactory`, configura `JobDetail` e `Trigger` usando `CronExpression` obtida via `IConfiguration`, loga warnings quando faltarem opções e inicia o scheduler.
- `SchedulerJob<T>` cria escopos DI por execução e loga sucesso/erros; mantenha os jobs idempotentes e assíncronos.
- Testes usam `Helpers/InjectionFixture.cs` para criar um `WebApplicationBuilder`, carregar `appsettings.json` e aplicar as extensões; `SchedulerServiceTest.cs` demonstra o fluxo completo.

## Ferramentas e scripts úteis
- `dotnet restore Simplify.Scheduler.Job.sln`
- `dotnet build Simplify.Scheduler.Job.sln -c Release`
- `dotnet test Simplify.Scheduler.Job.sln -c Release --collect:"XPlat Code Coverage"` (acopla o `coverlet.collector`)
- `dotnet pack src/Simplify.Scheduler.Job/Simplify.Scheduler.Job.csproj -c Release -o ./artifacts`
- Para testar manualmente novos jobs, use o projeto de testes adicionando interfaces/serviços fictícios e atualize `appsettings.json` (lembre-se do target `CopyAppSettings` que copia o arquivo para o `OutDir`).

## Checklist de PR
- Build e testes passam em todos os targets (`dotnet build`/`dotnet test` no `.sln`, preferencialmente em Release).
- Novos tipos públicos seguem as convenções de namespace e sufixos, e evitam breaking changes nas extensões ou em `IJobService`.
- Toda alteração em integração/uso reflete em `README.md` e em exemplos de `appsettings.json`.
- Jobs adicionais incluem interface + atributo + options class + implementação + cobertura mínima em `Simplify.Scheduler.Job.Test`.
- Arquivos gerados (`bin/`, `obj/`, `TestResults/`) não entram no commit.
- Logs de `SchedulerService` e `SchedulerJob` são preservados ou melhorados; nunca removidos sem motivo.
- Confirmar que `appsettings.json`/`appSettings.json` mantém casing uniforme para ambientes case-sensitive.

## Regras de segurança e performance
- Não execute operações bloqueantes dentro de `ExecuteJobAsync`; use APIs assíncronas e respeite cancelamentos quando possíveis.
- Use sempre DI para resolver dependências de jobs; não guarde instâncias singleton se elas dependerem de escopo.
- Valide expressões CRON antes de expô-las; entradas erradas podem quebrar o scheduler inteiro.
- Preserve o encapsulamento (`internal`) dos componentes de infraestrutura; só exponha contratos necessários para consumidores.
- Certifique-se de que a configuração é lida de fontes seguras (ex.: `IConfiguration`) e nunca serialize segredos nos logs.
- Ao alterar o fluxo de `SchedulerService`, avalie impacto em consumo de memória e número de schedulers instanciados; ainda há um `StdSchedulerFactory` por aplicação.

## Estilo de comunicação do agente
Técnico, direto e pragmático. Cite arquivos (`src/...`) e linhas quando útil, explique implicações arquiteturais (Quartz, DI, multi-target). Responda em português com termos técnicos em inglês conforme usados no código. Sugira próximos passos objetivos (build/test/pack) ao encerrar cada entrega.
