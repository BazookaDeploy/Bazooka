namespace Bazooka.Core
{
    using Microsoft.SqlServer.Dac;
    using System;
    using System.IO;

    public static class DacpacHelpers
    {
        public static void ApplyDacpac(Stream dacpac, string connectionString, string databaseName, ILogger log, bool partialDeploy = false)
        {
            var options = new DacDeployOptions()
            {
                BlockOnPossibleDataLoss = true,
                IncludeTransactionalScripts = true,
                DropDmlTriggersNotInSource = false,
                DropPermissionsNotInSource = false,
                DropStatisticsNotInSource = false,
                DropRoleMembersNotInSource = false,

                // cambiati rispetto al progetto originale di paonic
                DropConstraintsNotInSource = partialDeploy ? false: true, // vengono inseriti solo in prod ?
                DropExtendedPropertiesNotInSource = partialDeploy ? false : true,
                DropIndexesNotInSource = partialDeploy ? false : true, // ci sono indici creati da altri e non presenti nei source?
                DropObjectsNotInSource = partialDeploy ? false : true, // li schema e le viste della BI sono sempre nei source?

                // aggiunti rispetto al progetto originale di paonic
                CreateNewDatabase = false,
                DoNotDropObjectTypes = new ObjectType[]
                {
                    ObjectType.Aggregates,
                    ObjectType.Assemblies,
                    ObjectType.ApplicationRoles,
                    ObjectType.Audits,
                    ObjectType.BrokerPriorities,
                    ObjectType.Certificates,
                    ObjectType.ClrUserDefinedTypes,
                    ObjectType.Contracts,
                    ObjectType.Credentials,
                    ObjectType.CryptographicProviders,
                    ObjectType.DatabaseRoles,
                    ObjectType.DatabaseAuditSpecifications,
                    ObjectType.Defaults,
                    ObjectType.Endpoints,
                    ObjectType.ErrorMessages,
                    ObjectType.EventNotifications,
                    ObjectType.EventSessions,
                    ObjectType.FullTextCatalogs,
                    ObjectType.FullTextStoplists,
                    ObjectType.LinkedServers,
                    ObjectType.LinkedServerLogins,
                    ObjectType.Logins,
                    ObjectType.MessageTypes,
                    ObjectType.Permissions,
                    ObjectType.RoleMembership,
                    ObjectType.ServerRoleMembership,
                    ObjectType.ServerRoles,
                    ObjectType.Queues,
                    ObjectType.RemoteServiceBindings,
                    ObjectType.Routes,
                    ObjectType.Rules,
                    ObjectType.SearchPropertyLists,
                    ObjectType.Sequences,
                    ObjectType.ServerTriggers,
                    ObjectType.Services,
                    ObjectType.Signatures,
                    ObjectType.Users,
                },
                ExcludeObjectTypes = new ObjectType[]
                {
                    ObjectType.Aggregates,
                    ObjectType.Assemblies,
                    ObjectType.ApplicationRoles,
                    ObjectType.Audits,
                    ObjectType.BrokerPriorities,
                    ObjectType.Certificates,
                    ObjectType.ClrUserDefinedTypes,
                    ObjectType.Contracts,
                    ObjectType.Credentials,
                    ObjectType.CryptographicProviders,
                    ObjectType.DatabaseRoles,
                    ObjectType.DatabaseAuditSpecifications,
                    ObjectType.Defaults,
                    ObjectType.Endpoints,
                    ObjectType.ErrorMessages,
                    ObjectType.EventNotifications,
                    ObjectType.EventSessions,
                    ObjectType.FullTextCatalogs,
                    ObjectType.FullTextStoplists,
                    ObjectType.LinkedServers,
                    ObjectType.LinkedServerLogins,
                    ObjectType.Logins,
                    ObjectType.MessageTypes,
                    ObjectType.Permissions,
                    ObjectType.RoleMembership,
                    ObjectType.ServerRoleMembership,
                    ObjectType.ServerRoles,
                    ObjectType.Queues,
                    ObjectType.RemoteServiceBindings,
                    ObjectType.Routes,
                    ObjectType.Rules,
                    ObjectType.SearchPropertyLists,
                    ObjectType.Sequences,
                    ObjectType.ServerTriggers,
                    ObjectType.Services,
                    ObjectType.Signatures,
                    ObjectType.Users,
                },
                AllowDropBlockingAssemblies = false,
                AllowIncompatiblePlatform = true,
                BackupDatabaseBeforeChanges = false,
                BlockWhenDriftDetected = false,
                CommentOutSetVarDeclarations = false,
                CompareUsingTargetCollation = false,
                DeployDatabaseInSingleUserMode = false,
                DisableAndReenableDdlTriggers = false,
                DoNotAlterChangeDataCaptureObjects = true,
                DoNotAlterReplicatedObjects = true,
                GenerateSmartDefaults = true,
                IgnoreAnsiNulls = true,
                IgnoreAuthorizer = true,
                IgnoreColumnCollation = false,
                IgnoreColumnOrder = true,
                IgnoreComments = false,
                IgnoreCryptographicProviderFilePath = true,
                IgnoreDdlTriggerOrder = true,
                IgnoreDdlTriggerState = true,
                IgnoreDefaultSchema = true,
                IgnoreDmlTriggerOrder = true,
                IgnoreDmlTriggerState = true,
                IgnoreExtendedProperties = false,
                IgnoreFileAndLogFilePath = true,
                IgnoreFilegroupPlacement = true,
                IgnoreFileSize = true,
                IgnoreFillFactor = true,
                IgnoreFullTextCatalogFilePath = true,
                IgnoreIdentitySeed = true,
                IgnoreIncrement = true,
                IgnoreIndexOptions = false,
                IgnoreIndexPadding = false,
                IgnoreKeywordCasing = true,
                IgnoreLoginSids = true,
                IgnoreLockHintsOnIndexes = false,
                IgnoreNotForReplication = true,
                IgnoreObjectPlacementOnPartitionScheme = true,
                IgnorePartitionSchemes = false,
                IgnoreQuotedIdentifiers = true,
                IgnorePermissions = true,
                IgnoreRoleMembership = true,
                IgnoreRouteLifetime = true,
                IgnoreSemicolonBetweenStatements = true,
                IgnoreTableOptions = false,
                IgnoreUserSettingsObjects = true,
                IgnoreWhitespace = true,
                IgnoreWithNocheckOnCheckConstraints = false,
                IgnoreWithNocheckOnForeignKeys = false,
                IncludeCompositeObjects = false,
                NoAlterStatementsToChangeClrTypes = false,
                PopulateFilesOnFileGroups = false,
                RegisterDataTierApplication = false,
                RunDeploymentPlanExecutors = false,
                ScriptDatabaseCollation = false,
                ScriptDatabaseCompatibility = false,
                ScriptDatabaseOptions = false,
                ScriptDeployStateChecks = false,
                ScriptFileSize = false,
                ScriptNewConstraintValidation = true,
                ScriptRefreshModule = false,
                UnmodifiableObjectWarnings = true,
                VerifyCollationCompatibility = true,
                TreatVerificationErrorsAsWarnings = false,
                VerifyDeployment = true,

            };

            log.Log("Creating connection");
            var service = new DacServices(connectionString);
            service.Message += (x, y) =>
            {
                log.Log(y.Message.Message);
            };
            try
            {
                log.Log("Starting dacpac application");
                using (var package = DacPackage.Load(dacpac))
                {
                    log.Log(service.GenerateDeployScript(package, databaseName, options));
                    service.Deploy(package, databaseName, true, options);
                }
            }
            catch (Exception e)
            {
                log.Log(e.Message, true);
                if (e.InnerException != null)
                {
                    log.Log(e.InnerException.Message, true);
                }
            }
        }
    }
}
