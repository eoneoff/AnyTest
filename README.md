# Установка необходимого ПО

Загрузить и установить [Visual Studo 2019 Community Edition](https://visualstudio.microsoft.com/ru/vs/community/) (Младшие версии не подойдут).

При установке выбрать пункты ASP.NET и разработка веб-приложений, Разработка мобильных приложений на .NET, Хранение и обработка данных, Кроссплатформенная разработка .NET Core

Скачать и установить последнюю версию [.NET Core SDK](https://dotnet.microsoft.com/download)

После установки открыть любое приложение командной строки (cmd или PowerShell) и выполнить
```
dotnet tool install --global dotnet-ef
```

Скачать и установить [Microsoft SQL Server 2019 Developer](https://www.microsoft.com/ru-ru/sql-server/sql-server-downloads)

Во время установки выбирать Пользовательский тип установки

![](images/sqlinstall1.jpg)

После скачивания установочного пакета откроется Центр установки SQL Server. В нем в боковом меню нужно выбрать пункт **Установка** и на вкладке выбрать **Новая установка изолированного экземпляра SQL Server или добавлениие компонентов к существующей установке**

![](images/sqlinstall2.jpg)

Далее принимаем лицензию и выбираем все пункты по умолчанию, кроме

На вкладке **Выбор компонетов** обязательно убеждаемся, что выбраны **Службы ядра СУБД** и **Полнотекстовый и семантический поиск**

![](images/sqlinstall3.jpg)

На вкладке **Настройки экземпляра** выбираем **Экземпляр по умолчанию**

![](images/sqlinstall4.jpg)

На вкладке **Конфигурация сервера** во вкладке **Параметры сортировки** выбрать **Cyrillic_General**

![](images/sqlinstall5.jpg)

На вкладке **Настойка ядра СУБД** во вкладке **Конфигурация сервера** нажать внизу окна **Добавить текущего пользователя**

![](images/sqlinstall6.jpg)

Для удобства работы с MS SQL Server можно (но не обязательно) скачать и установить [SQL Server Management Studio](https://docs.microsoft.com/ru-ru/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
