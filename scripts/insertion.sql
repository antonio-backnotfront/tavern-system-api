INSERT INTO [Person] ([Id], [FirstName], [MiddleName], [LastName], [HasBounty])
VALUES 
('AB000101010000CD', 'John', NULL, 'Doe', 0),
('XY999912289999ZS', 'Jane', 'Mary', 'Smith', 1),
('KL054306151234MN', 'Bob', 'Lee', 'Johnson', 0),
('EF999911289999GH', 'Alice', 'Grace', 'Williams', 1);

-- Insert sample data into Race table
INSERT INTO [Race] ([Id], [Name])
VALUES 
    (1, 'Human'),
    (2, 'Elf'),
    (3, 'Dwarf'),
    (4, 'Orc');

-- Insert sample data into ExperienceLevel table
INSERT INTO [ExperienceLevel] ([Id], [Name])
VALUES 
    (1, 'Novice'),
    (2, 'Apprentice'),
    (3, 'Journeyman'),
    (4, 'Master');
