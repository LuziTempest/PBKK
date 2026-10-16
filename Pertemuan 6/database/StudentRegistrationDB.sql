/*
    PBKK Pertemuan 6 - Student Registration Database
    Script dapat dijalankan ulang. Menggunakan Windows Authentication dan
    tidak menyimpan password/kredensial pribadi.
*/

USE master;
GO

IF DB_ID(N'StudentRegistrationDB') IS NULL
BEGIN
    CREATE DATABASE StudentRegistrationDB;
END;
GO

USE StudentRegistrationDB;
GO

IF OBJECT_ID(N'dbo.Programs', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Programs
    (
        ProgramId INT IDENTITY(1,1)
            CONSTRAINT PK_Programs PRIMARY KEY,
        ProgramCode VARCHAR(20) NOT NULL
            CONSTRAINT UQ_Programs_ProgramCode UNIQUE,
        ProgramName VARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL
            CONSTRAINT DF_Programs_IsActive DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL
            CONSTRAINT DF_Programs_CreatedAt DEFAULT SYSDATETIME()
    );
END;
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentId INT IDENTITY(1,1)
            CONSTRAINT PK_Students PRIMARY KEY,
        NIM VARCHAR(20) NOT NULL
            CONSTRAINT UQ_Students_NIM UNIQUE,
        Name VARCHAR(100) NOT NULL,
        ProgramId INT NOT NULL,
        BirthDate DATE NULL,
        Address VARCHAR(250) NULL,
        PhoneNumber VARCHAR(20) NULL,
        CreatedAt DATETIME2 NOT NULL
            CONSTRAINT DF_Students_CreatedAt DEFAULT SYSDATETIME(),
        UpdatedAt DATETIME2 NULL,
        CONSTRAINT FK_Students_Programs
            FOREIGN KEY (ProgramId) REFERENCES dbo.Programs(ProgramId)
    );
END;
GO

INSERT INTO dbo.Programs (ProgramCode, ProgramName)
SELECT source.ProgramCode, source.ProgramName
FROM (VALUES
    ('TI',  'Teknik Informatika'),
    ('SI',  'Sistem Informasi'),
    ('MNJ', 'Manajemen'),
    ('TE',  'Teknik Elektro'),
    ('DKV', 'Desain Komunikasi Visual')
) AS source (ProgramCode, ProgramName)
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.Programs target
    WHERE target.ProgramCode = source.ProgramCode
);
GO

INSERT INTO dbo.Students
    (NIM, Name, ProgramId, BirthDate, Address, PhoneNumber)
SELECT
    source.NIM,
    source.Name,
    program.ProgramId,
    source.BirthDate,
    source.Address,
    source.PhoneNumber
FROM (VALUES
    ('20231001', 'Ahmad Rizki',   'TI',  CAST('2003-03-12' AS DATE), 'Jl. Merdeka No. 123, Surabaya',   '081234567890'),
    ('20231002', 'Siti Aisyah',   'SI',  CAST('2004-07-21' AS DATE), 'Jl. Diponegoro No. 45, Surabaya', '082233445566'),
    ('20231003', 'Budi Santoso',  'TI',  CAST('2003-11-05' AS DATE), 'Jl. Ahmad Yani No. 10, Surabaya', '081355667788'),
    ('20231004', 'Citra Lestari', 'MNJ', CAST('2004-01-18' AS DATE), 'Jl. Darmo No. 20, Surabaya',      '089900112233'),
    ('20231005', 'Dimas Prayoga', 'TE',  CAST('2003-09-27' AS DATE), 'Jl. Raya ITS, Surabaya',          '081288776655'),
    -- Mini assignment: masing-masing satu mahasiswa TI, SI, dan TE.
    ('20231006', 'Eka Putri',     'TI',  CAST('2004-05-14' AS DATE), 'Jl. Manyar, Surabaya',            '081122334455'),
    ('20231007', 'Farhan Akbar',  'SI',  CAST('2003-08-09' AS DATE), 'Jl. Rungkut, Surabaya',           '082211223344'),
    ('20231008', 'Gita Larasati', 'TE',  CAST('2004-02-22' AS DATE), 'Jl. Keputih, Surabaya',           '083344556677')
) AS source (NIM, Name, ProgramCode, BirthDate, Address, PhoneNumber)
INNER JOIN dbo.Programs program
    ON program.ProgramCode = source.ProgramCode
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.Students target
    WHERE target.NIM = source.NIM
);
GO

-- LAB 13 dan LAB 17: data pada masing-masing tabel.
SELECT * FROM dbo.Programs ORDER BY ProgramId;
SELECT * FROM dbo.Students ORDER BY StudentId;

-- LAB 18: JOIN Programs dan Students.
SELECT
    student.StudentId,
    student.NIM,
    student.Name,
    program.ProgramCode,
    program.ProgramName,
    student.BirthDate,
    student.PhoneNumber
FROM dbo.Students student
INNER JOIN dbo.Programs program
    ON student.ProgramId = program.ProgramId
ORDER BY student.Name;

-- Challenge: mahasiswa Teknik Informatika.
SELECT student.NIM, student.Name, program.ProgramName
FROM dbo.Students student
INNER JOIN dbo.Programs program
    ON student.ProgramId = program.ProgramId
WHERE program.ProgramCode = 'TI';

-- Challenge: jumlah seluruh mahasiswa.
SELECT COUNT(*) AS TotalMahasiswa
FROM dbo.Students;

-- Challenge: jumlah mahasiswa per program studi.
SELECT
    program.ProgramName,
    COUNT(student.StudentId) AS JumlahMahasiswa
FROM dbo.Programs program
LEFT JOIN dbo.Students student
    ON student.ProgramId = program.ProgramId
GROUP BY program.ProgramId, program.ProgramName
ORDER BY program.ProgramName;

-- Mini assignment: tampilkan TI, SI, dan TE tambahan beserta data utamanya.
SELECT
    student.NIM,
    student.Name,
    program.ProgramName,
    student.BirthDate,
    student.PhoneNumber
FROM dbo.Students student
INNER JOIN dbo.Programs program
    ON student.ProgramId = program.ProgramId
ORDER BY student.Name;
GO

/*
    Uji constraint berikut sengaja dikomentari agar script utama tetap sukses.
    Jalankan satu per satu bila ingin membuktikan FOREIGN KEY dan UNIQUE.

    -- Harus gagal: ProgramId 99 tidak tersedia.
    INSERT INTO dbo.Students (NIM, Name, ProgramId)
    VALUES ('20231099', 'Test Student', 99);

    -- Harus gagal: NIM 20231001 sudah tersedia.
    INSERT INTO dbo.Students (NIM, Name, ProgramId)
    VALUES ('20231001', 'Mahasiswa Duplikat', 1);
*/
