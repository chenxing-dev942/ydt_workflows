using MySql.Data.MySqlClient;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ydt_workflows_codegenerator.generators
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class CodeGenerator
    {
        /// <summary>
        /// 生成业务代码
        /// </summary>
        public void Generator()
        {
            // 1.1、创建MySQL连接地址
            var connectionString = "Database = ydt_workflows; Data Source = localhost; User Id = root; Password = 136269; SslMode = none; Charset=utf8mb4;";
            // 1.2、获取模型变量集合
            List<Entity> entities = GetAllTables(connectionString);
            // 1.3、生成业务代码【业务模型
            foreach (var entity in entities)
            {
                //1.4、生成Model
                GeneratorEbuisness("generators/Models/ModelTemplate.sbn", $"{entity.name}.cs", entity, $"Models/{entity.name}");
                //1.4.1、生成Model 分页入参
                GeneratorPages("generators/Models/GetListPageTemplate.sbn", $"{entity.name}GetListPage.cs", entity, $"Models/{entity.name}"); // 实现分类
                //1.4.2、生成Model 分页结果
                GeneratorPages("generators/Models/PageTemplate.sbn", $"{entity.name}Page.cs", entity, $"Models/{entity.name}");

                //1.5、生成仓储接口
                GeneratorEbuisness("generators/Repositorise/IRepositoryTemplate.sbn", $"I{entity.name}Repository.cs", entity, "Repositorise/IRepositorys");
                //1.5、生成仓储实现
                GeneratorEbuisness("generators/Repositorise/RepositoryTemplate.sbn", $"{entity.name}Repository.cs", entity, "Repositorise");

                //1.6、生成Service接口
                GeneratorEbuisness("generators/Services/IServiceTemplate.sbn", $"I{entity.name}Service.cs", entity, "Services/IServices");
                //1.6、生成Service实现
                GeneratorEbuisness("generators/Services/ServiceTemplate.sbn", $"{entity.name}Service.cs", entity, "Services");

                //1.7、生成Controllers
                GeneratorEbuisness("generators/Controllers/ControllerTemplate.sbn", $"{entity.name}PageController.cs", entity, "Controllers");
            }
            // 1.8、生成Contexts层代码
            GeneratorContexts("generators/Contexts/IContextTemplate.sbn", "IWorkflowDbContext.cs", entities, "Contexts");
            GeneratorContexts("generators/Contexts/ContextTemplate.sbn", "WorkflowDbContext.cs", entities, "Contexts");

            // 1.9、生成Program层代码
            GeneratorProgram("generators/ProgramTemplate.sbn", "Program.cs", entities);

            // 1.10、生成Dto层代码
            foreach (Entity entity in entities)
            {
                GeneratorDtos("generators/Dtos/CreateDtoTemplate.sbn", $"{entity.name}CreateDto.cs", entity, $"Dtos/{entity.name}"); // 实现分类
                GeneratorDtos("generators/Dtos/DtoTemplate.sbn", $"{entity.name}Dto.cs", entity, $"Dtos/{entity.name}");
                GeneratorDtos("generators/Dtos/GetListDtoTemplate.sbn", $"{entity.name}GetListDto.cs", entity, $"Dtos/{entity.name}");
                GeneratorDtos("generators/Dtos/GetListPageDtoTemplate.sbn", $"{entity.name}GetListPageDto.cs", entity, $"Dtos/{entity.name}");
                GeneratorDtos("generators/Dtos/PageDtoTemplate.sbn", $"{entity.name}PageDto.cs", entity, $"Dtos/{entity.name}");
                GeneratorDtos("generators/Dtos/UpdateDtoTemplate.sbn", $"{entity.name}UpdateDto.cs", entity, $"Dtos/{entity.name}");
            }

            // 1.11、生成MappingProfile代码
            GeneratorMappingProfile("generators/Dtos/MappingProfileTemplate.sbn", "MappingProfile.cs", entities, "Dtos");
        }

        #region 生成Dto层代码
        /// <summary>
        /// 13、生成Dto层代码
        /// </summary>
        /// <param name="TemplatePath"></param>
        /// <param name="ModelFileName"></param>
        /// <param name="entity"></param>

        private static void GeneratorDtos(string TemplatePath, string ModelFileName, Entity entity, string ModelLogicName)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent = File.ReadAllText(TemplatePath);
            // 2、模板内容解析
            Template template = Template.Parse(DemoTemplateContent);
            // 3、模板渲染
            var result = template.Render(new
            {
                name = entity.name,  // 模型名
                comment = entity.comment,// 表注释
                tablename = entity.tablename,// 表名
                properties = entity.properties, // 字段集合
            });

            //4、生成对应的模型文件
            // 4.1、创建指定的code目录
            // 确保code目录存在
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 获取项目的根目录
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            for (int i = 0; i < 3; i++) // 根据实际目录层级调整循环次数
            {
                directoryInfo = directoryInfo.Parent;
            }

            string projectDirectory = directoryInfo.FullName;

            // 指定生成文件的目录【根据模型名称实现分类】
            string codeDirectory = Path.Combine(projectDirectory, "code", ModelLogicName);
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }

            // 生成文件的完整路径$"{entity.name}.cs"
            var outputPath = Path.Combine(codeDirectory, ModelFileName);

            File.WriteAllText(outputPath, result);
        }
        #endregion

        #region 生成mappingPorifile
        private static void GeneratorMappingProfile(string TemplatePath, string ModelFileName,List<Entity> entities, string ModelLogicName)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent = File.ReadAllText(TemplatePath);
            // 2、模板内容解析
            Template template = Template.Parse(DemoTemplateContent);
            // 3、模板渲染
            var result = template.Render(new
            {
                entities = entities,  // 模型集合
            });

            //4、生成对应的模型文件
            // 4.1、创建指定的code目录
            // 确保code目录存在
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 获取项目的根目录
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            for (int i = 0; i < 3; i++) // 根据实际目录层级调整循环次数
            {
                directoryInfo = directoryInfo.Parent;
            }

            string projectDirectory = directoryInfo.FullName;

            // 指定生成文件的目录
            string codeDirectory = Path.Combine(projectDirectory, "code", ModelLogicName);
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }

            // 生成文件的完整路径$"{entity.name}.cs"
            var outputPath = Path.Combine(codeDirectory, ModelFileName);

            File.WriteAllText(outputPath, result);
        }

        #endregion

        #region 生成Page模型代码
        /// <summary>
        /// 14、生成Page模型代码
        /// </summary>
        /// <param name="TemplatePath"></param>
        /// <param name="ModelFileName"></param>
        /// <param name="entity"></param>

        private static void GeneratorPages(string TemplatePath, string ModelFileName, Entity entity, string ModelLogicName)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent = File.ReadAllText(TemplatePath);
            // 2、模板内容解析
            Template template = Template.Parse(DemoTemplateContent);
            // 3、模板渲染
            var result = template.Render(new
            {
                name = entity.name,  // 模型名
                comment = entity.comment,// 表注释
                tablename = entity.tablename,// 表名
                properties = entity.properties, // 字段集合
            });

            //4、生成对应的模型文件
            // 4.1、创建指定的code目录
            // 确保code目录存在
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 获取项目的根目录
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            for (int i = 0; i < 3; i++) // 根据实际目录层级调整循环次数
            {
                directoryInfo = directoryInfo.Parent;
            }

            string projectDirectory = directoryInfo.FullName;

            // 指定生成文件的目录【根据模型名称实现分类】
            string codeDirectory = Path.Combine(projectDirectory, "code", ModelLogicName);
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }

            // 生成文件的完整路径$"{entity.name}.cs"
            var outputPath = Path.Combine(codeDirectory, ModelFileName);

            File.WriteAllText(outputPath, result);
        }
        #endregion

        #region 生成Program代码
        /// <summary>
        /// 11、生成Contexts代码
        /// </summary>
        /// <param name="TemplatePath"></param>
        /// <param name="ModelFileName"></param>
        /// <param name="entity"></param>

        private static void GeneratorProgram(string TemplatePath, string ModelFileName, List<Entity> entities)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent = File.ReadAllText(TemplatePath);
            // 2、模板内容解析
            Template template = Template.Parse(DemoTemplateContent);
            // 3、模板渲染
            var result = template.Render(new
            {
                entities = entities,  // 模型集合
            });

            //4、生成对应的模型文件
            // 4.1、创建指定的code目录
            // 确保code目录存在
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 获取项目的根目录
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            for (int i = 0; i < 3; i++) // 根据实际目录层级调整循环次数
            {
                directoryInfo = directoryInfo.Parent;
            }

            string projectDirectory = directoryInfo.FullName;

            // 指定生成文件的目录
            string codeDirectory = Path.Combine(projectDirectory, "code");
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }

            // 生成文件的完整路径$"{entity.name}.cs"
            var outputPath = Path.Combine(codeDirectory, ModelFileName);

            File.WriteAllText(outputPath, result);
        }
        #endregion

        # region 生成Contexts代码
        /// <summary>
        /// 11、生成Contexts代码
        /// </summary>
        /// <param name="TemplatePath"></param>
        /// <param name="ModelFileName"></param>
        /// <param name="entity"></param>

        private static void GeneratorContexts(string TemplatePath, string ModelFileName, List<Entity> entities, string ModelLogicName)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent = File.ReadAllText(TemplatePath);
            // 2、模板内容解析
            Template template = Template.Parse(DemoTemplateContent);
            // 3、模板渲染
            var result = template.Render(new
            {
                entities = entities,  // 模型集合
            });

            //4、生成对应的模型文件
            // 4.1、创建指定的code目录
            // 确保code目录存在
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 获取项目的根目录
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            for (int i = 0; i < 3; i++) // 根据实际目录层级调整循环次数
            {
                directoryInfo = directoryInfo.Parent;
            }

            string projectDirectory = directoryInfo.FullName;

            // 指定生成文件的目录
            string codeDirectory = Path.Combine(projectDirectory, "code", ModelLogicName);
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }

            // 生成文件的完整路径$"{entity.name}.cs"
            var outputPath = Path.Combine(codeDirectory, ModelFileName);

            File.WriteAllText(outputPath, result);
        }
        #endregion

        #region 在指定文件生成业务代码
        /// <summary>
        /// 在指定文件生成业务代码
        /// </summary>
        /// <param name="TemplatePath"></param>
        /// <param name="ModelFileName"></param>
        /// <param name="entity"></param>
        private static void GeneratorEbuisness(string TemplatePath, string ModelFileName, Entity entity, string ModelLogicName)
        {
            // 1、加载模板文件"generators/ModelTemplate.sbn"
            var DemoTemplateContent=File.ReadAllText(TemplatePath);
            Template template=Template.Parse(DemoTemplateContent);
            var res = template.Render(new
            {
                name = entity.name,
                comment=entity.comment,
                tablename=entity.tablename,
                properties=entity.properties
            });
            // 确保code目录存在
            string currentDirecotry=AppDomain.CurrentDomain.BaseDirectory; 
            DirectoryInfo directoryInfo=new DirectoryInfo(currentDirecotry);
            for (int i=0;i<3;i++)
            {
                directoryInfo = directoryInfo.Parent;
            }
            string projectDiectory = directoryInfo.FullName;
            //指定生成文件的目录【根据模型名称实现分类】
            string codeDirectory = Path.Combine(projectDiectory,"code", ModelLogicName);
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);   
            }
            var outputPath=Path.Combine(codeDirectory, ModelFileName);
            File.WriteAllText(outputPath, res);
        }
        #endregion

        #region 1.获取ydt_workflows所有表
        /// <summary>
        /// 1、获取ydt_workflows所有表 [MySql.Data]
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static List<Entity> GetAllTables(string connectionString)
        {
            List<Entity> entities = new List<Entity>();
            using (var connection=new MySqlConnection(connectionString))
            {
                connection.Open();
                var tableCommand = new MySqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'ydt_workflows'",
                            connection);
                using (var reader =tableCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tableName=reader.GetString(0);
                        Entity entity=new Entity();
                        entity.tablename = tableName;
                        entity.comment = GetTableComments(connectionString, tableName);
                        entity.name = TableNameToModelName(tableName);
                        entity.properties=GetTableFileds(connectionString, tableName);
                        entities.Add(entity);   
                    }
                }
            }
            return entities;
        }
        #endregion

        #region 2.获取所有表字段
        /// <summary>
        /// 2、获取所有表字段
        /// </summary>
        private static List<Property> GetTableFileds(string connectionString, string tableName)
        {
            List<Property> properties = new List<Property>();
            using (var connection=new MySqlConnection(connectionString))
            {
                connection.Open();
                var columnCommand = new MySqlCommand($"SELECT COLUMN_NAME, DATA_TYPE, COLUMN_KEY, COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND TABLE_SCHEMA = 'ydt_workflows' ORDER BY ORDINAL_POSITION",
                                    connection);
                columnCommand.Parameters.AddWithValue("@TableName", tableName);
                using (var columnReader = columnCommand.ExecuteReader())
                {
                    while (columnReader.Read())
                    {
                        string name=columnReader.GetString(0);
                        string type=columnReader.GetString(1);
                        string key=columnReader.GetString(2);
                        string comment=columnReader.IsDBNull(3) ? string.Empty : columnReader.GetString(3);
                        Property property = new Property();
                        property.name = name;
                        property.type = MySQLTypeToCSharpType(type);          
                        property.key = key;
                        property.comment = comment;
                        properties.Add(property);   
                    }
                }
            }
            return properties;
        }
        #endregion

        #region 3.获取所有表注释

        /// <summary>
        /// 3、获取所有表注释
        /// </summary>
        private static string GetTableComments(string connectionString, string tableName)
        {
            using (var connection=new MySqlConnection(connectionString))
            {
                connection.Open();
                //创建表注释脚本
                var command = new MySqlCommand(@"
                    SELECT 
                        table_comment 
                    FROM 
                        information_schema.tables 
                    WHERE 
                        table_schema = 'ydt_workflows' 
                        AND table_name = @TableName", connection);
                command.Parameters.AddWithValue("@TableName", tableName);

                return command.ExecuteScalar()?.ToString();
            }
        }
        #endregion

        #region 4.字段类型转换
        /// <summary>
        /// 4、字段类型转换
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static string MySQLTypeToCSharpType(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "int":
                case "integer":
                case "smallint":
                case "mediumint":
                case "int2":
                case "int4":
                    return "int";
                case "bigint":
                case "int8":
                    return "long";
                case "float":
                case "real":
                    return "float";
                case "double":
                case "double precision":
                    return "double";
                case "decimal":
                case "numeric":
                    return "decimal";
                case "bit":
                case "bool":
                case "boolean":
                    return "bool";
                case "char":
                case "varchar":
                case "text":
                case "tinytext":
                case "mediumtext":
                case "longtext":
                    return "string";
                case "date":
                case "datetime":
                case "timestamp":
                    return "DateTime";
                case "time":
                    return "TimeSpan";
                case "blob":
                case "binary":
                case "varbinary":
                case "tinyblob":
                case "mediumblob":
                case "longblob":
                    return "byte[]";
                default:
                    return "object"; // 默认类型
            }
        }
        #endregion

        #region 5.表名转换成模型名
        //8、表名转换【表名转换成模型名】例如：ydt_user--->User
        public static string TableNameToModelName(string tableName)
        {
            // 去掉前缀 "ydt_"
            if (tableName.StartsWith("ydt_"))
            {
                tableName = tableName.Substring(4);
            }

            // 将下划线分隔的表名转换为驼峰命名法
            var parts = tableName.Split('_');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            }

            return string.Join(string.Empty, parts);
        }
        #endregion
    }
}
