using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrototype.Models
{
    public class DBPopulator
    {

        Random r;

        public DBPopulator()
        {
            r = new Random();
        }

        public void PopluateAllTeams(RosterContext context, int numPlayers)
        {
            foreach (var team in context.Teams)
            {
                PopulatePlayers(context.Players, team.TeamId, numPlayers);
            }
            context.SaveChanges();
        }

        public void PopulatePlayers(DbSet<Player> players, int TeamID, int numPlayers)
        {
            Random r = new Random();
            for (int i = 0; i < numPlayers; i++)
            {
                Player p = new Player();
                p.TeamId = TeamID;
                string name = names[r.Next(0, names.Length)];
                p.FirstName = name.Substring(0, name.IndexOf(' '));
                p.LastName = name.Substring(name.IndexOf(' ') + 1);
                p.DOB = DateTime.Now;
                p.DOB -= p.DOB - new DateTime(year: 2012, r.Next(1, 12), r.Next(1, 30));
                p.Height = r.Next(50, 65);
                p.Weight = r.Next(100, 190);
                p.NumAtBats = 0;
                p.NumHits = 0;
                p.NumHittingStrikeouts = 0;
                p.NumHomeruns = 0;
                p.NumRBI = 0;
                p.NumWalks = 0;
                p.Position = positions[r.Next(0, positions.Length)];
                players.Add(p);
            }
        }

        /// <summary>
        /// Dont Use rly
        /// </summary>
        /// <param name="context"></param>
        public void PopulatePlayerStats(RosterContext context)
        {
            Random r = new Random();
            foreach (Player p in context.Players)
            {
                p.NumAtBats = r.Next(0, 50);
                p.NumHits = r.Next(0, p.NumAtBats);
                p.NumHittingStrikeouts = r.Next(0, p.NumAtBats - p.NumHits);
                p.NumHomeruns = r.Next(0, p.NumHits / 2);
                p.NumRBI = r.Next(0, p.NumHits / 2);
                p.NumWalks = r.Next(0, p.NumAtBats - (p.NumHits - 1));
                p.NumPlays = r.Next(0, 50);
                p.NumErrors = r.Next(0, p.NumErrors);
                if (p.Position == "Pitcher")
                {
                    p.NumInningsPitched = r.Next(0, 50);
                    p.NumEarnedRunsAllowed = r.Next(0, p.NumInningsPitched * 2);
                    p.NumWalksAllowed = r.Next(0, p.NumInningsPitched * 3);
                    p.NumPitchingStrikeouts = r.Next(0, p.NumInningsPitched * 2);
                    p.NumHomerunsAllowed = r.Next(0, p.NumInningsPitched / 2);
                }
                context.Players.Update(p);
            }
            context.SaveChanges();
        }

        public void SimulateSeason(RosterContext context, int year)
        {
            Random r = new Random();

            // 12 Weeks in Spring, Every Satuday
            List<DateTime> SaturdaysForTheBoys = new List<DateTime>();
            DateTime temp = new DateTime(year, 3, 1);

            while (SaturdaysForTheBoys.Count < 12)
            {
                if (temp.DayOfWeek == DayOfWeek.Saturday)
                {
                    SaturdaysForTheBoys.Add(new DateTime(temp.Year, temp.Month, temp.Day, 12, 30, 0));
                }
                temp = temp.AddDays(1);
            }

            string[] locations = new string[] { "Veterans Park", "Bachmann Park", "Stephen C. Beachy Central Park" };

            // Generate Games/Matches
            foreach (DateTime date in SaturdaysForTheBoys)
            {
                List<int> teamScramble = new List<int>();
                foreach (Team t in context.Teams)
                {
                    teamScramble.Add(t.TeamId);
                }

                List<(int, int)> matchups = new List<(int, int)>();
                int count = context.Teams.Count() / 2;
                for (int i = 0; i < count; i++)
                {
                    var a = r.Next(0, teamScramble.Count());
                    var teamA = teamScramble.ElementAt(a);
                    teamScramble.RemoveAt(a);

                    var b = r.Next(0, teamScramble.Count());
                    var teamB = teamScramble.ElementAt(b);
                    teamScramble.RemoveAt(b);

                    matchups.Add((teamA, teamB));
                }

                // After that mess, we have the matchups 

                foreach (var matchup in matchups)
                {
                    var game = new Game()
                    {
                        DateTime = date,
                        Location = locations[r.Next(0, locations.Length)]
                    };
                    // Save game to get auto ID
                    context.Games.Add(game);
                    context.SaveChanges();

                    var scoreA = r.Next(0, 18);
                    var scoreB = r.Next(0, 18);

                    var matchA = new Match()
                    {
                        GameId = game.GameId,
                        TeamId = matchup.Item1,
                        Score = scoreA
                    };

                    var matchB = new Match()
                    {
                        GameId = game.GameId,
                        TeamId = matchup.Item2,
                        Score = scoreB
                    };

                    if (scoreA > scoreB)
                    {
                        matchA.Outcome = Outcome.Win;
                        matchB.Outcome = Outcome.Loss;
                    }
                    else if (scoreA < scoreB)
                    {
                        matchA.Outcome = Outcome.Loss;
                        matchB.Outcome = Outcome.Win;
                    }
                    else
                    {
                        matchA.Outcome = Outcome.Tie;
                        matchB.Outcome = Outcome.Tie;
                    }

                    context.Matches.AddRange(matchA, matchB);
                }
            }
            context.SaveChanges();

            int numMax = 50;
            int numMin = 10;

            foreach (Player p in context.Players)
            {
                int atBats = r.Next(numMin, numMax);
                p.NumAtBats += atBats;

                int hits = r.Next(numMin, atBats);
                p.NumHits += hits;
                p.NumHittingStrikeouts += r.Next(0, atBats - hits);
                p.NumHomeruns += r.Next(0, hits / 2);
                p.NumRBI += r.Next(0, hits / 2);
                p.NumWalks += r.Next(0, atBats - (hits - 1));
                
                int plays = r.Next(numMin, numMax);
                p.NumPlays += plays;
                p.NumErrors += r.Next(0, plays);
                if (p.Position == "Pitcher")
                {
                    int inningsPitched = r.Next(numMin, numMax);
                    p.NumInningsPitched += inningsPitched;
                    p.NumEarnedRunsAllowed += r.Next(0, inningsPitched * 3);
                    p.NumWalksAllowed += r.Next(0, inningsPitched * 3);
                    p.NumPitchingStrikeouts += r.Next(0, inningsPitched * 3);
                    p.NumHomerunsAllowed += r.Next(0, inningsPitched / 2);
                }
                context.Players.Update(p);
            }

            context.SaveChanges();
        }

        string[] positions = new string[]
        {
            "Pitcher","Catcher","First Baseman","Second Baseman","Third Baseman","Shortstop","Left Field","Center Field","Right Field"
        };

        string[] names = new string[]
        {
            "Tom Howard","Lindsey Tanner","Steven Gonzales","Cole Larsen","Katherine Carter","Casey Barrett","Patricia Murillo","Meagan Boyd","Andrew Harrison V","Brittany Martin","Adam Duke","Jennifer Robinson","Richard Nguyen","Michael Payne PhD","Sara Wood","Susan Martin","Jessica Barnett","Kathryn Huffman","David Davis","Shannon Russo","Lisa Ramirez","Ian Hampton","Emily Shepherd","Michael Johnson","Lori Watson","Shawn Brown","Mike Munoz","Kyle Garza","Michael Ayala","John Johnson","Amy Watson","Luis Pollard","Justin Chapman","Brian Hill","Theresa Mercado","Nicholas Levy","Todd Moss","Justin Chase","Christine Bush","Linda Pineda","John Marks","Jennifer Harris","Jennifer Mejia","Michael Burnett","Kristina Elliott","Joseph Wilkerson Jr.","Michele Wilson","Anna Wise","Courtney Rodriguez","Mr. William Taylor","Daniel Hardin","Samuel Jones","Phyllis Burgess","Paige Mercado","Cheryl Martinez","Craig Nguyen","John Williams","Jaclyn Johnson","Ryan Rodriguez","Kristopher Wise","Robert Schneider II","Erin Murphy","Carl Tapia","Dale George","Nicholas Griffith","Jim Moreno","Sandra Lewis PhD","Kyle Owens","James Wright","Brandon Santos","Kathleen Maldonado","Traci Martinez","Steven Massey","Justin Johnston","Wesley Pittman","Lee Kelly","Natalie Wise","Kent Bradley","Jason Walters","Nicole Vincent","Heather Wilson","Robert Brown","Corey Fernandez","Ashley Sanders","Nicole Henry","Julie Carrillo","James Paul","Jerry Williams","Matthew Willis","Mark Braun","Debbie Martin","Kristin Taylor","James Henry","Tara Hoffman","Ronald Thomas","Kenneth Smith","Ray Harrington","Emily Campos","Julie Williams","Amber Parks","James Lawson","Paul Valenzuela","Krista Strong","Melissa Le","Jeffrey Wilcox","Gregory Sosa","Lori Horn","Destiny Vargas","Sharon Mccoy","Steven Norris","William Morgan","Michelle Arnold","Angela Smith","George Thornton","Anna Kennedy","Kristin Williamson","Tamara Arellano","Debbie Hunt","Mr. Christopher Dillon","Donald Smith","Laura Anderson","Randy Dennis","Betty Ortiz","Paula Hall","Michelle Alvarado","Nancy Cook","Patrick Martinez","Brian Taylor","Kelly Anthony PhD","Jill Lee","Matthew Berger","Vanessa Camacho","Carlos Velazquez","Christopher Patterson","Adam Pittman","Joanne Harmon","Francisco Armstrong","Scott Gordon","Frank Woods","Travis Hernandez","Sergio Williams","Troy Williams","Mr. Chad Buchanan","Randall Johnson","Victor Williams","Antonio Brown","Cassandra Taylor","Ricky Butler","Carlos Wright","Katrina Dixon","James Baker","Anthony Thompson","Janet Singh","Bethany Valencia","Felicia Stewart","Eric Jones","Thomas Anderson Jr.","John Horton","Shannon Baird","Amy Charles","John Young","Laura Luna","Peter Dennis","Angel Casey","Luke Gay","Miss Christine Powers","Olivia Mcconnell","Dr. Beth Pierce","Jason Becker","Jasmine Brewer","Cheyenne Rodriguez","Tom Brewer","Christopher Smith","Alan Phillips","Benjamin Chambers","Denise Rodriguez","John Lawson","David Williams","James Miranda","Randall Callahan","Elizabeth Robinson","Howard Rodriguez","Wesley Nguyen","Kenneth Long","Dustin Smith","Tara King","Charles Brooks","Jeremy Anderson","Karen Larson","Wendy Brown","Pamela Stevenson","Michael Perry","Beverly Chapman","Ronnie Parks","Danielle Hill","Zachary Jimenez","Richard Arnold","Mr. Zachary Odom",
            "Christopher Chang","Kimberly Sloan","Elizabeth Wheeler","Brittany Miller","Nicholas Johnson","Jeff Ruiz","Lisa Thomas","David Baker","Dr. Kayla Brown DVM","David Silva","Matthew Hopkins","Samantha Murphy","Dr. Larry Myers","Gregory Johnson","Ronald Bright","James Foster","Christopher Mccoy","Daniel Phillips","Gary Petersen","David Harvey","Michelle Flores",
            "Shaun Gonzales","Shelby Payne","Charles Owens","Jeremy Bell","Parker King","Raymond Wagner","Brittany Bates","Elaine Baldwin","Kayla Bradshaw","Roger Obrien","Mr. James Wong","Kyle Ortiz","Philip Butler","James Singh","Justin Dixon","Justin Estrada","Brian Patton MD","Sean Bennett","Christopher Brown","Sarah Anthony","Courtney Edwards","David Mcdonald","Monica Lutz","Kathleen Hernandez","Nicole Blair","Isabel Sharp","Joseph Beard","Dr. Robert Harris IV","Caitlin Smith","Paul Banks","Laurie Bass","Steven Suarez","Michelle Cole","Anthony Moore","Melissa Church","William Brown","Jason Martinez","Alice Peterson","Shannon Lee","Roy Martinez","Frank Williams","Ryan May","Mark Wright","Brian Hall","Aaron Wall","Terry Gutierrez","Angela Jackson","Jeremy Cordova","Stephanie Johnson","Steven Gonzalez","Jacob Alvarado","Ann Clark","Gloria Nichols","Richard Thompson","Samuel Coleman","Rachel Klein","Robert Brown","Brian Ellis","Michael Lambert","Nicole Holder","Breanna Trujillo","Justin Johnson","Emily Cooper","Jessica Franklin","Howard Baker","Tina Hernandez","Jessica Berg","Sara Boyd","Austin Jacobs","Taylor Johnson","Bruce Hill","Cynthia Wilson","Kimberly Thomas","Jessica Obrien","Gary Cannon","Henry Murphy","Virginia Dixon","Cody Rivera","Christine Waters","Joshua Mclaughlin","Tyler Winters","Devin Mclean","Alfred Irwin","Jessica Flores","Troy Roth","Ashley Bowman","Janet Gomez","Cristian Walker","Rebecca Berry","Colleen Dunn","Mrs. Kristi Daniel","Lauren Bentley","Terry Pace","Kara Hammond","Christopher Kelly","Courtney Miller","Vincent Smith","Steven Brown","Kyle Gonzales","Mark Mercer","Travis Harper","Teresa Adkins","Dominique Reed","Deanna Payne","Kyle Bell","Darren Dickerson","Dawn Roberts","Robin Rodgers","Tanya Ortega","Christian Jones","Shawn Watts","Brittany Ross","Courtney Bishop","Peter Carter","Lindsay Daugherty","Raymond Bishop","Yolanda Huber","Charles Santos","Barbara Rivera","Aaron Chapman","Tiffany Roberts","Joshua Cross","Henry Merritt","Thomas Hudson","Scott Thomas","Heather Hall","Jacob Williams","Michael Smith","Jordan Dudley","Michael Curtis","Karen Cook","Bryan White","Jason Salazar","Marc Brown","Jennifer Moran","Richard Russo","Jennifer Mcbride","Joe Harrington","Jeremy Fry","Sarah Taylor","Jessica Anderson","Calvin Tran","Stephanie Nguyen","Peter Dixon","Savannah Taylor","Stephen Norman","Shelly Lee","Jason Lowery","Ricardo Hall","Timothy Moreno","Brian Johnson","William Daniels","Daniel Taylor","Susan Juarez","Rose Mathews","Michael Blake","Christopher Hawkins","Randall Harrison","Kaitlyn Mcguire","Diana Miller","David Payne","Roy Andersen","Paul Berger","Dana Lynch","Erin Johnston","Melissa Sullivan","Jamie Farmer","Casey Ward","Douglas Woods","Linda Roberts","Christopher Sullivan","Sandra Valentine","Jessica James","Jessica Mullins","Michael Gibson","Mary Brooks","Lisa Mccarty","James Madden","Michelle Diaz","Tony Zamora","Jill Hill","Amy Mcdonald","Ruth Mejia","Kimberly Vargas","Olivia Guerra","Jessica Bernard MD","Jason Dixon","Amanda Austin","Nancy White","Robin Tate","Susan Fitzgerald","Jeremy Young","Cindy Ayala","Thomas Thompson","Mary George","Angela Warren","Christine Ramirez","Christine Bennett","Benjamin Chase","Patrick Deleon","Kenneth Obrien","Miguel Gonzalez","Monica Medina MD","Leslie Reed","Lindsey Velazquez","Jasmine King","Brandon Scott","Scott Greene","Charlotte Rasmussen",
            "Amber King","Andrew Medina","Jenna Leach","Mrs. Alice Jordan","Steve Hatfield","Billy Ramos","Michael Jones","Erica Osborne","Laurie Hall","Joseph Johnson","Tara Harris","Whitney Burns","Timothy Sparks","Kristen Gutierrez","Timothy Spencer","Jason Reid","John Boyd","David Clark","Richard Mendoza","Paula Martinez","Carla Kirk","Benjamin Brown","Nicholas Salinas","Jonathan Walker DDS","Jennifer Tyler","Lisa Smith","Anthony Brown","James Randolph","Bailey Park","Lauren Nicholson","Linda Wells","Savannah Morales","Sherry Macias","Brooke Robbins","Elizabeth Thomas","Sean Thomas","Christopher Sullivan","Troy Nelson","Elizabeth Hoover","Eric Lambert","Colleen Weber","Ann Smith","Valerie Smith","Jerry Day","Marvin Dalton","Brandy Reid","Traci Fuller","Stephanie Escobar","Lisa Williams","Brian Harris","Scott Rojas","Nicole Mitchell","Mr. Donald Daniels","Mary Wood","Patrick Long","Joshua Webb","Mitchell Davis","Keith Patel","Ronald Andrews","Robert Hoffman","Ronald Walker","Crystal Rivers","Lori Wood","Johnny Solomon","Jennifer Glass MD","Christina Walker","Jackie Marshall","Kevin Hawkins","Daniel Blair","Rachel Flowers","Maria Hopkins","Alexandria Coleman","Patrick Flynn","Mark Henderson","William Nolan","April Wells","Laura Mason","Angel Wood","Jason Reed","James Odonnell","Danny Ross","Dennis Johnson","Krista Stephens","Ricky Anderson","Eric Bruce","Brianna Moon","Daniel Barker","Tiffany Patrick","Bradley Peterson","James Zuniga","Allen Ramirez","Terri Myers","Alex Hester","Gregory Johnson","James Miller","Mary Rodriguez","Kristen Collins","Michael Davis","Phillip Martin","Brian Perez","Lauren Kelly","Anna Patterson","Heather Wilson","Chelsea Cooper DDS","Wanda Davis","Kristin King","Heather Gutierrez","Kimberly Nicholson","Jennifer Boone","Julia Lewis","Michael Lane","Shelby Payne","Brian Murphy","Elizabeth Black","Kenneth Watson","Christina Stanton","Kendra Porter","Jason Garcia","Alejandra Vasquez","Yvonne Lewis","William Gonzalez","Teresa Ortiz","Susan Donovan","Chad Taylor","Kimberly Taylor","Erin Rose","Amanda Arroyo","Zoe Duncan","Barbara Perez","Terri Robinson","Mrs. Renee Lee","Tonya Hart","Meghan Harris","Joanna Jones","Jared Adams","James Alvarado","Chad Hart","Christine Brady","Ellen Myers","Connor Frederick","Sherry Robertson","Robert Sanchez","Sean Ellis","Jacob Stanton","Lauren Hill","Christian Bell","Leslie Garcia","Jessica Vaughn","Albert Jones","Riley Hernandez","Brandon George","Erica Scott","Mia Wilson","Stephen Shaw","William Navarro","Raymond Clark","Julie Johnson","Erika Griffin MD","Karen Blake","Dustin King","Michelle David","William Bautista","Jose Thompson","Cynthia Dickson","Andrea Pratt","Jonathan Gilbert","Dr. Christopher Nguyen","Travis Prince","Audrey Hall DDS","Sheila Steele","Vanessa Lyons","Nathaniel Robinson","Sandra Aguilar","Laura Thompson","Kara Stone","Jacob Adkins","Michele Kennedy","Samantha Estrada","Donna Miller","Heather Macias","Scott Christensen","Jason Schwartz","Michael Grant","Veronica Stanton","Gregory Frost","Miguel Miller PhD","Victor Estrada","Laura Gutierrez","Bryan Robertson","Sabrina Palmer","Rachel Moore","Suzanne Friedman","Shane Moreno","Wendy Miller","Jessica Hodges","Richard Harris","Regina Lopez","Mr. Kurt Harris",
            "Lisa Stephenson","David Flores","Jacqueline Stevens","Anna Carlson","Veronica Wilson","William Jackson","Jonathan English PhD","Rachel Marks","Samuel Brown","Anna Cole","Curtis Kelly","Stephanie Hayes","Crystal Vargas","Jerry Branch","Anna Reed","Sandra Sullivan","Tiffany Price","Lindsay Pacheco","Dustin Walls","Karl Erickson","Steven Williams","Ashley Guzman","Michael Payne","Jeffrey Olson","Kenneth Davis","Steven Torres","Rick Lowe","Kevin Robinson","Kim Sutton","Parker Morris","Emma Massey","Diane Lopez","Brianna Robinson","Gabrielle Vasquez","Mr. Richard Park","Lori Martinez","Spencer Faulkner","Kathy Riggs","George Hicks","Kristen Benson","Christopher King","John Fox","Heather Ramos","Gina Underwood","Kiara Haynes","Ashley Grimes","Kristina Francis","Julie Morrow","Katie Black","Victoria Williams","Nicholas Downs","Raymond Fletcher","Richard Martinez","David Hanson","Jordan Lopez","Scott Nichols","Alyssa Griffin","Elizabeth Peters","Ryan Wheeler DDS","Mario Carter","Lindsey Kelly","Lindsey Strong","Lauren Stokes","Larry Aguilar MD","Stacey Donaldson","Wendy Camacho","John Malone","Mary Smith","Paula Stephens","Evan Carlson",
            "Michael Morgan","Carolyn Adams","Brian Bowman","Gregory Nelson","Pamela Holmes","Jose Lloyd","Kathleen Summers","Christopher Marsh","Morgan Cooper","Michael Thompson","Jacqueline Ramos","James Cummings","Amanda Salinas","Rebekah Bauer","Mrs. Cynthia Welch","Amanda Myers","Cindy Garcia","Cody Brooks","Patricia Powell","Lynn Smith","Dylan Williams","James Cox","Mr. William Compton Jr.","Clifford Mccarty","Charles Meyer","Victoria Harris","Julia Mcguire","Sonya Brady","Cassandra Payne","Norma Deleon","Wesley Hammond","David Kramer","Susan Shaw","Lisa Houston","Adrian Martin","Amanda Patterson","Rebecca Klein","Andrea Lester","Christine Hobbs","Jacob Trevino","Autumn Hendrix","Elizabeth Collins","Anthony Melendez","Dwayne Miller","Matthew Harris DVM","Patrick Murphy","Randall Armstrong","Shelley Valdez","Sarah Strickland","Christopher Morrison","Larry Hall","Katie Wilkerson","Patrick Buckley","Maria Villa","Adam Duran","Karen White","James Carter","Kenneth Marshall","James Hood","Alison Curtis","Kristine Armstrong","Anthony Patel","David Hamilton","Megan Thompson","Eric Alexander","Linda Hull","Christopher Randolph","Marie Gray","Philip Parks","Jennifer Fernandez","Grace Forbes","Elizabeth Gomez","Mark Velasquez","Brian Lyons","Mike Clark","Denise Harris","Preston Conner","Zachary Rivers","Tammy Moreno","Olivia Brown","Michael Peterson","Phillip Campbell","Jessica Boone","Desiree Williams","Megan Stone","Brooke Holmes","Christopher Lang","Matthew Long","Sara Collins","Jessica Lopez","Shawn Villarreal","Barry Davis","Mary Estrada","Dana Berry","Mr. Manuel Barnes","Michael Smith","Katie Bailey","Janet Leonard","Phillip Moore","Jasmin Sandoval","Lisa Johnson","Dr. Alejandro Moore","Timothy Reed","Shelly Harrison","Kevin Wilson","Michael Davidson","Brian Merritt","Megan Hall","Emily Washington","Angel Norris","Angela Morris","David Macias","Erin Coffey","Jennifer Hendrix MD","Christopher Mcmillan","Patricia Watkins","David Irwin","Wayne Navarro","Alyssa Clark","Kristen Bradford","Eduardo Holmes","Eric Shaw","Nina Randall","Elizabeth Anthony","Tiffany Lee","William Peterson","Jessica Adams","Gloria Johnson","Jasmine Hudson","Kevin Jordan","Erin Baldwin","Devin Garner","Todd Gentry","Kim Sandoval","Destiny Wade","Shannon Guerrero","Carrie Rice","Andrew Durham","Matthew Sanchez","David Wheeler","Natalie Brock","Tony Noble","Matthew Peters","Mr. Paul Smith","Mary Delacruz","Felicia Bowman","Amy Harding","Crystal Page","Cathy Reyes","Jeffery Grant","Joseph Graham","Christopher Warner PhD","Jessica Miller","Brad Steele","Charles Underwood","David Hernandez","Joseph Bradley","Lance Snow","Emily Harrison","Nicole Hinton","Connor Santana","James Smith","Scott Johnson","Robert Murphy MD","Alison Stokes","Keith Everett","Brandy Best","Amanda Allen","Jared Cantrell","Megan Kane","Richard Mahoney","Amy Wheeler","Angela Stewart","Angela Francis","Robert Williams","Adrian Terrell",
            "Carolyn Price","Adrian Horn","Susan Thomas","Karen Cruz","Christopher Cooper DDS","Matthew Green","Taylor Johnson","Victoria Bennett","Leslie King","Sierra Delgado","Eduardo Francis","Crystal Banks","Edward Yates","James Smith","Joshua Taylor","James Murray","Robert Martinez","Rachel Larsen","Kellie Hayes","Mr. Preston Patton MD","Jessica Williams","Todd Johnson","Hannah Smith","Amanda Espinoza","Kyle Owens","Chelsey Watson","Angela Brown","Emily Schroeder","Richard Carson","Ethan Marsh","Christopher Smith","Nicholas Medina","Patricia Anderson","Benjamin Lynch","Scott Ramirez","Henry Braun","Alicia Nunez","Kevin Williams","Michael Evans","Bryan Edwards","Nancy Andersen","Zachary Johnson","Emily Jackson","Karen Edwards","Breanna Ruiz","Crystal Henson","Dr. Paula Maddox DVM","Donna Hernandez","Dawn Anderson","George Mejia","Erica Shah","Hunter Davis","Christopher Henry","Paul Jones","Ryan Copeland","Thomas Smith","Mrs. Jessica Freeman MD","Jennifer Jackson","Joseph Rodriguez","Belinda Obrien","Daniel Hester","Julie Coleman","Gerald Matthews","Matthew Parker","Amanda Pearson","Dale Martinez","Jessica Mathews","Evelyn Stevens","Laura Mckay","Kayla Bush","Sandra Padilla","Barry Ballard","Todd Spears","Madison Campbell","Andre Fernandez","Sarah Rojas","Daniel Clarke","Virginia Walker","Kimberly Wood","Justin Bell","Matthew Fisher","Karen Spencer","Olivia Bailey","Charles Mitchell","Kayla Williams","Angel Stanley","Andrea Oneill","Justin Freeman","Robert Kelley","Alvin Smith","Joshua Parker","Amanda Daniel","Monique Baird","Timothy Garcia","Julia Bennett","David Parker","Mitchell Spencer","Sarah Williams","Stacey Welch","Scott Ramirez","Rachel Young","David Chan","Joseph Maxwell","Rachael Hensley","Eric Walker","Erin Howe","John Richardson","Melvin Davis","Lawrence Franklin","John Mccullough","Tracy Berry","Ruben Hoover","Taylor Martin","Tammy Allen","Kevin Mendez","Ryan Doyle","Kimberly Anderson","Richard Silva","Sarah Moore","Jack Dunn","Jason Parker","Rhonda Cooper","Kimberly Lucas","Mandy Evans","Brad Mccullough","John James","Jessica Taylor","Crystal Russo"
        };
    }
}

