create database ChatApp

use ChatApp
go

create table Messages
(
	id int not null primary key identity(1,1),
	recipient varchar(100) not null, 
	message varchar(500) not null,
	author varchar(100) not null, 
	date datetime not null default getdate()
)

create table Users
(
	id int not null primary key identity(1,1),
	name varchar(100) not null 
)

create table Channels
(
	id int not null primary key identity(1,1),
	name varchar(100) not null
)

insert into Messages(recipient, message, author, date)
values('#test','Hello Test channel','@me','2012-04-23T18:25:43.511Z')
insert into Messages(recipient, message, author, date)
values('@bob','Hello Bob','@me','2012-04-23T18:25:43.511Z')
insert into Messages(recipient, message, author, date)
values('@bob','Hello Myself','@me','2012-04-23T18:25:43.511Z')
insert into Messages(recipient, message, author, date)
values('#test','Hello again Test channel','@me','2012-04-23T18:25:43.511Z')
insert into Messages(recipient, message, author, date)
values('@bob','Hello again Bob','@me','2012-04-23T18:25:43.511Z')

insert into Users(name)
values('me')
insert into Users(name)
values('bob')

insert into Channels(name)
values('#test')