using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrollment.MigrationTool.Migrations
{
    /// <inheritdoc />
    public partial class Nlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (
                @"SET ANSI_NULLS ON
                    GO

                    SET QUOTED_IDENTIFIER ON
                    GO

                CREATE TABLE [dbo].[NLog_Messages](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [Application] [nvarchar](50) NOT NULL,
	                [Logged] [datetime] NOT NULL,
	                [Level] [nvarchar](50) NOT NULL,
	                [Message] [nvarchar](max) NOT NULL,
	                [UserName] [nvarchar](250) NULL,
	                [Logger] [nvarchar](250) NULL,
	                [Callsite] [nvarchar](max) NULL,
	                [Exception] [nvarchar](max) NULL,
                 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                GO"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE [dbo].[NLog_Messages] GO");
        }
    }
}
