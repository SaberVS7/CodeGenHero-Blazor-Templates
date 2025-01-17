﻿using CodeGenHero.Core;
using CodeGenHero.Template.Blazor6.Generators;
using CodeGenHero.Template.Models;
using System;
using System.Collections.Generic;

namespace CodeGenHero.Template.Blazor6.Templates
{
    [Template(name: "RepositoryCrudInterface", version: "2021.9.14", uniqueTemplateIdGuid: "5A7FE640-915D-4B08-BA8A-A82C33D69D09",
        description: "Generates a Create/Read/Update/Delete Interface for a Repository Class to implement.")]
    public sealed class RepositoryCrudInterfaceTemplate : BaseBlazorTemplate
    {
        public RepositoryCrudInterfaceTemplate()
        {
        }

        #region TemplateVariables

        [TemplateVariable(defaultValue: Consts.PTG_EntitiesNamespace_DEFAULT, description: Consts.PTG_EntitiesNamespace_DESC)]
        public string EntitiesNamespace { get; set; }

        [TemplateVariable(defaultValue: Consts.PTG_RepositoryCrudInterfaceName_DEFAULT, description: Consts.PTG_RepositoryCrudInterfaceName_DESC)]
        public string RepositoryCrudInterfaceClassName { get; set; }

        [TemplateVariable(defaultValue: Consts.RepositoryCrudInterfaceOutputFilepath_DEFAULT, hiddenIndicator: true)]
        public string RepositoryCrudInterfaceOutputFilepath { get; set; }

        [TemplateVariable(defaultValue: Consts.PTG_RepositoryNamespace_DEFAULT, description: Consts.PTG_RepositoryNamespace_DESC)]
        public string RepositoryNamespace { get; set; }

        #endregion TemplateVariables

        public override TemplateOutput Generate()
        {
            TemplateOutput retVal = new TemplateOutput();

            try
            {
                string outputFile = TemplateVariablesManager.GetOutputFile(templateIdentity: ProcessModel.TemplateIdentity,
                    fileName: Consts.OUT_RepositoryCrudInterfaceOutputFilepath_DEFAULT);
                string filepath = outputFile;

                var usings = new List<NamespaceItem>
                {
                    new NamespaceItem(EntitiesNamespace),
                    new NamespaceItem($"Enums = {BaseNamespace}.Shared.Constants.Enums"),
                    new NamespaceItem("System.Linq"),
                    new NamespaceItem($"{BaseNamespace}.Repository.Infrastructure")
                };

                var entities = ProcessModel.MetadataSourceModel.GetEntityTypesByRegEx(RegexExclude, RegexInclude);

                var generator = new RepositoryCrudInterfaceGenerator(inflector: Inflector);
                string generatedCode = generator.Generate(usings, RepositoryNamespace, NamespacePostfix, entities, RepositoryCrudInterfaceClassName);

                retVal.Files.Add(new OutputFile()
                {
                    Content = generatedCode,
                    Name = filepath
                });
            }
            catch (Exception ex)
            {
                base.AddError(ref retVal, ex, Enums.LogLevel.Error);
            }

            return retVal;
        }
    }
}