USE master

if db_id('Test_ProjectsDB') is null
CREATE DATABASE Test_ProjectsDB

GO
USE Test_ProjectsDB
CREATE TABLE Worker (
ID int identity not null primary key,
FirstName varchar(50) not null,
SecondName varchar(50) not null,
ThirdName varchar(50) not null,
Email varchar(50) not null);

GO 
CREATE TABLE Company (
ID int identity not null primary key,
Name varchar(50) not null
	);

GO

CREATE TABLE Project(
ID int identity not null primary key,
Name varchar(50) not null,
CustomerID int,
ExecutorID int ,
LeaderID int ,
StartDate datetime not null,
EndDate datetime not null,
Priority_ int not null,
Comment varchar(100)
	 );
GO
CREATE TABLE ProjectWorker(
ID int identity not null primary key,
ProjectID int not null,
WorkerID int not null,
CONSTRAINT FK_Worker_Project FOREIGN KEY (WorkerID)   
    REFERENCES Worker (ID),
	 CONSTRAINT FK_Project_Project FOREIGN KEY (ProjectID)   
    REFERENCES Project (ID)  );

GO
--тригер на случай удаления значения компании
CREATE TRIGGER TR_Del_Company
    ON Company
    INSTEAD OF DELETE
AS
DECLARE @del_id int
SET @del_id=(SELECT deleted.ID FROM deleted) 
    UPDATE Project SET CustomerID=null WHERE CustomerID=@del_id	
    UPDATE Project SET ExecutorID=null WHERE ExecutorID=@del_id
	DELETE Company WHERE Company.ID=@del_id
GO
--то же и для удаления из Project
CREATE TRIGGER TR_Del_Project
    ON Project
    INSTEAD OF DELETE
AS
DECLARE @del_id int
SET @del_id=(SELECT deleted.ID FROM deleted) 
    DELETE FROM ProjectWorker
    WHERE ProjectWorker.ProjectID=@del_id
	DELETE Project WHERE Project.ID=@del_id
GO
--воркер
CREATE TRIGGER TR_Del_Worker
    ON Worker
    INSTEAD OF DELETE
AS
DECLARE @del_id int
SET @del_id=(SELECT deleted.ID FROM deleted) 
    DELETE FROM ProjectWorker
    WHERE ProjectWorker.WorkerID=@del_id
	
	UPDATE Project SET LeaderID=null
	WHERE LeaderID=@del_id

	DELETE Worker WHERE Worker.ID=@del_id
GO

--заполнение
INSERT INTO Worker VALUES
('Иван','Иванов','Иванович','asd@mail.ru'),
('Василий','Васильев','Васильевич','asd2@mail.ru'),
('Петр','Петров','Петрович','asd3@mail.ru'),
('Алексей','Алексеев','Алексеевич','asd4@mail.ru');
GO

INSERT INTO Company VALUES
('Компания1'),
('Компания2'),
('Компания3'),
('Компания4'),
('Компания5');
GO
