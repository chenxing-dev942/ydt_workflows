using Org.BouncyCastle.Asn1;
using Scriban;
using ydt_workflows_codegenerator.generators;
using ydt_workflows_codegenerator.Templates;

#region Scriban深入使用-模板变量判断if
//var demoTemplateContent = File.ReadAllText("Templates/DemoTemplateIf.sbn");
//Template template=Template.Parse(demoTemplateContent);
//List<TemplateObject> _templateObjects=new List<TemplateObject>();
//_templateObjects.Add(new TemplateObject
//{
//    user="User1",
//    username="username1",
//    password="password1",
//});
//_templateObjects.Add(new TemplateObject 
//{
//    user = "User2",
//    username = "username2",
//    password = "password2",
//});
//var res = template.Render(new
//{
//    filed1="Id",
//    templateobjects=_templateObjects
//});
//File.WriteAllText("DemoTemplate.cs", res);
#endregion

#region Scriban深入使用-代码换行
//var demoTemplateContent = File.ReadAllText("Templates/DemoTemplateIfn.sbn");
//Template template = Template.Parse(demoTemplateContent);
//List<TemplateObject> _templateObjects = new List<TemplateObject>();
//_templateObjects.Add(new TemplateObject
//{
//    user = "User1",
//    username = "username1",
//    password = "password1",
//});
//_templateObjects.Add(new TemplateObject
//{
//    user = "User2",
//    username = "username2",
//    password = "password2",
//});
//var res = template.Render(new
//{
//    filed1 = "Id",
//    templateobjects = _templateObjects
//});

//string cunrretentDirectory=AppDomain.CurrentDomain.BaseDirectory;   
//DirectoryInfo directoryInfo=new DirectoryInfo(cunrretentDirectory);
//for(int i=0;i<3;i++)
//{
//    directoryInfo = directoryInfo.Parent; 
//}
//string projectDirectory = directoryInfo.FullName;
//// 指定生成文件的目录
//string codeDirectory = Path.Combine(projectDirectory,"code");
//if (!Directory.Exists(codeDirectory))
//{
//    Directory.CreateDirectory(codeDirectory);
//}
//var outputPath = Path.Combine(codeDirectory, "DemoTemplate.cs");
//File.WriteAllText(outputPath, res);

#endregion

#region 7、代码生成器使用
{
    CodeGenerator codeGenerator = new CodeGenerator();
    codeGenerator.Generator();
}
#endregion
