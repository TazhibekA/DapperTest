using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Dapper;

namespace DapperTest
{
    class Program
    {
        // Скрипты в конце!
        static void Main(string[] args)
        {
   
            var providerName = ConfigurationManager.ConnectionStrings["DapperConnection"].ProviderName;

            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);

            string insertToPlayerSql = "insert into Player values (@Id,@FullName,@TeamId,@Role)";
            string selectTeamSql = "select * from Team";
            string selectPlayerSql = "select * from Player";
            string deletePlayerSql = "DELETE FROM Player WHERE Id=@Id";
            string editPlayerSql = "UPDATE Player SET FullName = @FullName, TeamId = @TeamId, Role= @Role WHERE Id = @Id; ";

            using (var connection = providerFactory.CreateConnection()) {
                connection.ConnectionString= ConfigurationManager.ConnectionStrings["DapperConnection"].ConnectionString;

             

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1 - Insert");
                    Console.WriteLine("2 - All players");
                    Console.WriteLine("3 - Player id");
                    Console.WriteLine("4 - Delete");
                    Console.WriteLine("5 - Edit");

 

                    string choose = Console.ReadLine();
                    int chooseInt;
                    bool success = int.TryParse(choose, out chooseInt);

                    if (success == true && chooseInt <= 5)
                    {
                        switch (chooseInt) {
                            case 1:
                                Console.Clear();
                                var team = connection.Query<Team>(selectTeamSql);

                                foreach (var item in team) {
                                    Console.WriteLine("{0}. {1}", item.Id, item.Name); 
                                }
                                Console.Write("Choose team ID: ");
                                string chooseId = Console.ReadLine();
                                int chooseIdInt;
                                bool successId = int.TryParse(chooseId, out chooseIdInt);
                                if (success == false && chooseIdInt <= team.ToList().Count) {
                                    Console.WriteLine("Enter 1-{0}", team.ToList().Count);
                                    Console.Read();
                                    break;
                                }
                                Console.Write("Full Name: ");
                                string fullName = Console.ReadLine();
                                Console.Write("Role: ");
                                string role = Console.ReadLine();
                                var player = new Player
                                {
                                  
                                    FullName = fullName,
                                    TeamId = chooseIdInt,
                                    Role=role
                                };
                                var result = connection.Execute(insertToPlayerSql, player);
                                if (result != 1)
                                {
                                    Console.WriteLine("Error!");
                                }
                                else {
                                    Console.WriteLine("Inserted!");
                                }
                                Console.Read();
                                break;
                            case 2:
                                Console.Clear();
                                var secondPlayers = connection.Query<Player>(selectPlayerSql);
                                var secondTeams = connection.Query<Team>(selectTeamSql);


                                foreach (var item in secondPlayers)
                                {
                                    foreach (var itemTeam in secondTeams)
                                    {
                                        if (item.TeamId == itemTeam.Id)
                                            Console.WriteLine("{0}. {1}     {2}     {3}", item.Id, item.FullName, item.Role, itemTeam.Name);
                                    }
                                }
                                break;
                            case 3:



                                Console.Clear();
                                var players = connection.Query<Player>(selectPlayerSql);
                                var teams = connection.Query<Team>(selectTeamSql);
                                Console.WriteLine("Enter player Id: ");
                                string stringId = Console.ReadLine();
                                int intId;
                                bool successIntId = int.TryParse(stringId, out intId);
                                if (successIntId == false && intId <= players.ToList().Count)
                                {
                                    Console.WriteLine("Enter 0-{0}", players.ToList().Count);
                                    Console.Read();
                                    break;
                                }

                                foreach (var item in players.ToList())
                                {
                                    foreach (var itemTeam in teams)
                                    {
                                        if (item.TeamId == itemTeam.Id && item.Id == intId)
                                            Console.WriteLine("Full Name: {0}        Role: {1}     Team name: {2}", item.FullName, item.Role, itemTeam.Name);
                                    }
                                }

                                Console.Read();
                                break;
                            case 4:
                                Console.Clear();
                                var thirdPlayers = connection.Query<Player>(selectPlayerSql);
                                var thirdTeams = connection.Query<Team>(selectTeamSql);


                                foreach (var item in thirdPlayers)
                                {
                                    foreach (var itemTeam in thirdTeams)
                                    {
                                        if (item.TeamId == itemTeam.Id)
                                            Console.WriteLine("{0}. {1}     {2}     {3}", item.Id, item.FullName, item.Role, itemTeam.Name);
                                    }
                                }
                                Console.WriteLine("Enter player Id: ");
                                string stringPlayerId = Console.ReadLine();
                                int intPlayerId;
                                bool successPlayerIntId = int.TryParse(stringPlayerId, out intPlayerId);
                                if (successPlayerIntId == false && intPlayerId <= thirdPlayers.ToList().Count)
                                {
                                    Console.WriteLine("Enter 0-{0}", thirdPlayers.ToList().Count);
                                    Console.Read();
                                    break;
                                }

                                var firstResult = connection.Execute(deletePlayerSql, new { Id = intPlayerId });
                                if (firstResult != 1)
                                {
                                    Console.WriteLine("Error!");
                                }
                                else
                                {
                                    Console.WriteLine("Deleted!");
                                }
                                Console.Read();

                                break;
                            case 5:
                                Console.Clear();
                                var fourthPlayers = connection.Query<Player>(selectPlayerSql);
                                var fourthTeams = connection.Query<Team>(selectTeamSql);


                                foreach (var item in fourthPlayers)
                                {
                                    foreach (var itemTeam in fourthTeams)
                                    {
                                        if (item.TeamId == itemTeam.Id)
                                            Console.WriteLine("{0}. {1}     {2}     {3}", item.Id, item.FullName, item.Role, itemTeam.Name);
                                    }
                                }

                                Console.WriteLine("Enter player Id: ");
                                string stringPId = Console.ReadLine();
                                int intPId;
                                bool successPIntId = int.TryParse(stringPId, out intPId);
                                if (successPIntId == false && intPId <= fourthPlayers.ToList().Count)
                                {
                                    Console.WriteLine("Enter 0-{0}", fourthPlayers.ToList().Count);
                                    Console.Read();
                                    break;
                                }
 
                                foreach (var item in fourthTeams)
                                {
                                    Console.WriteLine("{0}. {1}", item.Id, item.Name);
                                }
                                Console.Write("Choose team ID: ");
                                string editTeamId = Console.ReadLine();
                                int editTeamIdInt;
                                bool successeditId = int.TryParse(editTeamId, out editTeamIdInt);
                                if (successeditId == false && editTeamIdInt <= fourthTeams.ToList().Count)
                                {
                                    Console.WriteLine("Enter 1-{0}", fourthTeams.ToList().Count);
                                    Console.Read();
                                    break;
                                }
                                Console.Write("Full Name: ");
                                string editFullName = Console.ReadLine();
                                Console.Write("Role: ");
                                string editRole = Console.ReadLine();
                    
                                var editResult = connection.Execute(editPlayerSql, new { FullName= editFullName, TeamId= editTeamIdInt,Role=editRole,Id= intPId });
                                if (editResult != 1)
                                {
                                    Console.WriteLine("Error!");
                                }
                                else
                                {
                                    Console.WriteLine("Edited!");
                                }
                                Console.Read();

                                break;
                        }
                        Console.Read();
                    }
                    else {
                        Console.WriteLine("Enter 1-5!");
                    }

                }

                //                CREATE TABLE[dbo].[Player]
                //        (

                //   [Id] INT        IDENTITY(1, 1)   NOT NULL,
                //    [FullName] NVARCHAR(50) NOT NULL,

                //   [TeamId]   INT NOT NULL,
                //    [Role] NVARCHAR(50) NULL,
                //    PRIMARY KEY CLUSTERED([Id] ASC),
                //    FOREIGN KEY([TeamId]) REFERENCES[dbo].[Team] ([Id])
                //);

                //                CREATE TABLE[dbo].[Team]
                //        (

                //   [Id] INT        IDENTITY(1, 1) NOT NULL,

                //[Name] NCHAR(10) NULL,
                //    PRIMARY KEY CLUSTERED([Id] ASC)
                //);



                Console.Read();
            }

        }
    }
}
