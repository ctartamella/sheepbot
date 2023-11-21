CREATE TABLE [dbo].[event_length] (
    [id]             BIGINT NOT NULL,
    [length]         INT    NOT NULL,
    [length_unit_id] INT    NOT NULL
);
GO

ALTER TABLE [dbo].[event_length]
    ADD CONSTRAINT [FK_event_length_length_unit] FOREIGN KEY ([length_unit_id]) REFERENCES [dbo].[length_unit] ([id]);
GO

ALTER TABLE [dbo].[event_length]
    ADD CONSTRAINT [PK_race_length] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[event_length]
    ADD CONSTRAINT [CK_race_length_positive] CHECK ([length]>(0));
GO

