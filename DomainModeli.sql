IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE SEQUENCE [KategoriIdSirasi] START WITH 1 INCREMENT BY 10 NO CYCLE;

CREATE TABLE [Ayarlar] (
    [Id] int NOT NULL,
    [KilitTarihi] date NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Ayarlar] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Ayarlar_TekSatir] CHECK ([Id] = 1)
);

CREATE TABLE [Depolar] (
    [Id] int NOT NULL IDENTITY,
    [Ad] nvarchar(100) NOT NULL,
    [Aktif] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Depolar] PRIMARY KEY ([Id])
);

CREATE TABLE [Kategoriler] (
    [Id] int NOT NULL,
    [Ad] nvarchar(100) NOT NULL,
    [UstKategoriId] int NULL,
    [Yol] varchar(100) NOT NULL,
    [Seviye] int NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Kategoriler] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Kategoriler_Seviye] CHECK ([Seviye] BETWEEN 1 AND 5),
    CONSTRAINT [FK_Kategoriler_Kategoriler_UstKategoriId] FOREIGN KEY ([UstKategoriId]) REFERENCES [Kategoriler] ([Id])
);

CREATE TABLE [Tedarikciler] (
    [Id] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    [Telefon] varchar(20) NULL,
    [Eposta] varchar(150) NULL,
    [Aktif] bit NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Tedarikciler] PRIMARY KEY ([Id])
);

CREATE TABLE [Urunler] (
    [Id] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    [Barkod] varchar(50) NULL,
    [Birim] nvarchar(10) NOT NULL,
    [SatisFiyati] decimal(18,2) NOT NULL,
    [VarsayilanKritikSeviye] decimal(18,3) NOT NULL,
    [Aktif] bit NOT NULL,
    [KategoriId] int NOT NULL,
    [TedarikciId] int NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Urunler] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Urunler_Kategoriler_KategoriId] FOREIGN KEY ([KategoriId]) REFERENCES [Kategoriler] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Urunler_Tedarikciler_TedarikciId] FOREIGN KEY ([TedarikciId]) REFERENCES [Tedarikciler] ([Id])
);

CREATE TABLE [DepoStoklar] (
    [Id] int NOT NULL IDENTITY,
    [DepoId] int NOT NULL,
    [UrunId] int NOT NULL,
    [Miktar] decimal(18,3) NOT NULL,
    [KritikSeviye] decimal(18,3) NOT NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_DepoStoklar] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_DepoStoklar_KritikSeviye] CHECK ([KritikSeviye] >= 0),
    CONSTRAINT [CK_DepoStoklar_Miktar] CHECK ([Miktar] >= 0),
    CONSTRAINT [FK_DepoStoklar_Depolar_DepoId] FOREIGN KEY ([DepoId]) REFERENCES [Depolar] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DepoStoklar_Urunler_UrunId] FOREIGN KEY ([UrunId]) REFERENCES [Urunler] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [StokHareketleri] (
    [Id] int NOT NULL IDENTITY,
    [Tip] nvarchar(20) NOT NULL,
    [UrunId] int NOT NULL,
    [DepoId] int NOT NULL,
    [Miktar] decimal(18,3) NOT NULL,
    [BirimFiyat] decimal(18,2) NULL,
    [TederikciId] int NULL,
    [TransferId] uniqueidentifier NULL,
    [IslemTarihi] date NOT NULL,
    [OlusturmaTarihi] datetimeoffset NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
    [Aciklama] nvarchar(500) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_StokHareketleri] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_StokHareketleri_Miktar] CHECK ([Miktar] > 0),
    CONSTRAINT [FK_StokHareketleri_Depolar_DepoId] FOREIGN KEY ([DepoId]) REFERENCES [Depolar] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StokHareketleri_Tedarikciler_TederikciId] FOREIGN KEY ([TederikciId]) REFERENCES [Tedarikciler] ([Id]),
    CONSTRAINT [FK_StokHareketleri_Urunler_UrunId] FOREIGN KEY ([UrunId]) REFERENCES [Urunler] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'KilitTarihi') AND [object_id] = OBJECT_ID(N'[Ayarlar]'))
    SET IDENTITY_INSERT [Ayarlar] ON;
INSERT INTO [Ayarlar] ([Id], [KilitTarihi])
VALUES (1, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'KilitTarihi') AND [object_id] = OBJECT_ID(N'[Ayarlar]'))
    SET IDENTITY_INSERT [Ayarlar] OFF;

CREATE UNIQUE INDEX [IX_Depolar_Ad] ON [Depolar] ([Ad]);

CREATE INDEX [IX_DepoStoklar_DepoId_Miktar] ON [DepoStoklar] ([DepoId], [Miktar]) INCLUDE ([KritikSeviye]);

CREATE UNIQUE INDEX [IX_DepoStoklar_DepoId_UrunId] ON [DepoStoklar] ([DepoId], [UrunId]);

CREATE INDEX [IX_DepoStoklar_UrunId] ON [DepoStoklar] ([UrunId]);

CREATE UNIQUE INDEX [IX_Kategoriler_UstKategoriId_Ad] ON [Kategoriler] ([UstKategoriId], [Ad]);

CREATE UNIQUE INDEX [IX_Kategoriler_Yol] ON [Kategoriler] ([Yol]);

CREATE INDEX [IX_StokHareketleri_DepoId] ON [StokHareketleri] ([DepoId]);

CREATE INDEX [IX_StokHareketleri_TederikciId] ON [StokHareketleri] ([TederikciId]);

CREATE INDEX [IX_StokHareketleri_TransferId] ON [StokHareketleri] ([TransferId]) WHERE [TransferId] IS NOT NULL;

CREATE INDEX [IX_StokHareketleri_UrunId_DepoId_IslemTarihi] ON [StokHareketleri] ([UrunId], [DepoId], [IslemTarihi]);

CREATE UNIQUE INDEX [IX_Tedarikciler_Ad] ON [Tedarikciler] ([Ad]);

CREATE INDEX [IX_Urunler_Ad] ON [Urunler] ([Ad]);

CREATE UNIQUE INDEX [IX_Urunler_Barkod] ON [Urunler] ([Barkod]) WHERE [Barkod] IS NOT NULL;

CREATE INDEX [IX_Urunler_KategoriId] ON [Urunler] ([KategoriId]);

CREATE INDEX [IX_Urunler_TedarikciId] ON [Urunler] ([TedarikciId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261027183709_DomainModeli', N'10.0.14');

COMMIT;
GO

