INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Employee', GETDATE(), GETDATE()
from AppUser; 

-- QA
INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Technical Writer', GETDATE(), GETDATE() 
FROM AppUser WHERE Department = 'QualityAssurance';

-- Developer
INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Developer', GETDATE(), GETDATE() 
FROM AppUser WHERE Department = 'Developer';


-- Team Leader
INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Team Leader', GETDATE(), GETDATE() 
FROM AppUser WHERE UserName in ('adam', 'shovi', 'patra', 'dery', 'dewa', 'faizul');

-- Manager
INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Manager', GETDATE(), GETDATE() 
FROM AppUser WHERE UserName in ('RIALDI', 'skwary', 'aminullah', 'henky');

-- Administrative
INSERT INTO UserAuthRole(UserAuthId, Role, CreatedDate, ModifiedDate) 
SELECT ID, 'Administrative', GETDATE(), GETDATE() 
FROM AppUser WHERE UserName in ('anggraini', 'jajat');

SELECT * FROM Client

INSERT INTO Client ( Code, Name,  PhoneNo, Email, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES('SKKM', 'SKK Migas', '-', '-', '-', 1, GETDATE(), 'rialdi@ptgdi.com', GETDATE(), 'rialdi@ptgdi.com');

INSERT INTO Client ( Code, Name,  PhoneNo, Email, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES('POVN', 'Premier Oil Vietnam', '-', '-', '-', 1, GETDATE(), 'rialdi@ptgdi.com', GETDATE(), 'rialdi@ptgdi.com');

INSERT INTO Client ( Code, Name,  PhoneNo, Email, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES('POUK', 'Premier Oil Ukraine', '-', '-', '-', 1, GETDATE(), 'rialdi@ptgdi.com', GETDATE(), 'rialdi@ptgdi.com');

INSERT INTO Client ( Code, Name,  PhoneNo, Email, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES('Pertamina', 'Pertamina', '-', '-', '-', 1, GETDATE(), 'rialdi@ptgdi.com', GETDATE(), 'rialdi@ptgdi.com');

INSERT INTO Client ( Code, Name,  PhoneNo, Email, Description, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES('RPN', 'PT. Roda Pembina Nusantara', '-', '-', '-', 1, GETDATE(), 'rialdi@ptgdi.com', GETDATE(), 'rialdi@ptgdi.com');