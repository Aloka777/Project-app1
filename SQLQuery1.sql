-- Create the 4 main Faculties
INSERT INTO Faculties (Name) VALUES ('FOC'), ('FOB'), ('FOS'), ('FOE');

-- Link Research Areas to FOC (FacultyId 1)
INSERT INTO ResearchAreas (Name, FacultyId) VALUES 
('Information Technology (Cyber Security)', 1),
('Technology Management', 1),
('Computer Science', 1),
('Software Engineering', 1),
('Artificial Intelligence', 1);

-- Link Research Areas to FOE (FacultyId 4)
INSERT INTO ResearchAreas (Name, FacultyId) VALUES 
('Mechatronic Engineering', 4),
('Electrical & Electronic Engineering', 4);
