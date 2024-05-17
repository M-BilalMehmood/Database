CREATE DATABASE projectDB;
Drop database projectDB
USE projectDB;

CREATE TABLE Administrator (
	AdministratorID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255),
	Email VARCHAR(255),
	[Password] VARCHAR(255),
	[Role] VARCHAR(255)
);

CREATE TABLE StaffMember (
	StaffID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255),
	Email VARCHAR(255),
	[Password] VARCHAR(255),
	[Role] VARCHAR(255)
);

CREATE TABLE Parent (
	ParentID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255),
	Email VARCHAR(255),
	PhoneNumber VARCHAR(11),
	[Password] VARCHAR(8),
	CNIC VARCHAR(13),
	[Address] VARCHAR(255)
);

CREATE TABLE Child (
	ChildID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255),
	DateOfBirth DATE,
	ParentID INT,
	FOREIGN KEY (ParentID) REFERENCES Parent(ParentID)
);

CREATE TABLE Classroom (
	ClassroomID INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255),
	Capacity INT,
	Location VARCHAR(255)
);

CREATE TABLE assignedClassrooms(
ClassroomID INT,
StaffID INT,
FOREIGN KEY (StaffID) REFERENCES StaffMember(StaffID),
FOREIGN KEY (ClassroomID) REFERENCES Classroom(ClassroomID)
);

CREATE TABLE Enrollment (
	EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
	EnrollmentDate DATE,
	ChildID INT,
	StartDate DATE,
	EndDate DATE,
	[Status] VARCHAR(255),
	ClassroomID INT,
	FOREIGN KEY (ChildID) REFERENCES Child(ChildID),
	FOREIGN KEY (ClassroomID) REFERENCES Classroom(ClassroomID)
);

CREATE TABLE AttendanceRecord (
	EnrollmentID INT,
	[Date] DATE,
	[Status] VARCHAR(255),
	FOREIGN KEY (EnrollmentID) REFERENCES Enrollment(EnrollmentID)
);

CREATE TABLE Announcement (
	AnnouncementID INT PRIMARY KEY IDENTITY(1,1),
	AdministratorID INT,
	[DateTime] DATETIME,
	[Message] TEXT,
	FOREIGN KEY (AdministratorID) REFERENCES Administrator(AdministratorID)
);

--Drop table Schedule

CREATE TABLE Schedule (
	ScheduleID INT PRIMARY KEY IDENTITY(1,1),
	[Date] DATE,
	StartTime TIME,
	EndTime TIME,
);

CREATE TABLE assignedSchedules(
	ScheduleID INT,
	StaffID INT
	FOREIGN KEY (ScheduleID) REFERENCES Schedule(ScheduleID),
	FOREIGN KEY (StaffID) REFERENCES StaffMember(StaffID)
);

CREATE TABLE StaffAttendance(
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    StaffID INT,
    ScheduleID INT,
    [Date] DATE,
    [Status] VARCHAR(255),
    FOREIGN KEY (StaffID) REFERENCES StaffMember(StaffID),
    FOREIGN KEY (ScheduleID) REFERENCES Schedule(ScheduleID)
);

--DROP TABLE FEE
CREATE TABLE Fee (
	FeeID INT PRIMARY KEY IDENTITY(1,1),
	ChildID INT,
	Amount DECIMAL(10, 2),
	BillingDate DATE,
	DueDate DATE,
	[Status] VARCHAR(255),
	FOREIGN KEY (ChildID) REFERENCES Child(ChildID)
);
--DROP TABLE Payment
CREATE TABLE Salary (
	SalaryID INT PRIMARY KEY IDENTITY(1,1),
	StaffID INT,
	Amount DECIMAL(10, 2),
	PaymentDate DATE,
	FOREIGN KEY (StaffID) REFERENCES StaffMember(StaffID)
);

CREATE TABLE Timetable (
	TimetableID INT PRIMARY KEY IDENTITY(1,1),
	ClassroomID INT,
	StaffID INT,
	[Date] DATE,
	StartTime TIME,
	EndTime TIME,
	FOREIGN KEY (ClassroomID) REFERENCES Classroom(ClassroomID),
	FOREIGN KEY (StaffID) REFERENCES StaffMember(StaffID)
);

CREATE TABLE Logs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    LogDateTime DATETIME,
    Email NVARCHAR(255)
);

--DROP TABLE Mails
CREATE TABLE Mails(
	MailID INT PRIMARY KEY IDENTITY(1,1),
	SendingDateTime DATETIME,
	SenderEmail VARCHAR(255),
	ReceiverEmail VARCHAR(255),
	[Message] VARCHAR(255)
);


-- Administrators
INSERT INTO Administrator VALUES ('Bilal Mehmood', 'bilal.mehmood@nu.edu.pk', 'admin1', 'Admin');
INSERT INTO Administrator VALUES ('Azmar Kashif', 'azmar.kashif@nu.edu.pk', 'admin2', 'Admin');

-- Staff Members

INSERT INTO StaffMember ([Name], Email, [Password], [Role]) VALUES 
('Ahmed Hassan', 'ahmed.hassan@nu.edu.pk', 'staff1', 'Staff'),
('Fatima Ali', 'fatima.ali@nu.edu.pk', 'staff2', 'Staff'),
('Zain Malik', 'zain.malik@nu.edu.pk', 'staff3', 'Staff'),
('Ayesha Ahmed', 'ayesha.ahmed@nu.edu.pk', 'staff4', 'Staff'),
('Usman Khalid', 'usman.khalid@nu.edu.pk', 'staff5', 'Staff');

-- Parents
INSERT INTO Parent ([Name], Email, PhoneNumber, [Password], CNIC, [Address]) VALUES 
('Asad Iqbal', 'asad.iqbal@nu.edu.pk','03000000001' , 'parent1', '3252154231524', 'House#79, Street#9, F10/4, Islamabad, Pakistan'),
('Zara Zafar', 'zara.zafar@nu.edu.pk','03000000002', 'parent2', '2226514856324', 'House#96, Street#5, Askari4, Karachi, Pakistan'),
('Ali Raza', 'ali.raza@nu.edu.pk','03000000003', 'parent3', '3330305267251', 'House#299, Street#38, G9/1, Islamabad, Pakistan'),
('Sara Khan', 'sara.khan@nu.edu.pk','03000000004', 'parent4', '8885123254756', 'House#644, Infront of Alfalah Bank, Satellite Town, Sadqabad, Rawalpindi, Pakistan'),
('Faisal Qureshi', 'faisal.qureshi@nu.edu.pk','03000000005', 'parent5', '5556214236587', 'House#55, Street#2, F8/1, Islamabad, Pakistan'),
('Mehwish Hayat', 'mehwish.hayat@nu.edu.pk','03000000006', 'parent6', '8526473169425', 'House#666, Mustafabad, Faisalabad, Pakistan'),
('Hamza Ali', 'hamza.ali@nu.edu.pk', '03000000007','parent7', '1110325486521', 'House#12, Street#5, H13/3, Islamabad, Pakistan'),
('Mahira Khan', 'mahira.khan@nu.edu.pk','03000000008', 'parent8', '2451535215475', 'House#88, Street#13, E11/4, Islamabad, Pakistan');

-- Children
INSERT INTO Child ([Name], DateOfBirth, ParentID) VALUES
('Ayan Asad', '2010-01-01', 1),
('Zoya Asad', '2011-02-02', 1),
('Rahim Zara', '2012-03-03', 2),
('Hania Zara', '2013-04-04', 2),
('Saad Bilal', '2014-05-05', 3),
('Ayesha Bilal', '2015-06-06', 3),
('Ali Nida', '2016-07-07', 4),
('Sara Nida', '2017-08-08', 4),
('Ahmed Faisal', '2018-09-09', 5),
('Zainab Faisal', '2019-10-10', 5),
('Usman Mehwish', '2020-11-11', 6),
('Fatima Mehwish', '2021-12-12', 6),
('Hassan Hamza', '2022-01-13', 7),
('Aiman Hamza', '2023-02-14', 7),
('Bilal Mahira', '2024-03-15', 8),
('Mahira Arshad', '2024-03-15', 8),
('Ahmed Tariq', '2024-03-15', 5);

--SELECT * FROM StaffMember

INSERT INTO Classroom ([Name], Capacity, Location) VALUES
('Classroom A', 30, 'First Floor, Block A'),
('Classroom B', 25, 'Second Floor, Block B'),
('Classroom C', 20, 'Ground Floor, Block C');

INSERT INTO assignedClassrooms(StaffID, ClassroomID) VALUES
(1, 1),
(2, 2),
(3, 2),
(4, 1),
(5, 3);

INSERT INTO Enrollment (EnrollmentDate, ChildID, StartDate, EndDate, [Status], ClassroomID) VALUES
('2023-06-01', 1, '2023-07-01', '2025-12-31', 'Approved', 1),
('2023-06-15', 2, '2023-07-15', '2025-12-31', 'Approved', 2),
('2023-07-01', 3, '2023-08-01', '2025-12-31', 'Approved', 1),
('2023-07-15', 4, '2023-08-15', '2025-12-31', 'Approved', 2),
('2023-08-01', 5, '2023-09-01', '2025-12-31', 'Approved', 3),
('2023-06-01', 6, '2023-07-01', '2025-12-31', 'Approved', 3),
('2023-06-15', 7, '2023-07-15', '2025-12-31', 'Approved', 3),
('2023-07-01', 8, '2023-08-01', '2025-12-31', 'Approved', 2),
('2023-07-15', 9, '2023-08-15', '2025-12-31', 'Approved', 2),
('2023-08-01', 10, '2023-09-01', '2025-12-31', 'Approved', 1),
('2023-06-01', 11, '2023-07-01', '2025-12-31', 'Approved', 1),
('2023-06-15', 12, '2023-07-15', '2025-12-31', 'Approved', 1),
('2023-07-01', 13, '2023-08-01', '2025-12-31', 'Approved', 2),
('2023-07-15', 14, '2023-08-15', '2025-12-31', 'Approved', 1),
('2023-08-01', 15, '2023-09-01', '2025-12-31', 'Approved', 2),
('2023-08-01', 16, '2023-09-01', '2025-12-31', 'Waiting', Null),
('2023-08-01', 17, '2023-09-01', '2025-12-31', 'Rejected', Null);

INSERT INTO AttendanceRecord (EnrollmentID, [Date], [Status]) VALUES
(1, '2023-07-01', 'Present'),
(2, '2023-07-01', 'Present'),
(3, '2023-07-01', 'Present'),
(4, '2023-07-01', 'Present'),
(5, '2023-07-01', 'Present'),
(6, '2023-07-01', 'Present'),
(7, '2023-07-01', 'Present'),
(8, '2023-07-01', 'Absent'),
(9, '2023-07-01', 'Present'),
(10, '2023-07-01', 'Present'),
(11, '2023-07-01', 'Present'),
(12, '2023-07-01', 'Present'),
(13, '2023-07-01', 'Present'),
(14, '2023-07-01', 'Present'),
(15, '2023-07-01', 'Present');

INSERT INTO Announcement (AdministratorID, [DateTime], [Message]) VALUES
(1, '2023-07-01 08:00:00', 'School will be closed on Eid holidays.'),
(1, '2023-07-15 10:00:00', 'Reminder: Parent-Teacher meeting on Saturday.'),
(2, '2023-08-01 09:00:00', 'Welcome back students! Hope you had a great summer break.'),
(1, '2024-05-05 08:00:00', 'Upcoming Birthday: Saad Bilal (5/5/2024) will turn 10 years old on his birthday on May 5th!'),
(2, '2024-07-15 08:00:00', 'Reminder: Last day for summer camp registration is July 20th.'),
(2, '2024-08-01 09:00:00', 'Reminder: School will reopen on August 15th after summer vacation.'),
(1, '2024-08-15 10:00:00', 'Welcome back! We hope you had a great break.'),
(2, '2024-09-01 09:00:00', 'Sports day scheduled for September 10th. Get ready for some exciting competitions.'),
(1, '2024-09-10 11:00:00', 'Reminder: Projects submission deadline extended to September 15th.'),
(2, '2024-09-25 14:00:00', 'Annual talent show coming up on October 1st. Start preparing your acts now!'),
(2, '2024-10-10 08:00:00', 'Reminder: Don''t forget to submit your artwork for the school art competition by October 15th.'),
(2, '2024-10-20 10:00:00', 'Parent-Teacher conferences scheduled for October 30th. Sign up sheets will be available starting next week.'),
(1, '2024-11-05 09:00:00', 'School photography session on November 15th. Get ready to say cheese!'),
(2, '2024-11-20 08:00:00', 'Annual Book Fair coming up on November 25th. Bring your reading lists and grab some great deals!'),
(2, '2024-12-05 09:00:00', 'Holiday toy drive starts next week. Let''s spread some joy this holiday season!'),
(1, '2024-12-20 10:00:00', 'Winter break starts on December 21st. Wishing everyone a happy and safe holiday season!');

INSERT INTO Schedule ([Date], StartTime, EndTime) VALUES
('2024-05-01', '08:00:00', '12:00:00'),
('2024-05-01', '08:30:00', '12:30:00'),
('2024-05-01', '09:00:00', '13:00:00'),
('2024-05-01', '09:30:00', '13:30:00'),
('2024-05-01', '10:00:00', '14:00:00'),
('2024-05-02', '08:00:00', '12:00:00'),
('2024-05-02', '08:30:00', '12:30:00'),
('2024-05-02', '09:00:00', '13:00:00'),
('2024-05-02', '09:30:00', '13:30:00'),
('2024-05-02', '10:00:00', '14:00:00'),
('2024-05-03', '08:00:00', '12:00:00'),
('2024-05-03', '08:30:00', '12:30:00'),
('2024-05-03', '09:00:00', '13:00:00'),
('2024-05-03', '09:30:00', '13:30:00'),
('2024-05-03', '10:00:00', '14:00:00'),
('2024-05-04', '08:00:00', '12:00:00'),
('2024-05-04', '08:30:00', '12:30:00'),
('2024-05-04', '09:00:00', '13:00:00'),
('2024-05-04', '09:30:00', '13:30:00'),
('2024-05-04', '10:00:00', '14:00:00'),
('2024-05-05', '08:00:00', '12:00:00'),
('2024-05-05', '08:30:00', '12:30:00'),
('2024-05-05', '09:00:00', '13:00:00'),
('2024-05-05', '09:30:00', '13:30:00'),
('2024-05-05', '10:00:00', '14:00:00');

-- Insert data into assignedSchedules table
INSERT INTO assignedSchedules (ScheduleID, StaffID)
VALUES
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5), 
(6, 1), (7, 2), (8, 3), (9, 4), (10, 5), 
(11, 1), (12, 2), (13, 3), (14, 4), (15, 5), 
(16, 1), (17, 2), (18, 3), (19, 4), (20, 5), 
(21, 1), (22, 2), (23, 3), (24, 4), (25, 5); 

-- Insert data into StaffAttendance table
INSERT INTO StaffAttendance (StaffID, ScheduleID, [Date], [Status]) VALUES
(1, 1, '2024-05-01', 'Present'),
(2, 2, '2024-05-01', 'Present'),
(3, 3, '2024-05-01', 'Present'),
(4, 4, '2024-05-01', 'Absent'),
(5, 5, '2024-05-01', 'Present'),
(1, 6, '2024-05-02', 'Absent'),
(2, 7, '2024-05-02', 'Present'),
(3, 8, '2024-05-02', 'Present'),
(4, 9, '2024-05-02', 'Absent'),
(5, 10, '2024-05-02', 'Present'),
(1, 11, '2024-05-03', 'Present'),
(2, 12, '2024-05-03', 'Absent'),
(3, 13, '2024-05-03', 'Present'),
(4, 14, '2024-05-03', 'Absent'),
(5, 15, '2024-05-03', 'Present'),
(1, 16, '2024-05-04', 'Absent'),
(2, 17, '2024-05-04', 'Present'),
(3, 18, '2024-05-04', 'Absent'),
(4, 19, '2024-05-04', 'Absent'),
(5, 20, '2024-05-04', 'Present'),
(1, 21, '2024-05-05', 'Present'),
(2, 22, '2024-05-05', 'Absent'),
(3, 23, '2024-05-05', 'Absent'),
(4, 24, '2024-05-05', 'Absent'),
(5, 25, '2024-05-05', 'Absent');


-- May 2024
INSERT INTO Fee (ChildID, Amount, BillingDate, DueDate, [Status]) VALUES
(1, 2500.00, '2024-05-01', '2024-05-31', 'Unpaid'),
(9, 3800.00, '2024-05-01', '2024-05-31', 'Paid'),
(8, 1900.00, '2024-05-01', '2024-05-31', 'Paid'),
(6, 1200.00, '2024-05-01', '2024-05-31', 'Unpaid'),
(5, 4200.00, '2024-05-01', '2024-05-31', 'Paid'),
(3, 3100.00, '2024-05-01', '2024-05-31', 'Paid'),
(2, 2800.00, '2024-05-01', '2024-05-31', 'Paid'),
(4, 1500.00, '2024-05-01', '2024-05-31', 'Paid'),
(7, 2000.00, '2024-05-01', '2024-05-31', 'Paid'),
(10, 3500.00, '2024-05-01', '2024-05-31', 'UnPaid'),
(12, 4000.00, '2024-05-01', '2024-05-31', 'Paid'),
(11, 2300.00, '2024-05-01', '2024-05-31', 'Paid'),
(14, 1800.00, '2024-05-01', '2024-05-31', 'Paid'),
(15, 3200.00, '2024-05-01', '2024-05-31', 'Paid'),
(13, 2700.00, '2024-05-01', '2024-05-31', 'Paid');

-- April 2024
INSERT INTO Fee (ChildID, Amount, BillingDate, DueDate, [Status]) VALUES
(1, 4500.00, '2024-04-01', '2024-04-30', 'Paid'),
(9, 1800.00, '2024-04-01', '2024-04-30', 'Paid'),
(8, 5900.00, '2024-04-01', '2024-04-30', 'Paid'),
(6, 8200.00, '2024-04-01', '2024-04-30', 'Paid'),
(5, 3200.00, '2024-04-01', '2024-04-30', 'Paid'),
(3, 8100.00, '2024-04-01', '2024-04-30', 'Paid'),
(2, 2800.00, '2024-04-01', '2024-04-30', 'Paid'),
(4, 5500.00, '2024-04-01', '2024-04-30', 'Paid'),
(7, 1000.00, '2024-04-01', '2024-04-30', 'Paid'),
(10, 3500.00, '2024-04-01', '2024-04-30', 'Paid'),
(12, 1000.00, '2024-04-01', '2024-04-30', 'Paid'),
(11, 1300.00, '2024-04-01', '2024-04-30', 'Paid'),
(14, 1800.00, '2024-04-01', '2024-04-30', 'Paid'),
(15, 5200.00, '2024-04-01', '2024-04-30', 'Paid'),
(13, 8700.00, '2024-04-01', '2024-04-30', 'Paid');

-- March 2024
INSERT INTO Fee (ChildID, Amount, BillingDate, DueDate, [Status]) VALUES
(1, 2500.00, '2024-03-01', '2024-03-31', 'Paid'),
(9, 3800.00, '2024-03-01', '2024-03-31', 'Paid'),
(8, 1900.00, '2024-03-01', '2024-03-31', 'Paid'),
(6, 1200.00, '2024-03-01', '2024-03-31', 'Paid'),
(5, 4200.00, '2024-03-01', '2024-03-31', 'Paid'),
(3, 3100.00, '2024-03-01', '2024-03-31', 'Paid'),
(2, 2800.00, '2024-03-01', '2024-03-31', 'Paid'),
(4, 1500.00, '2024-03-01', '2024-03-31', 'Paid'),
(7, 2000.00, '2024-03-01', '2024-03-31', 'Paid'),
(10, 3500.00, '2024-03-01', '2024-03-31', 'Paid'),
(12, 4000.00, '2024-03-01', '2024-03-31', 'Paid'),
(11, 2300.00, '2024-03-01', '2024-03-31', 'Paid'),
(14, 1800.00, '2024-03-01', '2024-03-31', 'Paid'),
(15, 3200.00, '2024-03-01', '2024-03-31', 'Paid'),
(13, 2700.00, '2024-03-01', '2024-03-31', 'Paid');

-- February 2024
INSERT INTO Fee (ChildID, Amount, BillingDate, DueDate, [Status]) VALUES
(1, 8500.00, '2024-02-01', '2024-02-29', 'Paid'),
(9, 2200.00, '2024-02-01', '2024-02-29', 'Paid'),
(8, 2300.00, '2024-02-01', '2024-02-29', 'Paid'),
(6, 1200.00, '2024-02-01', '2024-02-29', 'Paid'),
(5, 4200.00, '2024-02-01', '2024-02-29', 'Paid'),
(3, 3100.00, '2024-02-01', '2024-02-29', 'Paid'),
(2, 2800.00, '2024-02-01', '2024-02-29', 'Paid'),
(4, 5400.00, '2024-02-01', '2024-02-29', 'Paid'),
(7, 2500.00, '2024-02-01', '2024-02-29', 'Paid'),
(10, 3600.00, '2024-02-01', '2024-02-29', 'Paid'),
(12, 4000.00, '2024-02-01', '2024-02-29', 'Paid'),
(11, 2300.00, '2024-02-01', '2024-02-29', 'Paid'),
(14, 1800.00, '2024-02-01', '2024-02-29', 'Paid'),
(15, 5500.00, '2024-02-01', '2024-02-29', 'Paid'),
(13, 8500.00, '2024-02-01', '2024-02-29', 'Paid');

-- January 2024
INSERT INTO Fee (ChildID, Amount, BillingDate, DueDate, [Status]) VALUES
(1, 2500.00, '2024-01-01', '2024-01-31', 'Paid'),
(9, 3800.00, '2024-01-01', '2024-01-31', 'Paid'),
(8, 1900.00, '2024-01-01', '2024-01-31', 'Paid'),
(6, 1200.00, '2024-01-01', '2024-01-31', 'Paid'),
(5, 4200.00, '2024-01-01', '2024-01-31', 'Paid'),
(3, 3100.00, '2024-01-01', '2024-01-31', 'Paid'),
(2, 2800.00, '2024-01-01', '2024-01-31', 'Paid'),
(4, 1500.00, '2024-01-01', '2024-01-31', 'Paid'),
(7, 2000.00, '2024-01-01', '2024-01-31', 'Paid'),
(10, 3500.00, '2024-01-01', '2024-01-31', 'Paid'),
(12, 4000.00, '2024-01-01', '2024-01-31', 'Paid'),
(11, 2300.00, '2024-01-01', '2024-01-31', 'Paid'),
(14, 1800.00, '2024-01-01', '2024-01-31', 'Paid'),
(15, 3200.00, '2024-01-01', '2024-01-31', 'Paid'),
(13, 2700.00, '2024-01-01', '2024-01-31', 'Paid');


INSERT INTO Salary(Amount, StaffID, PaymentDate) VALUES
(5000.00, 2, '2024-05-10'),
(5000.00, 1, '2024-05-10'),
(5000.00, 3, '2024-05-10'),
(4500.00, 5, '2024-05-10'),
(3500.00, 4, '2024-05-10'),
(5000.00, 2, '2024-04-10'),
(5500.00, 1, '2024-04-10'),
(4000.00, 3, '2024-04-10'),
(4700.00, 5, '2024-04-10'),
(3500.00, 4, '2024-04-10'),
(5000.00, 2, '2024-03-10'),
(5200.00, 1, '2024-03-10'),
(4100.00, 3, '2024-03-10'),
(5500.00, 5, '2024-03-10'),
(3500.00, 4, '2024-03-10'),
(5000.00, 2, '2024-02-10'),
(5600.00, 1, '2024-02-10'),
(4000.00, 3, '2024-02-10'),
(4000.00, 5, '2024-02-10'),
(3500.00, 4, '2024-02-10'),
(5900.00, 2, '2024-01-10'),
(5500.00, 1, '2024-01-10'),
(4000.00, 3, '2024-01-10'),
(4500.00, 5, '2024-01-10'),
(5500.00, 4, '2024-01-10');

INSERT INTO Timetable (ClassroomID, StaffID, [Date], StartTime, EndTime) VALUES
(1, 1, '2023-07-01', '08:00:00', '10:00:00'),
(3, 2, '2023-07-01', '09:00:00', '11:00:00'),
(2, 3, '2023-07-01', '10:00:00', '12:00:00');

INSERT INTO Mails (SendingDateTime, SenderEmail, ReceiverEmail, [Message]) VALUES
('2024-05-03 08:30:00', 'bilal.mehmood@nu.edu.pk', 'fatima.ali@nu.edu.pk', 'Reminder: Parent-Teacher meeting on Saturday.'),
('2024-05-03 09:00:00', 'azmar.kashif@nu.edu.pk', 'zara.zafar@nu.edu.pk', 'Welcome back students! Hope you had a great summer break.'),
('2024-05-03 10:00:00', 'bilal.mehmood@nu.edu.pk', 'hamza.ali@nu.edu.pk', 'Welcome back! We hope you had a great break.'),
('2024-05-03 08:00:00', 'azmar.kashif@nu.edu.pk', 'sara.khan@nu.edu.pk', 'Upcoming Birthday: Saad Bilal (5/5/2024) will turn 10 years old on his birthday on May 5th!'),
('2024-05-03 07:00:00', 'azmar.kashif@nu.edu.pk', 'zara.zafar@nu.edu.pk', 'Reminder: Last day for summer camp registration is July 20th.'),
('2024-05-03 09:00:00', 'bilal.mehmood@nu.edu.pk', 'fatima.ali@nu.edu.pk', 'Reminder: School will reopen on August 15th after summer vacation.'),
('2024-05-03 08:00:00', 'azmar.kashif@nu.edu.pk', 'sara.khan@nu.edu.pk', 'Sports day scheduled for September 10th. Get ready for some exciting competitions.'),
('2024-05-03 10:00:00', 'bilal.mehmood@nu.edu.pk', 'hamza.ali@nu.edu.pk', 'Reminder: Projects submission deadline extended to September 15th.'),
('2024-05-03 07:00:00', 'azmar.kashif@nu.edu.pk', 'zara.zafar@nu.edu.pk', 'Annual talent show coming up on October 1st. Start preparing your acts now!'),
('2024-05-03 08:00:00', 'azmar.kashif@nu.edu.pk', 'sara.khan@nu.edu.pk', 'Reminder: Dont forget to submit your artwork for the school art competition by October 15th.');
-- Inserting emails from parents to staff
INSERT INTO Mails (SendingDateTime, SenderEmail, ReceiverEmail, [Message])
VALUES
('2024-05-03 08:30:00', 'asad.iqbal@nu.edu.pk', 'ahmed.hassan@nu.edu.pk', 'Dear Ahmed, I would like to discuss my child Ayans progress in your class. Can we schedule a meeting?'),
('2024-05-03 09:15:00', 'zara.zafar@nu.edu.pk', 'fatima.ali@nu.edu.pk', 'Hi Fatima, I have some concerns regarding my child Rahim. Could you please update me on his recent activities in class?'),
('2024-05-03 10:00:00', 'ali.raza@nu.edu.pk', 'zain.malik@nu.edu.pk', 'Dear Zain, I wanted to inquire about the upcoming school event. Can you provide me with more details?');

-- Inserting emails from parents to admin
INSERT INTO Mails (SendingDateTime, SenderEmail, ReceiverEmail, [Message])
VALUES
('2024-05-03 11:30:00', 'faisal.qureshi@nu.edu.pk', 'bilal.mehmood@nu.edu.pk', 'Dear Bilal, I have a suggestion regarding the schools extracurricular activities. Can we discuss this further?'),
('2024-05-03 12:45:00', 'mehwish.hayat@nu.edu.pk', 'azmar.kashif@nu.edu.pk', 'Hi Azmar, I wanted to bring to your attention an issue regarding the schools transportation service. Can we address this?'),
('2024-05-03 14:00:00', 'mahira.khan@nu.edu.pk', 'bilal.mehmood@nu.edu.pk', 'Dear Bilal, I have some feedback regarding the recent school event. Who should I contact to share my thoughts?');


----------------------------------------------------------------------------------
DROP VIEW MonthlyRevenueView
CREATE VIEW MonthlyRevenueView
AS
SELECT 
    FORMAT(BillingDate, 'MM yyyy') AS Month,
    ISNULL(SUM(p.Amount), 0) + ISNULL(SUM(f.Amount), 0) - ISNULL(SUM(pa.Amount), 0) AS Total_Revenue
FROM 
    Fee f
LEFT JOIN 
    Salary p ON MONTH(p.PaymentDate) = MONTH(f.BillingDate)
              AND YEAR(p.PaymentDate) = YEAR(f.BillingDate)
LEFT JOIN 
    Salary pa ON pa.StaffID = f.ChildID
              AND MONTH(pa.PaymentDate) = MONTH(f.BillingDate)
              AND YEAR(pa.PaymentDate) = YEAR(f.BillingDate)
WHERE 
    f.[Status] = 'Paid'
GROUP BY 
    FORMAT(BillingDate, 'MM yyyy');


select * from MonthlyRevenueView

------------------------------------------------------------------------------------
DECLARE @CurrentDate DATE = '2024-05-03'; -- Current date for testing
DECLARE @EndDate DATE = DATEADD(MONTH, 5, @CurrentDate);

SELECT [Name], DateOfBirth
FROM Child
WHERE 
    (MONTH(DateOfBirth) > MONTH(@CurrentDate) OR (MONTH(DateOfBirth) = MONTH(@CurrentDate) AND DAY(DateOfBirth) >= DAY(@CurrentDate)))
    AND (MONTH(DateOfBirth) <= MONTH(@EndDate) OR (MONTH(DateOfBirth) = MONTH(@EndDate) AND DAY(DateOfBirth) <= DAY(@EndDate)))
ORDER BY MONTH(DateOfBirth), DAY(DateOfBirth);

select * from Enrollment

----------------------------------------------------------------------------------
SELECT 
    c.[Name] AS StudentName,
    c.DateOfBirth,
    p.[Name] AS ParentName,
    p.Email AS ParentEmail,
    AVG(CASE WHEN ar.[Status] = 'Present' THEN 1 ELSE 0 END) AS AvgAttendance,
    AVG(f.Amount) AS AvgFee
FROM 
    Child c
INNER JOIN 
    Parent p ON c.ParentID = p.ParentID
LEFT JOIN 
    Enrollment e ON c.ChildID = e.ChildID
LEFT JOIN 
    AttendanceRecord ar ON e.EnrollmentID = ar.EnrollmentID
LEFT JOIN 
    Fee f ON c.ChildID = f.ChildID
WHERE 
    e.[Status] NOT IN ('Unenrolled', 'Rejected')
GROUP BY 
    c.ChildID, c.[Name], c.DateOfBirth, p.[Name], p.Email;
-------------------------------------------------------------------------------------

SELECT 
    sm.StaffID,
    sm.[Name] AS StaffName,
    sm.Email AS StaffEmail,
    sm.Role AS StaffRole,
    AVG(s.Amount) AS AvgSalary,
    STUFF((SELECT DISTINCT ', ' + c.[Name]
           FROM Classroom c
           WHERE c.StaffID = sm.StaffID
           FOR XML PATH('')), 1, 2, '') AS AssignedClassrooms
FROM 
    StaffMember sm
LEFT JOIN 
    Salary s ON sm.StaffID = s.StaffID
GROUP BY 
    sm.StaffID, sm.[Name], sm.Email, sm.Role;


	Select * from assignedClassrooms

-------------------------------------------------------------------------------
SELECT c.Name AS ChildName, p.Name AS ParentName, f.Amount AS FeeAmount
FROM Child c
JOIN Parent p ON c.ParentID = p.ParentID
LEFT JOIN Fee f ON c.ChildID = f.ChildID
WHERE (f.Status IS NULL OR f.Status <> 'Paid')
AND f.Amount IS NOT NULL;

-------------------------------------------------------------------------------------------------------
SELECT SendingDateTime, RecieverEmail, [Message] FROM Mails WHERE SenderEmail = 'bilal.mehmood@nu.edu.pk'
-----------------------------------------------------------------------------------------------------------
DROP TRIGGER trg_CheckEmailExistsBeforeSend
CREATE TRIGGER trg_CheckEmailExistsBeforeSend
ON Mails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SenderEmail VARCHAR(255)
    DECLARE @ReceiverEmail VARCHAR(255)

    SELECT @SenderEmail = SenderEmail, @ReceiverEmail = ReceiverEmail FROM INSERTED

    IF NOT EXISTS (SELECT 1 FROM Administrator WHERE Email = @SenderEmail)
        AND NOT EXISTS (SELECT 1 FROM StaffMember WHERE Email = @SenderEmail)
        AND NOT EXISTS (SELECT 1 FROM Parent WHERE Email = @SenderEmail)
    BEGIN
        THROW 50001, 'Sender email does not exist in the database.', 1;
    END

    IF NOT EXISTS (SELECT 1 FROM Administrator WHERE Email = @ReceiverEmail)
        AND NOT EXISTS (SELECT 1 FROM StaffMember WHERE Email = @ReceiverEmail)
        AND NOT EXISTS (SELECT 1 FROM Parent WHERE Email = @ReceiverEmail)
    BEGIN
        THROW 50002, 'Receiver email does not exist in the database.', 1;
    END
END

SELECT 
    AVG(AttendancePercentage) AS AverageAttendancePercentage
FROM
    (SELECT 
        StaffID,
        SUM(CASE WHEN [Status] = 'Present' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS AttendancePercentage
    FROM 
        StaffAttendance
    WHERE 
        StaffID = '5'
    GROUP BY 
        StaffID) AS IndividualAttendancePercentage;



SELECT C.ChildID, C.Name AS StudentName
FROM Child C
INNER JOIN Enrollment E ON C.ChildID = E.ChildID
WHERE '2024-05-05' BETWEEN E.StartDate AND E.EndDate
AND E.Status NOT IN ('Waiting', 'UnEnrolled', 'Rejected')

SELECT * FROM AttendanceRecord

SELECT Child.ChildID, Child.Name, Child.DateOfBirth 
                        FROM Child 
                        INNER JOIN Enrollment ON Child.ChildID = Enrollment.ChildID 
                        WHERE Enrollment.ClassroomID IN (SELECT ClassroomID FROM Classroom WHERE [Name] = 'Classroom A')


SELECT 
    Child.ChildID, 
    Child.Name, 
    Child.DateOfBirth,
    ISNULL((SELECT 
                SUM(CASE WHEN AttendanceRecord.Status = 'Present' THEN 1 ELSE 0 END) 
                FROM AttendanceRecord 
                WHERE AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
           ), 0) AS PresentCount,
    ISNULL((SELECT COUNT(*) 
                FROM AttendanceRecord 
                WHERE AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
           ), 0) AS TotalAttendanceCount,
    CASE 
        WHEN ISNULL((SELECT COUNT(*) 
                        FROM AttendanceRecord 
                        WHERE AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                   ), 0) > 0 
        THEN 
            CAST((ISNULL((SELECT 
                            SUM(CASE WHEN AttendanceRecord.Status = 'Present' THEN 1 ELSE 0 END) 
                            FROM AttendanceRecord 
                            WHERE AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                       ), 0) * 100.0 / (SELECT COUNT(*) 
                                            FROM AttendanceRecord 
                                            WHERE AttendanceRecord.EnrollmentID = Enrollment.EnrollmentID
                                       )) AS DECIMAL(5, 2))
        ELSE 0 
    END AS AttendancePercentage
FROM 
    Child 
INNER JOIN 
    Enrollment ON Child.ChildID = Enrollment.ChildID 
WHERE 
    Enrollment.ClassroomID IN (SELECT ClassroomID FROM Classroom WHERE [Name] = 'Classroom A')

	

CREATE VIEW ParentChildrenFees AS
SELECT c.ChildID, c.Name AS ChildName, f.Amount AS FeeAmount, f.BillingDate, f.DueDate, f.Status AS FeeStatus, c.ParentID
FROM Child c
JOIN Fee f ON c.ChildID = f.ChildID
JOIN Parent p ON c.ParentID = p.ParentID;

SELECT * FROM ParentChildrenFees WHERE ParentID = 5;

CREATE VIEW ChildrenTimetable AS
SELECT c.ChildID, c.Name AS ChildName, tt.Date AS TimetableDate, tt.StartTime, tt.EndTime, cl.Name AS ClassroomName, sm.Name AS StaffName
FROM Child c
INNER JOIN Enrollment e ON c.ChildID = e.ChildID
INNER JOIN Timetable tt ON e.ClassroomID = tt.ClassroomID
INNER JOIN Classroom cl ON tt.ClassroomID = cl.ClassroomID
INNER JOIN StaffMember sm ON tt.StaffID = sm.StaffID;

SELECT *
FROM ChildrenTimetable
WHERE ChildID = 3;

Select * from Child