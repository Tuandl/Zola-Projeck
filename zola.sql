CREATE TABLE Users (
	username varchar(50) primary key,
	[password] varchar(50) not null,
	[name] nvarchar(100) not null,
	gender bit,
	avatar_url varchar(200) DEFAULT '/Resources/Avatar/default.jpg'
)

CREATE TABLE RelationType(
	id int primary key,
	relation_name varchar(50) not null
)

CREATE TABLE Relations (
	id bigint primary key IDENTITY(1,1),
	foo varchar(50) foreign key references Users(username) not null,
	bar varchar(50) foreign key references Users(username) not null,
	relation int foreign key references RelationType(id) not null
)

CREATE TABLE [Messages] (
	id bigint primary key IDENTITY(1,1),
	sender varchar(50) foreign key references Users(username) not null,
	receiver varchar(50) foreign key references Users(username) not null,
	time_sent datetime not null,
	[message] nvarchar(MAX)
)

--ALTER TABLE Users 
--ADD CONSTRAINT DF_avatar DEFAULT '/Resources/avatars/default.jpg' FOR avatar_url