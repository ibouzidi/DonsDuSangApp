## Guide

### Racine du projet :
- DonsDuSang.bak -> backup de la base de données
- donsdusang.loo -> Lopping

## DBB


### Donneur
~~~~
INSERT INTO Donneur (nom, prenom, DateNaissance, email, motdepasse) VALUES
('Martin', 'Elodie', '1990-03-15', 'elodie.martin@example.com', 'hashedpassword123'),
('Dupont', 'Jean', '1985-06-22', 'jean.dupont@example.com', 'hashedpassword456'),
('Nguyen', 'Marie', '1992-11-02', 'marie.nguyen@example.com', 'hashedpassword789'),
('Garcia', 'Lucas', '1988-09-30', 'lucas.garcia@example.com', 'hashedpassword321'),
('Moreau', 'Sophie', '1995-01-08', 'sophie.moreau@example.com', 'hashedpassword654');
~~~~

### Questionnaire
~~~~
INSERT INTO Questionnaire (DateCreation, AccordEnseignement, AccordNonTherapeutique, idDonneur) VALUES
('2024-09-01 10:00:00', 1, 0, 1), -- Elodie Martin
('2024-09-01 10:30:00', 0, 1, 2), -- Jean Dupont
('2024-09-01 11:00:00', 1, 1, 3), -- Marie Nguyen
('2024-09-01 11:30:00', 1, 0, 4), -- Lucas Garcia
('2024-09-01 12:00:00', 0, 0, 5); -- Sophie Moreau
~~~~

### Questions
~~~~
INSERT INTO Question (Numero, Enonce, Categorie, estCritique) VALUES
(1, 'Vous sentez-vous en forme pour donner votre sang ?', 'État de santé', 1),
(2, 'Avez-vous consulté un médecin dans les 4 derniers mois ?', 'État de santé', 1),
(3, 'Avez-vous réalisé des examens de santé (bilan biologique, radiographies...) dans les 4 derniers mois ?', 'État de santé', 0),
(4, 'Avez-vous pris (ou prenez-vous actuellement) des médicaments (même à titre préventif) ?', 'État de santé', 0),
(5, 'Avez-vous pris un médicament pour prévenir l''infection à VIH comme la prophylaxie pré-exposition (PrEP) ou la prophylaxie post-exposition (PEP) dans les 4 derniers mois ?', 'État de santé', 1),
(6, 'Avez-vous eu une injection de désensibilisation pour allergie dans les 15 derniers jours ?', 'État de santé', 0),
(7, 'Avez-vous été vacciné(e) contre l''hépatite B ?', 'Vaccinations', 0),
(8, 'Avez-vous été vacciné(e) contre d''autres maladies dans le dernier mois ?', 'Vaccinations', 0),
(9, 'Avez-vous été vacciné(e) contre le tétanos dans les 2 dernières années (rappel) ?', 'Vaccinations', 0),
(10, 'Avez-vous eu récemment des saignements (du nez, des hémorroïdes, des règles abondantes) ?', 'Symptômes', 1),
(11, 'Avez-vous ressenti dans les jours ou semaines qui précèdent une douleur thoracique ou un essoufflement anormal à la suite d''un effort ?', 'Symptômes', 1),
(12, 'Avez-vous été traité(e) dans les 3 dernières années pour un psoriasis important ?', 'Antécédents médicaux', 0),
(13, 'Avez-vous une maladie qui nécessite un suivi médical régulier ?', 'Antécédents médicaux', 1),
(14, 'Avez-vous prévu une activité avec efforts physiques (sportive, professionnelle...) juste après votre don ?', 'Activités', 0),
(15, 'Avez-vous déjà consulté un cardiologue ?', 'Antécédents médicaux', 1),
(16, 'Avez-vous déjà été opéré(e) ou hospitalisé(e) ?', 'Antécédents médicaux', 1),
(17, 'Avez-vous de l''asthme, une réaction allergique importante, notamment lors d''un soin médical ?', 'Antécédents médicaux', 1),
(18, 'Avez-vous une maladie de la coagulation du sang ?', 'Antécédents médicaux', 1),
(19, 'Avez-vous une anémie, un manque de globules rouges, un traitement pour compenser un manque de fer ?', 'Antécédents médicaux', 1),
(20, 'Avez-vous eu un diagnostic de cancer (y compris mélanome, leucémie, lymphome...) ?', 'Antécédents médicaux', 1),
(21, 'Avez-vous eu un accident vasculaire cérébral, un accident ischémique transitoire, des crises d''épilepsie, des convulsions (en dehors de l''enfance), des syncopes répétées ?', 'Antécédents médicaux', 1);
~~~~

### BDD CREATION + TABLE
~~~~

USE [master]
GO
/****** Object:  Database [DonsDuSang]    Script Date: 04/09/2024 23:22:19 ******/
CREATE DATABASE [DonsDuSang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DonsDuSang', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DonsDuSang.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DonsDuSang_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DonsDuSang_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DonsDuSang] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DonsDuSang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DonsDuSang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DonsDuSang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DonsDuSang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DonsDuSang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DonsDuSang] SET ARITHABORT OFF 
GO
ALTER DATABASE [DonsDuSang] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DonsDuSang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DonsDuSang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DonsDuSang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DonsDuSang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DonsDuSang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DonsDuSang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DonsDuSang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DonsDuSang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DonsDuSang] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DonsDuSang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DonsDuSang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DonsDuSang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DonsDuSang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DonsDuSang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DonsDuSang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DonsDuSang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DonsDuSang] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DonsDuSang] SET  MULTI_USER 
GO
ALTER DATABASE [DonsDuSang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DonsDuSang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DonsDuSang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DonsDuSang] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DonsDuSang] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DonsDuSang] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DonsDuSang] SET QUERY_STORE = ON
GO
ALTER DATABASE [DonsDuSang] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DonsDuSang]
GO
/****** Object:  Table [dbo].[Donneur]    Script Date: 04/09/2024 23:22:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donneur](
	[idDonneur] [smallint] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](50) NULL,
	[prenom] [varchar](50) NULL,
	[DateNaissance] [date] NULL,
	[email] [varchar](100) NULL,
	[motdepasse] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDonneur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 04/09/2024 23:22:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[idQuestion] [smallint] IDENTITY(1,1) NOT NULL,
	[Numero] [tinyint] NULL,
	[Enonce] [varchar](500) NULL,
	[Categorie] [varchar](100) NULL,
	[estCritique] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idQuestion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaire]    Script Date: 04/09/2024 23:22:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire](
	[idQuestionnaire] [smallint] IDENTITY(1,1) NOT NULL,
	[DateCreation] [datetime2](7) NULL,
	[AccordEnseignement] [bit] NULL,
	[AccordNonTherapeutique] [bit] NULL,
	[idDonneur] [smallint] NOT NULL,
	[BesoinEntretient] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idQuestionnaire] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reponse]    Script Date: 04/09/2024 23:22:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reponse](
	[idReponse] [smallint] IDENTITY(1,1) NOT NULL,
	[Reponse] [varchar](20) NULL,
	[ComplementTextuel] [varchar](200) NULL,
	[EstDisqualifié] [bit] NULL,
	[idDonneur] [smallint] NOT NULL,
	[idQuestion] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idReponse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questionnaire]  WITH CHECK ADD FOREIGN KEY([idDonneur])
REFERENCES [dbo].[Donneur] ([idDonneur])
GO
ALTER TABLE [dbo].[Reponse]  WITH CHECK ADD FOREIGN KEY([idDonneur])
REFERENCES [dbo].[Donneur] ([idDonneur])
GO
ALTER TABLE [dbo].[Reponse]  WITH CHECK ADD FOREIGN KEY([idQuestion])
REFERENCES [dbo].[Question] ([idQuestion])
GO
/****** Object:  StoredProcedure [dbo].[AddRandomResponses]    Script Date: 04/09/2024 23:22:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddRandomResponses]
    @idDonneur SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @totalQuestions INT;
    DECLARE @i INT = 1;
    DECLARE @randomReponse VARCHAR(20);
    DECLARE @randomComplement VARCHAR(200);

    -- Get the total number of questions
    SELECT @totalQuestions = COUNT(*) FROM Question;

    WHILE @i <= @totalQuestions
    BEGIN
        -- Randomly select a response: 'Oui', 'Non', or 'Je ne sais pas'
        DECLARE @rand FLOAT = RAND();
        IF @rand < 0.33
            SET @randomReponse = 'Oui';
        ELSE IF @rand < 0.66
            SET @randomReponse = 'Non';
        ELSE
            SET @randomReponse = 'Je ne sais pas';

        -- Optionally, add a random complement for 'Oui' responses
        IF @randomReponse = 'Oui' AND RAND() < 0.5
            SET @randomComplement = 'Complément aléatoire pour réponse Oui';
        ELSE
            SET @randomComplement = NULL;

        -- Insert the random response into the Reponse table
        INSERT INTO Reponse (Reponse, ComplementTextuel, idDonneur, idQuestion)
        VALUES (
            @randomReponse,
            @randomComplement,
            @idDonneur,
            @i
        );

        -- Increment the question counter
        SET @i = @i + 1;
    END

    SELECT CONCAT(@totalQuestions, ' réponses ajoutées pour le donneur ID ', @idDonneur) AS Resultat;

END
GO
USE [master]
GO
ALTER DATABASE [DonsDuSang] SET  READ_WRITE 
GO
~~~~



### Espace Medecin

- *Mot de passe*: `123`