CREATE DATABASE HotelManagement;
GO

CREATE TABLE [dbo].[Booking] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [CustomerId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [RoomId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [CheckInDate] datetime2(7)  NULL,
  [CheckOutDate] datetime2(7)  NULL,
  [NumberOfGuests] int  NULL,
  [TotalPrice] decimal(10,2)  NULL,
  [DepositAmount] decimal(10,2)  NULL,
  [DepositStatus] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL,
  [Status] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL,
  [IsExtend] bit  NULL
)


-- ----------------------------
-- Records of Booking
-- ----------------------------
INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'7b1e4d8f-cff3-43b7-bb43-3e1a90d6b4c2', N'c1d2e3f4-a5b6-7c8d-9e0f-1g2h3i4j5k6l', N'9f8b64a4-718a-4ba7-86c2-747d3b5b5d2a', N'2024-06-21 00:00:00.0000000', N'2024-06-30 00:00:00.0000000', N'0', N'.00', N'.00', N'Unpaid', N'Pending')
GO

INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'a2b3c4d5-e6f7-8a9b-0c1d-2e3f4a5b6c7d', N'd2e3f4g5-a6b7-8c9d-0e1f-2g3h4i5j6k7l', N'af94b6c1-8f4b-49a5-8a34-d9d74cf27c6e', N'2024-06-10 14:00:00.0000000', N'2024-06-15 12:00:00.0000000', N'4', N'750.00', N'200.00', N'Unpaid', N'Confirmed')
GO

INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'b3c4d5e6-f7a8-9b0c-1d2e-3f4a5b6c7d8e', N'e3f4g5h6-a7b8-9c0d-1e2f-3g4h5i6j7k8l', N'be546d52-034f-4c95-a925-c5bbef8e4dc3', N'2024-06-20 14:00:00.0000000', N'2024-06-25 12:00:00.0000000', N'3', N'600.00', N'150.00', N'Paid', N'Confirmed')
GO

INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'c4d5e6f7-a9b0-0c1d-2e3f-4a5b6c7d8e9f', N'f4g5h6i7-a8b9-0c1d-2e3f-4g5h6i7j8k9l', N'c56a4180-65aa-42ec-a945-5fd21dec0538A', N'2024-07-01 14:00:00.0000000', N'2024-07-05 12:00:00.0000000', N'2', N'500.00', N'100.00', N'Unpaid', N'Cancelled')
GO

INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'd5e6f7a8-b0c1-1d2e-3f4a-5b6c7d8e9f0a', N'g5h6i7j8-a9b0-1c2d-3e4f-5g6h7i8j9k0l', N'f47ac10b-58cc-4372-a567-0e02b2c3d479', N'2024-07-10 14:00:00.0000000', N'2024-07-15 12:00:00.0000000', N'4', N'750.00', N'200.00', N'Paid', N'Confirmed')
GO

INSERT INTO [dbo].[Booking] ([Id], [CustomerId], [RoomId], [CheckInDate], [CheckOutDate], [NumberOfGuests], [TotalPrice], [DepositAmount], [DepositStatus], [Status]) VALUES (N'e1c1d8a6-5a58-4a39-b6e8-7f1b7b9d8e65', N'c1d2e3f4-a5b6-7c8d-9e0f-1g2h3i4j5k6l', N'9f8b64a4-718a-4ba7-86c2-747d3b5b5d2a', N'2024-06-01 14:00:00.0000000', N'2024-06-05 12:00:00.0000000', N'2', N'500.00', N'100.00', N'Paid', N'Confirmed')
GO


CREATE TABLE [dbo].[BookingDetail] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [BookingId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [ServiceId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Quantity] int  NULL,
  [Price] decimal(18,2)  NULL,
  [Status] nvarchar(50) COLLATE Vietnamese_CI_AS  NULL
)
GO


-- ----------------------------
-- Records of BookingDetail
-- ----------------------------
INSERT INTO [dbo].[BookingDetail] ([Id], [BookingId], [ServiceId], [Quantity], [Price], [Status]) VALUES (N'f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c', N'a2b3c4d5-e6f7-8a9b-0c1d-2e3f4a5b6c7d', N'a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d', N'1', N'123000.00', N'none')
GO



CREATE TABLE [dbo].[Customer] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [Name] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Phone] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Password] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Role] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL
)
GO

-- ----------------------------
-- Records of Customer
-- ----------------------------
INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'c1d2e3f4-a5b6-7c8d-9e0f-1g2h3i4j5k6l', N'John Doe', N'123-456-7890', N'john.doe@example.com', N'827ccb0eea8a706c4c34a16891f84e7b')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'd2e3f4g5-a6b7-8c9d-0e1f-2g3h4i5j6k7l', N'Jane Smith', N'234-567-8901', N'jane.smith@example.com', N'827ccb0eea8a706c4c34a16891f84e7b')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'ddc79df0-203d-440a-98aa-2546974d04a4', N'Sammy', N'0384659245', N'sammy@gm.com', N'81dc9bdb52d04dc20036dbd8313ed055')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'e3f4g5h6-a7b8-9c0d-1e2f-3g4h5i6j7k8l', N'Alice Johnson', N'345-678-9012', N'alice.johnson@example.com', N'827ccb0eea8a706c4c34a16891f84e7b')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'f4g5h6i7-a8b9-0c1d-2e3f-4g5h6i7j8k9l', N'Bob Brown', N'456-789-0123', N'bob.brown@example.com', N'827ccb0eea8a706c4c34a16891f84e7b')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'g5h6i7j8-a9b0-1c2d-3e4f-5g6h7i8j9k0l', N'Charlie Black', N'567-890-1234', N'charlie.black@example.com', N'827ccb0eea8a706c4c34a16891f84e7b')
GO

INSERT INTO [dbo].[Customer] ([Id], [Name], [Phone], [Email], [Password]) VALUES (N'string', N'string', N'string', N'string', N'827ccb0eea8a706c4c34a16891f84e7b')
GO


CREATE TABLE [dbo].[Invoice] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [CustomerId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [BookingId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [TotalAmount] decimal(18,2)  NULL,
  [PaymentMethod] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [PaymentDate] datetime2(7)  NULL,
  [CreatedDate] datetime2(7)  NULL,
  [Status] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL
)


CREATE TABLE [dbo].[Manager] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [Name] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Phone] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Password] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Role] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL
)
GO


-- ----------------------------
-- Records of Manager
-- ----------------------------
INSERT INTO [dbo].[Manager] ([Id], [Name], [Phone], [Email], [Password], [Role]) VALUES (N'4947e71d-b470-47c4-91b5-3cc82daf742d', N'Admin', N'0384659245', N'admin@gmail.com.vn', N'827ccb0eea8a706c4c34a16891f84e7b', N'Admin')
GO



CREATE TABLE [dbo].[Review] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [CustomerId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [RoomId] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Content] nvarchar(255) COLLATE Vietnamese_CI_AS  NULL,
  [Rating] int  NULL
)


CREATE TABLE [dbo].[Room] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [Name] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Type] nvarchar(50) COLLATE Vietnamese_CI_AS  NULL,
  [Price] decimal(18,2)  NULL,
  [Area] decimal(18,2)  NULL,
  [Description] nvarchar(255) COLLATE Vietnamese_CI_AS  NULL,
  [Status] nvarchar(20) COLLATE Vietnamese_CI_AS  NULL
)
GO

-- ----------------------------
-- Records of Room
-- ----------------------------
INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'9f8b64a4-718a-4ba7-86c2-747d3b5b5d2a', N'Sample Name 2', N'Sample Type 2', N'102.00', N'52.00', N'Sample Description 2', N'Active')
GO

INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'af94b6c1-8f4b-49a5-8a34-d9d74cf27c6e', N'Sample Name 3', N'Sample Type 3', N'103.00', N'53.00', N'Sample Description 3', N'Active')
GO

INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'be546d52-034f-4c95-a925-c5bbef8e4dc3', N'Sample Name 4', N'Sample Type 4', N'104.00', N'54.00', N'Sample Description 4', N'Active')
GO

INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'c56a4180-65aa-42ec-a945-5fd21dec0538A', N'Sample Name 1', N'Sample Type 1', N'101.00', N'51.00', N'Sample Description 1', N'Active')
GO

INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'f47ac10b-58cc-4372-a567-0e02b2c3d479', N'Sample Name 0', N'Sample Type 0', N'100.00', N'50.00', N'Sample Description 0', N'Active')
GO

INSERT INTO [dbo].[Room] ([Id], [Name], [Type], [Price], [Area], [Description], [Status]) VALUES (N'string', N'string', N'string', N'.00', N'.00', N'string', N'string')
GO


-- ----------------------------
-- Table structure for Service
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Service]') AND type IN ('U'))
	DROP TABLE [dbo].[Service]
GO

CREATE TABLE [dbo].[Service] (
  [Id] nvarchar(100) COLLATE Vietnamese_CI_AS  NOT NULL,
  [Name] nvarchar(100) COLLATE Vietnamese_CI_AS  NULL,
  [Price] decimal(18,2)  NULL
)
GO

ALTER TABLE [dbo].[Service] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Service
-- ----------------------------
INSERT INTO [dbo].[Service] ([Id], [Name], [Price]) VALUES (N'a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d', N'Sample Name 1', N'100.00')
GO

INSERT INTO [dbo].[Service] ([Id], [Name], [Price]) VALUES (N'b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e', N'Sample Name 2', N'101.00')
GO

INSERT INTO [dbo].[Service] ([Id], [Name], [Price]) VALUES (N'c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f', N'Sample Name 3', N'102.00')
GO

INSERT INTO [dbo].[Service] ([Id], [Name], [Price]) VALUES (N'd4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a', N'Sample Name 4', N'103.00')
GO

INSERT INTO [dbo].[Service] ([Id], [Name], [Price]) VALUES (N'e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b', N'Sample Name 5', N'104.00')
GO


-- ----------------------------
-- Primary Key structure for table Booking
-- ----------------------------
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [PK__Booking__3214EC07FBC26F4F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table BookingDetail
-- ----------------------------
ALTER TABLE [dbo].[BookingDetail] ADD CONSTRAINT [PK__BookingD__3214EC0758A05EB5] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Customer
-- ----------------------------
ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [PK__Customer__3214EC07FC9AA8C1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Invoice
-- ----------------------------
ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [PK__Invoice__3214EC07A896690A] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Manager
-- ----------------------------
ALTER TABLE [dbo].[Manager] ADD CONSTRAINT [PK__Manager__3214EC07A6E8E60B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Review
-- ----------------------------
ALTER TABLE [dbo].[Review] ADD CONSTRAINT [PK__Review__3214EC070BD388DB] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Room
-- ----------------------------
ALTER TABLE [dbo].[Room] ADD CONSTRAINT [PK__Room__3214EC071F58699E] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Service
-- ----------------------------
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK__Service__3214EC07BF755FD8] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Booking
-- ----------------------------
ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [FK__Booking__Custome__2C3393D0] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Booking] ADD CONSTRAINT [FK__Booking__RoomId__2D27B809] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table BookingDetail
-- ----------------------------
ALTER TABLE [dbo].[BookingDetail] ADD CONSTRAINT [FK__BookingDe__Servi__32E0915F] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[BookingDetail] ADD CONSTRAINT [FK__BookingDe__Booki__33D4B598] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Booking] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Invoice
-- ----------------------------
ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [FK__Invoice__Booking__300424B4] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Booking] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Review
-- ----------------------------
ALTER TABLE [dbo].[Review] ADD CONSTRAINT [FK__Review__Customer__36B12243] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[Review] ADD CONSTRAINT [FK__Review__RoomId__37A5467C] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

