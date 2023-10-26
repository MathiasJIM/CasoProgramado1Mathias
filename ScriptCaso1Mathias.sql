--Tabla consultas

CREATE TABLE [dbo].[Consultas](
    [ID] [bigint] IDENTITY(1,1) NOT NULL,
    [Fecha] [datetime] NOT NULL,
    [Monto] [decimal](10, 2) NOT NULL,
    [DescripcionTipoCurso] [varchar](50) NOT NULL,
    [Nombre] [varchar](255) NOT NULL,
    CONSTRAINT [PK_Consultas] PRIMARY KEY CLUSTERED ([ID] ASC)
) ON [PRIMARY];


--Trigger para la tabla consultas

CREATE TRIGGER trg_InsertEstudiante
ON [dbo].[Estudiantes]
AFTER INSERT
AS
BEGIN
    INSERT INTO [dbo].[Consultas] ([Fecha], [Monto], [DescripcionTipoCurso], [Nombre])
    SELECT
        i.[Fecha],
        i.[Monto],
        t.[DescripcionTipoCurso],
        i.[Nombre]
    FROM
        INSERTED AS i
    JOIN
        [dbo].[TiposCursos] AS t
    ON
        i.[TipoCurso] = t.[TipoCurso];
END;


--Procedimiento Alamcenado


CREATE PROCEDURE RegistrarMatriculaV2
(
    @Nombre VARCHAR(255),
    @Monto DECIMAL(10, 2),
    @TipoCurso INT
)
AS
BEGIN
    DECLARE @Fecha DATETIME
    SET @Fecha = GETDATE()  -- Obtener la fecha y hora actual

    -- Verificar si el tipo de curso es válido (En Laboratorio o En Clase)
    IF @TipoCurso NOT IN (1, 2)
    BEGIN
        THROW 51000, 'Error: Tipo de curso no válido. Debe ser 1 (En Laboratorio) o 2 (En Clase).', 1;
    END

    -- Verificar si ya hay 3 estudiantes matriculados en el tipo de curso
    DECLARE @EstudiantesRegistrados INT
    SELECT @EstudiantesRegistrados = COUNT(*) FROM Estudiantes WHERE TipoCurso = @TipoCurso

    IF @EstudiantesRegistrados >= 3
    BEGIN
        THROW 51001, 'Error: No se pueden matricular más de 3 estudiantes en este tipo de curso.', 1;
    END

    -- Insertar el registro en la tabla Estudiantes
    INSERT INTO Estudiantes (Nombre, Fecha, Monto, TipoCurso)
    VALUES (@Nombre, @Fecha, @Monto, @TipoCurso)
END
