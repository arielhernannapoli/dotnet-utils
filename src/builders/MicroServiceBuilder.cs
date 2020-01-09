using dotnet_utils.src.contracts;
using dotnet_utils.src.enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace dotnet_utils.src.builders
{
    public class MicroServiceBuilder : IBuilder
    {
        private readonly IParser _parser;

        public MicroServiceBuilder(IParser parser)
        {
            this._parser = parser;
        }

        public bool Build(string folderName)
        {
            var di = BuildSolution(folderName);
            BuildProjectApi(di, folderName);
            BuildProjectDomain(di, folderName);
            BuildProjectInfrastructure(di, folderName);
            BuildTestProject(di, folderName);
            return true;
        }

        private DirectoryInfo BuildSolution(string folderName) {
            var directoryInfo = Directory.CreateDirectory(folderName);
            var dicSolution = new Dictionary<string,string>();
            dicSolution.Add(SolutionTypes.name, folderName);
            dicSolution.Add(SolutionTypes.guidAPI, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidAPIc, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidDomain, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidDomainc, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidInfrastructure, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidInfrastructurec, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidUnitTesting, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidUnitTestingc, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidIntegrationTesting, Guid.NewGuid().ToString());
            dicSolution.Add(SolutionTypes.guidIntegrationTestingc, Guid.NewGuid().ToString());                        
            dicSolution.Add(SolutionTypes.testc, Guid.NewGuid().ToString());                                    
            var content = _parser.Parse(enums.skeletontypes.solution, dicSolution);
            File.WriteAllText(directoryInfo.FullName + $@"\{folderName}.sln", content);
            return directoryInfo;
        }

        private void BuildProjectApi(DirectoryInfo directoryInfo, string folderName) {
            var subDirectoryInfo = directoryInfo.CreateSubdirectory($"{folderName}.API");
            var dicSolution = new Dictionary<string,string>();
            dicSolution.Add(SolutionTypes.name, folderName);            
            var content = _parser.Parse(enums.skeletontypes.projectAPI, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\{folderName}.API.csproj", content);
            var applicationDirectory = subDirectoryInfo.CreateSubdirectory("Application");
            var controllersDirectory = subDirectoryInfo.CreateSubdirectory("Controllers");
            var infrastructureDirectory = subDirectoryInfo.CreateSubdirectory("Infrastructure");
            var modelsDirectory = subDirectoryInfo.CreateSubdirectory("Models");
            var setupDirectory = subDirectoryInfo.CreateSubdirectory("Setup");
            content = _parser.Parse(enums.skeletontypes.appsettings, null);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\appsettings.json", content);            
            File.WriteAllText(subDirectoryInfo.FullName + $@"\appsettings.Development.json", content);
            content = _parser.Parse(enums.skeletontypes.programAPI, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\Program.cs", content);                                    
            content = _parser.Parse(enums.skeletontypes.startupAPI, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\Startup.cs", content);                                                
            content = _parser.Parse(enums.skeletontypes.homeController, dicSolution);
            File.WriteAllText(controllersDirectory.FullName + $@"\HomeController.cs", content);                                                            
            content = _parser.Parse(enums.skeletontypes.crudController, dicSolution);
            File.WriteAllText(controllersDirectory.FullName + $@"\{folderName}Controller.cs", content);                                                                        
        }

        private void BuildProjectDomain(DirectoryInfo directoryInfo, string folderName) {
            var subDirectoryInfo = directoryInfo.CreateSubdirectory($"{folderName}.Domain");
            var dicSolution = new Dictionary<string,string>();
            dicSolution.Add(SolutionTypes.name, folderName);                        
            var content = _parser.Parse(enums.skeletontypes.projectDomain, null);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\{folderName}.Domain.csproj", content);
            var aggregatesDirectory = subDirectoryInfo.CreateSubdirectory("Aggregates");
            var itemDirectory = aggregatesDirectory.CreateSubdirectory($"{folderName}");
            var commonDirectory = subDirectoryInfo.CreateSubdirectory("Common");                        
            var exceptionsDirectory = subDirectoryInfo.CreateSubdirectory("Exceptions");                 
            content = _parser.Parse(enums.skeletontypes.unitofwork, dicSolution);
            File.WriteAllText(commonDirectory.FullName + $@"\IUnitOfWork.cs", content);                               
            content = _parser.Parse(enums.skeletontypes.entity, dicSolution);
            File.WriteAllText(commonDirectory.FullName + $@"\Entity.cs", content);                                           
            content = _parser.Parse(enums.skeletontypes.iAggregateRoot, dicSolution);
            File.WriteAllText(commonDirectory.FullName + $@"\IAggregateRoot.cs", content);                                                       
            content = _parser.Parse(enums.skeletontypes.iRepository, dicSolution);
            File.WriteAllText(commonDirectory.FullName + $@"\IRepository.cs", content);                                                       
            content = _parser.Parse(enums.skeletontypes.aggregate, dicSolution);
            File.WriteAllText(itemDirectory.FullName + $@"\{folderName}.cs", content);                                                       
            content = _parser.Parse(enums.skeletontypes.iAggregateRepository, dicSolution);
            File.WriteAllText(itemDirectory.FullName + $@"\I{folderName}Repository.cs", content);                                                                   
        } 

        private void BuildProjectInfrastructure(DirectoryInfo directoryInfo, string folderName) {
            var subDirectoryInfo = directoryInfo.CreateSubdirectory($"{folderName}.Infrastructure");
            var dicSolution = new Dictionary<string,string>();
            dicSolution.Add(SolutionTypes.name, folderName);            
            var content = _parser.Parse(enums.skeletontypes.projectInfrastructure, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\{folderName}.Infrastructure.csproj", content);
            var entityConfigurationsDirectory = subDirectoryInfo.CreateSubdirectory("EntityConfigurations");
            var repositoriesDirectory = subDirectoryInfo.CreateSubdirectory("Repositories");                        
            content = _parser.Parse(enums.skeletontypes.aggregateRepository, dicSolution);
            File.WriteAllText(repositoriesDirectory.FullName + $@"\{folderName}Repository.cs", content);                        
            content = _parser.Parse(enums.skeletontypes.dbcontext, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\{folderName}Context.cs", content);            
            content = _parser.Parse(enums.skeletontypes.mediatorExtension, dicSolution);
            File.WriteAllText(subDirectoryInfo.FullName + $@"\MediatorExtension.cs", content);                        
            content = _parser.Parse(enums.skeletontypes.entityConfiguration, dicSolution);
            File.WriteAllText(entityConfigurationsDirectory.FullName + $@"\{folderName}EntityTypeConfiguration.cs", content);                                    
        }

        private void BuildTestProject(DirectoryInfo directoryInfo, string folderName) {
            var subDirectoryInfo = directoryInfo.CreateSubdirectory($"test");
            var subDirectoryUnitInfo = subDirectoryInfo.CreateSubdirectory($"{folderName}.UnitTests");
            var subDirectoryIntegrationInfo = subDirectoryInfo.CreateSubdirectory($"{folderName}.IntegrationTests");
            var dicSolution = new Dictionary<string,string>();
            dicSolution.Add(SolutionTypes.name, folderName);            
            var content = _parser.Parse(enums.skeletontypes.projectUnitTesting, dicSolution);
            File.WriteAllText(subDirectoryUnitInfo.FullName + $@"\{folderName}.UnitTests.csproj", content);                        
            content = _parser.Parse(enums.skeletontypes.projectIntegrationTesting, dicSolution);
            File.WriteAllText(subDirectoryIntegrationInfo.FullName + $@"\{folderName}.IntegrationTests.csproj", content);                                    
        }

    }

}