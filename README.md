
### 在 AppHost 项目上使用 AgileConfig

```
dotnet add package AgileConfig.Aspire.Hosting --version 1.0.0
```
首先安装 AgileConfig.Aspire.Hosting。 这个包是 AgileConfig 服务端的一个扩展。使用它配合 Aspire 可以直接启动 AgileConfig 容器并且简单配置它。

安装完后，我们在 Program 下添加如下代码：

```
using Aspire.Hosting.AgileConfig;

var builder = DistributedApplication.CreateBuilder(args);

var agileConfig = builder.AddAgileConfig(); // 添加 AgileConfig 服务端，这会启动一个 Container

var agileConfig_apiservice = agileConfig.AddApp("apiservice"); // 在 AgileConfig 添加一个应用 apiservice，客户端会从这里读取业务
var agileConfig_webfrontend = agileConfig.AddApp("webfrontend"); // 在 AgileConfig 添加一个应用 webfrontend，客户端会从这里读取业务


var apiService = builder.AddProject<Projects.AspireProjectWithAgileConfig_ApiService>("apiservice");
var webFrontend = builder.AddProject<Projects.AspireProjectWithAgileConfig_Web>("webfrontend").WithExternalHttpEndpoints();

apiService.WithReference(agileConfig_apiservice); // apiservice 项目引用 agileConfig_apiservice 应用
apiService.WaitFor(agileConfig); // apiservice 项目等待 agileConfig container 启动后再启动自己

webFrontend.WithReference(agileConfig_webfrontend);  // apiservice 项目引用 agileConfig_webfrontend 应用
webFrontend.WaitFor(agileConfig); // webFrontend 项目等待 apiservice container 启动后再启动自己

webFrontend.WithReference(apiService);
webFrontend.WaitFor(apiService);

builder.Build().Run();

```

### 在客户端项目上使用 AgileConfig.Client

要连接 AgileConfig 服务端，我们需要在客户端项目添加新的包引用：

```
dotnet add package AgileConfig.Client.Aspire --version 1.0.0
```

以 ApiService 项目为例：

```
using Aspire.AgileConfig.Client;

var appName = "apiService";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAspireAgileConfig(appName);
```

客户端项目现在配置起来就超级简单了，只需要一行代码就解决问题了，你甚至不需要去配置 appsettings 来指定 agileconfig 的相关配置它就能运行了。
