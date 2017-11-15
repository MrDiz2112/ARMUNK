create database ARMUNK 
use ARMUNK 

create table Students ( 
id_student int not null IDENTITY(1,1), 
FIO_students nvarchar (100) not null, 
Telephone_number_students nvarchar (100)) 

create table Parents ( 
id_parents int not null IDENTITY(1,1), 
id_student1 int not null, 
FIO_mather nvarchar (100) not null, 
FIO_father nvarchar (100) not null, 
Telephone_number_mather nvarchar (100) not null, 
Telephone_number_father nvarchar (100) not null, 
Email_father nvarchar (100) not null, 
Email_mother nvarchar (100) not null, 
Adress_family nvarchar (100) not null) 

insert into Students values 

('Андреанов Денис Андреевич', 89100000101), 
('Аввакумов Алексей Иванович', 89100113445), 
('Вурносова Екатерина Валерьевна', 89045678831), 
('Гнесина Ольга Михайловна', 89154338000), 
('Ежов Виктор Владимирович', 89101511567), 
('Жуков Андрей Дмитриевич', 89067775431), 
('Ильин Константин Сергеевич', 89167009181), 
('Лобанова Мария Евгеньевна', 89045670000), 
('Митусов Аркадий Иванович', 89158889032), 
('Мазохина Юлия Юрьевна', 89007776544), 
('Тишкина Анастасия Викторовна', 89034569988), 
('Орусов Антон Павлович', 89036445555), 
('Пеганов Илья Борисович', 89106665500), 
('Россов Михаил Вадимович', 89021112445), 
('Ухина Александра Юрьевна', 89166669908) 

insert into Parents values 

(001, 'Андреанова Олеся Игоревна', 'Андреанов Андрей Сергеевич', 89205151169, 89046847677, 'diz2012x@gmail.com', 'Diz2112@yandex.ru', 'Судогодское шоссе д.41 кв.1'), 
(002,'Аввакумова Лидия Витальевна', 'Аввакумов Иван Дмитриевич', 89046847877, 89202404561, 'MrDiz2112@yandex.ru', 'MrDiz2112@yandex.ru', 'Добросельская д.34 кв.33'), 
(003,'Вурносова Марина Алексеевна', 'Вурносов Валерий Владимирович', 89605697346, 89994682569, '123@sample.com', '123@sample.com', 'Большая д.1 кв.6'), 
(004,'Гнесина Татьяна Романовна', 'Гнесин Михаил Юрьевич', 89004798320, 89506712598, '123@sample.com', '123@sample.com', 'Никитская д.7 кв.117'), 
(005,'Ежова Светлана Андреевна', 'Ежов Владимир Сергеевич', 89046213793, 89992683541, '123@sample.com', '123@sample.com', 'Добросельская д.44 кв.'), 
(006,'Жукова Наталья Владимировна', 'Жуков Дмитрий Алексеевич', 89204596236, 89046235791, '123@sample.com', '123@sample.com', 'Токарева д.9 кв.78'), 
(007,'Ильина Оксана Александровна', 'Ильин Сергей Александрович', 89507923460, 89105248317, '123@sample.com', '123@sample.com', 'Горького д.90 кв.100'), 
(008,'Лобанова Елизавета Петровна', 'Лобанов Евгений Михайлович', 89045696321, 89105683247, '123@sample.com', '123@sample.com', 'Офицерская д.78 кв.88'), 
(009,'Митусова Светлана Васильевна', 'Митусов Иван Сергеевич', 89143570525, 89256414693, '123@sample.com', '123@sample.com', 'Каманина д.6 кв.50'), 
(010,'Мазохина Анастасия Анатольвна', 'Мазохин Юрий Владимирович', 89313507625, 89234606734, '123@sample.com', '123@sample.com', 'Мира д.92 кв.12'), 
(011,'Тишкина Юлия Игоревна', 'Тишкин Виктор Семенович', 89235607145, 89245793127, '123@sample.com', '123@sample.com', 'Восстания д.44 кв.1'), 
(012,'Орусова Дарья Сергеевна', 'Орусов Павел Адреевич', 89055694217, 89335794158, '123@sample.com', '123@sample.com', 'Терешковой д.9 кв.2'), 
(013,'Пеганова Алёна Станиславовна', 'Пеганов Борис Геннадьевич', 89263903264, 89269531584, '123@sample.com', '123@sample.com', 'Революции д.95 кв.19'), 
(014,'Россова Валерия Олеговна', 'Россов Владимир Владимирович', 89051478426, 89357425803, '123@sample.com', '123@sample.com', 'Пионерская д.7 кв.8'), 
(015,'Ухина Анна Викторовна', 'Ухин Юрий Юрьевич', 89604573199, 89270415831, '123@sample.com', '123@sample.com', 'Гастелло д.45 кв.30') 

alter table Students add constraint pk_idstudent primary key ("id_student") 
alter table Parents add constraint fk_idstudent foreign key ("id_student1") references Students("id_student") 

alter table Parents add constraint pk_idparents primary key ("id_parents") 

create table Subjects ( 
id_subject int not null, 
name_subjects nvarchar (50) not null) 

insert into Subjects (id_subject, name_subjects) values 

(01, 'Русский язык'), 
(02, 'Математика'), 
(03, 'Чтение'), 
(04, 'Музыка'), 
(05, 'Физкультура'), 
(06, 'Природоведение'), 
(07, 'Рисование'), 
(08, 'Труд'), 
(09, 'Английский язык') 

create table Marks ( 
id_marks int not null IDENTITY(1,1), 
id_subject1 int not null, 
id_student2 int not null, 
mark int, 
type_mark nvarchar (max), 
date_mark date, 
fourth int) 

INSERT INTO Marks VALUES 

(1, 1, 5, 'Контрольная', '2017-05-14', 4), 
(2, 1, 2, 'Контрольная', '2017-05-14', 4) 

alter table Marks add constraint pk_idmarks primary key ("id_marks") 

alter table Subjects add constraint pk_idsubjects primary key ("id_subject") 
alter table Marks add constraint fk_idsubjects foreign key ("id_subject1") references Subjects("id_subject") 

alter table Marks add constraint fk_idstudents foreign key ("id_student2") references Students("id_student") 

go 

CREATE VIEW students_marks AS 
SELECT Students.FIO_students AS "ФИО", 
Subjects.name_subjects AS "Предмет", Marks.mark AS "Оценка", Marks.type_mark AS "Тип", 
Marks.fourth AS "Четверть", Marks.date_mark AS "Дата выставления" 
FROM Students LEFT JOIN Marks 
ON Students.id_student=Marks.id_student2 

LEFT JOIN Subjects 
ON Subjects.id_subject=Marks.id_subject1 

go 

CREATE VIEW students_parents AS 
SELECT Students.FIO_students AS "ФИО", Students.Telephone_number_students AS "Телефон", 
Parents.FIO_father AS "ФИО Отца", Parents.FIO_mather AS "ФИО Матери", 
Parents.Telephone_number_father AS "Телефон Отца", Parents.Telephone_number_mather AS "Телефон Матери", 
Parents.Email_father AS "E-Mail Отца", Parents.Email_mother AS "E-Mail Матери", 
Parents.Adress_family AS "Адрес" 
FROM Students LEFT JOIN Parents 
ON Students.id_student=Parents.id_student1 

go 

CREATE VIEW avg_marks AS 
SELECT Students.FIO_students, Subjects.name_subjects, CAST(ROUND(AVG(CAST(Marks.mark as decimal)), 0) as int) AS "Средний балл" 
FROM Students INNER JOIN Marks 
ON Students.id_student=Marks.id_student2 
INNER JOIN Subjects 
ON Marks.id_subject1=Subjects.id_subject 

GROUP BY Students.FIO_students, Subjects.name_subjects 

go 

CREATE TABLE Meeting ( 
id_meeting int not null IDENTITY(1,1), 
name_meeting nvarchar (max), 
date_meeting date) 

CREATE TABLE Meeting_isCome ( 
id_iscome int not null IDENTITY(1,1), 
id_meeting1 int not null, 
id_student int not null, 
parents_iscome nvarchar (max)) 

alter table Meeting add constraint pk_idmeeting primary key ("id_meeting") 
alter table Meeting_isCome add constraint pk_idiscome primary key (id_iscome) 

alter table Meeting_isCome add constraint fk_idmeeting foreign key ("id_meeting1") references Meeting("id_meeting") 
alter table Meeting_isCome add constraint fk_idstudents1 foreign key ("id_student") references Students("id_student") 

go 

CREATE VIEW meeting_students AS 
SELECT Meeting.name_meeting AS "Тема собрания", Meeting.date_meeting AS "Дата собрания", 
Students.FIO_students AS "Родители ученика", Meeting_isCome.parents_iscome AS "Явка" 
FROM Meeting INNER JOIN Meeting_isCome 
ON Meeting.id_meeting=Meeting_isCome.id_meeting1 
INNER JOIN Students 
ON Students.id_student=Meeting_isCome.id_student 

go 

CREATE TABLE teacher_email ( 
id_email int not null, 
email nvarchar (max), 
email_pass nvarchar (max)) 

go 
