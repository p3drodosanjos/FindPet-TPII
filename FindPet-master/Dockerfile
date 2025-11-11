#Estágio 1: Build da Aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

#--- CORREÇÃO FINAL E MAIS ROBUSTA ---
#Copia TUDO primeiro.
#Isso garante que o processo de restore tenha 100% de visibilidade de todos os
#arquivos e configurações do projeto, evitando pacotes faltando.
COPY . .

#Restaura as dependências.
#Agora ele tem certeza de tudo que precisa para baixar os pacotes corretos.
RUN dotnet restore

#Publica o projeto.
#Removemos '--no-restore', pois queremos que ele use a restauração que acabamos de fazer.
#Também removemos '-v n', pois não precisamos mais do log detalhado.
RUN dotnet publish *.csproj -c Release -o /app/publish

#Estágio 2: Imagem Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FindPet.dll"]