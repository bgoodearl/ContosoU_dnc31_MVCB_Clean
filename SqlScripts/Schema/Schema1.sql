USE ContosoUniversity_dev; --Database used for Migrations
--USE ContosoUniversity; --Database used for web app
GO

Declare @UseMigrationHistory bit = 0;
--Declare @UseMigrationHistory bit = 1;
DECLARE @CurrentMigration [nvarchar](max)

If ISNULL(@UseMigrationHistory,0) = 1 Begin

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'ContosoUniversity.DAL.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

End;

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '202202242101280_Schema1'
BEGIN
    CREATE TABLE [dbo].[Course] (
        [CourseID] [int] NOT NULL,
        [Title] [nvarchar](50),
        [Credits] [int] NOT NULL,
        [DepartmentID] [int] NOT NULL CONSTRAINT [DF_Course_DepartmentID] DEFAULT 1,
        CONSTRAINT [PK_dbo.Course] PRIMARY KEY ([CourseID])
    )
    CREATE INDEX [IX_DepartmentID] ON [dbo].[Course]([DepartmentID])
    CREATE TABLE [dbo].[Department] (
        [DepartmentID] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](50),
        [Budget] [money] NOT NULL,
        [StartDate] [datetime] NOT NULL,
        [InstructorID] [int],
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_dbo.Department] PRIMARY KEY ([DepartmentID])
    )
    CREATE INDEX [IX_InstructorID] ON [dbo].[Department]([InstructorID])
    CREATE TABLE [dbo].[Instructor] (
        [ID] [int] NOT NULL IDENTITY,
        [LastName] [nvarchar](50),
        [FirstName] [nvarchar](50),
        [HireDate] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Instructor] PRIMARY KEY ([ID])
    )
    CREATE TABLE [dbo].[OfficeAssignment] (
        [InstructorID] [int] NOT NULL,
        [Location] [nvarchar](50),
        CONSTRAINT [PK_dbo.OfficeAssignment] PRIMARY KEY ([InstructorID])
    )
    CREATE INDEX [IX_InstructorID] ON [dbo].[OfficeAssignment]([InstructorID])
    CREATE TABLE [dbo].[Enrollment] (
        [EnrollmentID] [int] NOT NULL IDENTITY,
        [CourseID] [int] NOT NULL,
        [StudentID] [int] NOT NULL,
        [Grade] [int],
        CONSTRAINT [PK_dbo.Enrollment] PRIMARY KEY ([EnrollmentID])
    )
    CREATE INDEX [IX_CourseID] ON [dbo].[Enrollment]([CourseID])
    CREATE INDEX [IX_StudentID] ON [dbo].[Enrollment]([StudentID])
    CREATE TABLE [dbo].[Student] (
        [ID] [int] NOT NULL IDENTITY,
        [LastName] [nvarchar](50) NOT NULL,
        [FirstMidName] [nvarchar](50) NOT NULL,
        [EnrollmentDate] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Student] PRIMARY KEY ([ID])
    )
    CREATE TABLE [dbo].[CourseInstructor] (
        [CourseID] [int] NOT NULL,
        [InstructorID] [int] NOT NULL,
        CONSTRAINT [PK_dbo.CourseInstructor] PRIMARY KEY ([CourseID], [InstructorID])
    )
    CREATE INDEX [IX_CourseID] ON [dbo].[CourseInstructor]([CourseID])
    CREATE INDEX [IX_InstructorID] ON [dbo].[CourseInstructor]([InstructorID])
    ALTER TABLE [dbo].[Course] ADD CONSTRAINT [FK_dbo.Course_dbo.Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Department] ADD CONSTRAINT [FK_dbo.Department_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [dbo].[Instructor] ([ID])
    ALTER TABLE [dbo].[OfficeAssignment] ADD CONSTRAINT [FK_dbo.OfficeAssignment_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [dbo].[Instructor] ([ID])
    ALTER TABLE [dbo].[Enrollment] ADD CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Enrollment] ADD CONSTRAINT [FK_dbo.Enrollment_dbo.Student_StudentID] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Student] ([ID]) ON DELETE CASCADE
    ALTER TABLE [dbo].[CourseInstructor] ADD CONSTRAINT [FK_dbo.CourseInstructor_dbo.Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID]) ON DELETE CASCADE
    ALTER TABLE [dbo].[CourseInstructor] ADD CONSTRAINT [FK_dbo.CourseInstructor_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [dbo].[Instructor] ([ID]) ON DELETE CASCADE

If ISNULL(@UseMigrationHistory,0) = 1 Begin
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'202202242101280_Schema1', N'ContosoUniversity.DAL.Migrations.Configuration',  0x1F8B080000000000000AE45DDB6EE4B8117D0F907F10F418CC76DB9E049835DABBF0D8E34D637D19B8BD8BBC19B4C46E0BD1A523B1BD6E04F9B27DC827E5175252EBC2BB4849EE9667B1C0AC2D91C562D561B18AAC92FFF7FB7F673FBE46A1F382D32C48E233F77872E43A38F6123F885767EE862CBFFBE4FEF8C39FFF34FBE247AFCEAF55BB8F793BE8196767EE3321EBD3E934F39E7184B24914786992254B32F192688AFC647A7274F4FDF4F8788A81840BB41C6776BF894910E1E217F8F522893DBC261B14DE243E0EB3F239BC5914549D5B14E16C8D3C7CE6425B02F47F8983826BB29D5C9E5FBBCE791820606681C3A5EBA0384E0822C0EAE92F195E903489578B353C40E1C3768DA1DD1285192EA770DA34379DCDD1493E9B69D3B122E56D32924496048F3F96E299F2DD3B097927E352805F40D0649BCFBA10622EBF4D9A4F9D1FEAF4224CF3663209EFB432D975FDE0A81A7CA8010238CAFF83A69B906C527C16E30D4951F8C1F9BA790A03EF67BC7D48FE89E3B378138634BFC031BC631EC0A3AF69B2C629D9DEE325338BF9A5EB4CD9DE53BE7BDD59E8B99BED3C261F4F5CE71618414F21AEA1414966419214FF84639C2282FDAF88109C82666F93180BE373A33D0424C4D55000445856AE73835EAF71BC22CF67EEDF601D5D05AFD8AF1E94A3836C6111421F926E5A07B948B11F90AC6D467A2297788D5212E198B4CB86A3748B5E8255212A254DD7B9C761D1247B0ED6BB753A695E3FEEB40253B84A93E83EC9C712DF3E3EA0748581D643A26CB280FF7B162C7E89D3240C73129994C7E67D3904CDA2F052E0506C61CBE03CCE00031E4050CE60499669D67028BEAD19A8589434A96641F3389B36A6446B6068A57730324DF751181A7659D81A1B9B45D56E70E63E100229B42DE5FCDFB7B6399F37FE0EE8C52897D80B2214BACED7147E2A7D894FAEB3F0503E437B6BB42020B74B987D3D02FCFC002E8335A506D7820AF41DEF93DF4A9455DD3E07314A41FAB9BBB24953F093B6391ECFDC429A8C8C3FC945DC41E91749B4DEC003738B71EE47411CC0AC11506C33BC5C63A9F965DA08F643D9506645747CD77BC0105B85864B7E37E964E51A5875B2724DF75158B92EB66D9F16ED1A65641F56ED2A48337213F8FB18EBEF418ABB59B84E2BC8DA4FE05D198D2B61CADFDD721978F83CCB8255ACF40A9B011EC5F60DC79A66C2E2D7B5ED650544063BD8029EC8382C02B36D5ADB06F5A66BB97D5F275E19090FBC1A0D1CEEB78227BFB24CA0DC099E4DF4D109984DF75140B261A70B24D9DEFBDAB84C4F1BDABCE14D3E5E5F323FA5C86FB6B51CCCE513BBFD65883899B7D0EA48DA94B7524652E6CA778F4CB0DFB027792DAC51599B5E6BB3E6B7C3C22CFB8E6255FE715CC75BBBD536AC2F69397803D3815DCCB6F3B20E4B8DB705BAE568B6D43611B5D04A1B37CFAE42B46A8EFE3B2CBB82D2F08B0E54EDE334DC0232E845C12AE406474F38AD227CD7F915851BF8E948D01CD3F073DDF058DFF0A26E78A26F785937FCA86F785537FCABA8B69D82E887E0F2245E506842F0C344478B1DF74BEC3B164141B3FDD1AEDE0DA82A58837240A13271990D526F1BCD20A297C80E75349988A3817DC570C094DF5801DCF213952026A2310E622F58A3D07CF61C09438B9E2BAD1E8C7F03272B18001C1373F11871A10940727EEA61B9BDA74D72B32905353D0295C7642A64B49F9935B0A08FC95940FC4583BDD6C33623740F07B9367EF680B736A1BF3FB055C7380620108E3B4D0076CCCB6476175FE21013EC9C7BBB5BEB0B9479B2D0006CB76FC19604955574D086F9BE70E424638202DD5D4F5748729218828D3D80518CE6544AD784768DCEE97308735BA7B95D6D83D3A008EF844425F32608506537588150A999BE1CEC017FB2284205136D48D100A58EB7F7620BB5470ADD164627186A84B387AD59230593D1A963AF83A0507265A2D2B72ECFC27EEFD3D2B5F2F438DACAC9EF22E93CB20571D5311C64DF254978F9943FC7AFB2B32A48AF2BE3E6AC3CA9E06792D35D60C2DF5035A13B279BA99E40B339CA88D03E4F0B21E6224C20444BB485101FE4C8C88931600B51E6F84420479B8D1642E51A9251A92D22478282884C5C926B0FAA87C12D09BF94EDC2F77A829C0A050B6117B05364250AE52D3F2B2203F1A9332E44D999859D768127353D660569A4D61A6A9A69A29FB06A7BA115933460320D99FA89868F74286A35EFBD0522B95C11E5D1E2B31B7AED14FF8C0DD24843EDA7BF8930A427CCA2385A5D48C7D489A466D158538D3C74BE9F99783B484596DE210AA5CDA331F569DA15DB4669302352DD02D4BE4BFD6E36DD550F940F66534599C1EC06ADD770F84E951D944F9C455973F0DDC23E133FDAD1987A8C8C794FAB1E09A68D56987B9B8BCEC7C54D12DCDDA027942FFD0B3F129AF19E9AC229A846639D31517195A750B5CF7FAEFA48CA2F2AE74D74EA4B025730B71CE9C534B1801EB12374CD734751AABC4FBF48C24D14B745AB6A4A65663E4DA67C644EA34EBC6798A91E9AD3614F7C6862FAB3A01CD49C8485384950A610C4B2D830420EBD51F6470FE5B5DB2348D7797879AB28EE2E79694ABB27E614AA846A9A46F5CC9C0A95374D13A21E9BD3620FC56972FAE37235453AA59AA6473F1F0DC6F59BA42DC6A980D21EE3BACE4AEDF13AB3D254933541D3689E9A5362D320686AC51B5B724DD62C4DAA793A1AF88851647F10B585D206506A27B13F73D0E4763220AB9F8E4697FA18C2568BD4E98DBDFE749D557266531F6959EB522AF7E3865167BCEC86A53CFA55D32A337D683AD204C70322A90E20FBC3A83ABEB3C790B2E73BDF4BEA37E614F93C39F9E238D4D6C206BDB2E08D0DF8CDC234B68F3C22537B1C79582FB506B283025162839893E2EAADA2D1893FE52D9419FE5BD24554DCF1C718A2D285D30CBE490DB9FA54833BBD98952709265F52E08E16764DF222CEE425809444B014DB8CE0689237982CFE155E844171DE5F35B84171B0C419D92533BA2747C727DC9718C6F355846996F9A1D1A7110EFA3902B8ACB44EF6653E3210BFA0D47B46A99064DC10EDF04D812E6CC98A9F0B3AC22DE21C2E725FCFDC7F171D4F9DF93F1EE9BE1F9CBB14C076EA1C39FF6961A24B91FA288AC20BB9B466BA5B2A80CE3C1F08166CD97794C478DBBFB6DB879FC950B5DDA6089B537D550833918858269E26BF951FD451AAB429EA1E06CECD64F65FC2F126D0E5AB3606822F75E430285DBE96D710D1DDEB4C4751D139E452EB540CDA497B5DAA274751ADF8260B4DEA7F18E8B4EAD74B9F425DA3E9E875C75EC333F5909C87D30337D2F0FA8F64893B97C70D455B5EFD36B451D647CAFD028737B0E0075DE907D83E547A1CBAD26C808A3231A3EEC0F563FB480E36B8CCF836AAC2ACA122D5A82CFBAC7341D93B80873629E0DBA9E03A6CC956954DB8FFFAACFD166429F393DE79F595B1FEDE083F7422FA3ECB5A2CF439702DD5375A3D75C052A94361487D7F3AF85E66099FF1D53D49EFF346648784504CE444ED6EBD539BD476513B46CB647813DB2FCEFA661136DFAB03DE095F4DF33D614C5EB82866CD9B94256AAA127757E37086F49480E677670ED6158B2D058BB2313A1534B6D433CAC6E951EF6854EE281BB34751644B4DA46C34FB9A495DC9A46C04553DA51C6C5A75894DD400D4A86E0C959C8A121F6DBD900A62F20CD6D15767F61386683FF8BA8791575C1AB22D7528C552A11156531A33AB8C7BE409BEE3AE959415461E7CC29A9A45936A4845DE626F5DAB4CBE3CE3F24DC450155C1A89419E1E3998497B2B7158D4828A5992E084527F950ADCE17C83A949E47FA32AC64500D110ADDACCE36552F9C21C475513EE3EEC061304F791E83C25C11279045E7B18B6B4FCE3A0E56713BF444FD89FC7771B02A94230651C3D85CCD74E736F5A377E51F0CAF23CBB5B17DFEB1C620AC066905FA9DEC59F3741E8D77C5F49AEE214247237BDBCAACE75996FA47805732C29897F604945A8145F1D5D3CE0680D9F26C5D95DBC402F40C49E37F8B2C9355E216F5B25BBAA89B42B8215FBEC3240AB14455949A3E90FBF0286E1EFA4FDF07F000000FFFF030013AD1F469C6D0000 , N'6.4.4')
End;
END
GO
