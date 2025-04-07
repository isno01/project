

CREATE DATABASE StudentInfoDB;

Use StudentInfoDB;

CREATE TABLE CourseTB (
	courseId int,
    courseName varchar(250),
    Primary Key(courseId)
);

Create Table YearTB(
     yearId  int,
     yearLvl int,
     Primary Key(yearId)
);

INSERT INTO CourseTB (courseId, courseName)
VALUES (1, "IT"), (2, "BSBA"), (3, "ABEL"), (4, "BPA");

INSERT INTO YearTB (yearId, yearLvl)
VALUES (1, 1), (2, 2), (3, 3), (4, 4);

CREATE TABLE StudentRecordTB(
	studentId int,
    firstName varchar(250),
    lastName varchar(250),
    middleName varchar(250),
    houseNo int,
    brgyName varchar(250),
    municipality varchar(250),
    province varchar(250),
    region varchar(250),
    country varchar(250),
    birthdate varchar(250),
    age int,
    studContactNo varchar(250),
    emailAddress varchar(250),
    guardianFirstName varchar(250),
    guardianLastName varchar(250),
    hobbies varchar(250),
    nickname varchar(250),
    courseId int, 
    yearId int,
    Primary Key (studentId),
    foreign key (courseId) References CourseTB(courseId),
	foreign key (yearId) References YearTB(yearId)
);
INSERT INTO StudentRecordTB (
    studentId,
    firstName,
    lastName,
    middleName,
    houseNo,
    brgyName,
    municipality,
    province,
    region,
    country,
    birthdate,
    age,
    studContactNo,
    emailAddress,
    guardianFirstName,
    guardianLastName,
    hobbies,
    nickname,
    courseId,
    yearId
) 
VALUES 
(1, 'John', 'Doe', 'Smith', 123, 'Poblacion', 'Citytown', 'ProvinceX', 'RegionY', 'CountryZ', '2000-05-10', 24, '09171234567', 'john.doe@example.com', 'Jane', 'Doe', 'Reading, Swimming', 'Johnny', 101, 2),
(2, 'Alice', 'Johnson', 'Marie', 456, 'San Isidro', 'Townville', 'ProvinceY', 'RegionZ', 'CountryX', '1999-08-20', 25, '09179876543', 'alice.johnson@example.com', 'Mark', 'Johnson', 'Photography, Hiking', 'Ali', 102, 1),
(3, 'Bob', 'Williams', 'Lee', 789, 'Luna', 'Villagetown', 'ProvinceZ', 'RegionA', 'CountryY', '2001-03-15', 23, '09171239876', 'bob.williams@example.com', 'Mary', 'Williams', 'Gaming, Cycling', 'Bobby', 103, 3),
(4, 'Clara', 'Martinez', 'Gomez', 101, 'Bagumbayan', 'Cityville', 'ProvinceX', 'RegionB', 'CountryZ', '2000-12-05', 24, '09172345678', 'clara.martinez@example.com', 'Carlos', 'Martinez', 'Traveling, Reading', 'Clari', 104, 2),
(5, 'David', 'Brown', 'Taylor', 202, 'Talipapa', 'Oldtown', 'ProvinceY', 'RegionC', 'CountryX', '1998-06-25', 26, '09173456789', 'david.brown@example.com', 'Susan', 'Brown', 'Movies, Sports', 'Dave', 105, 1);


