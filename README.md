Настройка подключения к базе данных:
---

      •В корневом каталоге проекта найти файл Web.config
      •Открыть его и заменить значение data source, на имя вашего sql сервера
      <connectionStrings>    <add name="Test_ProjectsDBEntities" connectionString="metadata=res://*/DB.M_Projects.csdl|res://*/DB.M_Projects.ssdl|res://*/DB.M_Projects.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=S4LT0N;initial catalog=Test_ProjectsDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
      </connectionStrings>
      •Открыть проект
      •Восстановить nuget пакеты  
  
Так же прилагается .sql файлик для создания бд и добавлением пары значений.  
Используемое ПО: VS 2017, MS SQL 2016  
«Фреймворки»: .net 4.5, entity framework 6  
Вспомогательные средства: ~=10 часов времени, кофеин, гугл
  
