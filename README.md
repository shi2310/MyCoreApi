# MyCoreApi
.net core 3.1 api示例

nuget包  
Install-Package Microsoft.EntityFrameworkCore.SqlServer  
Install-Package Microsoft.EntityFrameworkCore.InMemory  
Install-Package Microsoft.EntityFrameworkCore.Tools

数据库注册  
appsettings.json  
<pre><code>
"ConnectionStrings": {
    "todoContext": "server=.;database=TodoDatas;uid=sa;pwd=123456"
}
</code></pre>  

CodeFirst方式生成数据库  
Add-Migration InitDatabase  
update-database  

使用swagger  
Install-Package Swashbuckle.AspNetCore  

修改启动项  
Properties/launchSettings.json  
<pre><code>"launchUrl": "swagger"</code></pre>
