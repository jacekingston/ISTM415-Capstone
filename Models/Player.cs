using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrototype.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; } // Foreign Key
        //Physical stats
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a date-of-birth.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please enter a height.")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Please enter a weight.")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Please enter a position.")]
        public string Position { get; set; } = "Default Position";

        //Offensive stats
        [Required(ErrorMessage = "Please enter the number of at-bats.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumAtBats { get; set; } = 0;
        [Required(ErrorMessage = "Please enter the number of hits.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumHits { get; set; } = 0;
        [Required(ErrorMessage = "Please enter the number of hitting strikeouts.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumHittingStrikeouts { get; set; } = 0;
        [Required(ErrorMessage = "Please enter the number of homeruns.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumHomeruns { get; set; } = 0;
        [Required(ErrorMessage = "Please enter the number of RBIs.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumRBI { get; set; } = 0;
        [Required(ErrorMessage = "Please enter the number of walks.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumWalks { get; set; } = 0;

        //Defensive stats
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumPlays { get; set; } = 0;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumErrors { get; set; } = 0;

        //Pitching stats
        public int NumInningsPitched { get; set; } = 0;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumEarnedRunsAllowed { get; set; } = 0;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumWalksAllowed { get; set; } = 0;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumPitchingStrikeouts { get; set; } = 0;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        public int NumHomerunsAllowed { get; set; } = 0;

        public Team Team { get; set; }


        public string getDateOfBirthDate()
        {
            return this.DOB.ToString("MM/dd/yyyy");
        }

        public string calculateHeight()
        {
            int feet = this.Height / 12;
            int inches = this.Height % 12;
            return feet.ToString() + "'" + inches.ToString();
        }

        public decimal calculateBattingAverage()
        {
            if (this.NumAtBats > 0)
            {
                return Math.Round(Convert.ToDecimal(this.NumHits) / Convert.ToDecimal(this.NumAtBats), 3);
            }
            return 0.000m;
        }

        public decimal calculateFieldingPercentage()
        {
            if (this.NumPlays > 0)
            {
                return Math.Round(Convert.ToDecimal(this.NumPlays - this.NumErrors) / Convert.ToDecimal(this.NumPlays), 3);
            }
            return 0.000m;
        }

        public decimal calculateERA()
        {
            if (this.NumInningsPitched > 0)
            {
                return Math.Round(Convert.ToDecimal(this.NumEarnedRunsAllowed * 9) / Convert.ToDecimal(this.NumInningsPitched), 2);
            }
            return 0.00m;
        }

        public void SetGameStats(Player p)
        {
            this.NumAtBats = p.NumAtBats;
            this.NumHits = p.NumHits;
            this.NumHittingStrikeouts = p.NumHittingStrikeouts;
            this.NumHomeruns = p.NumHomeruns;
            this.NumRBI = p.NumRBI;
            this.NumWalks = p.NumWalks;
            this.NumPlays = p.NumPlays;
            this.NumErrors = p.NumErrors;
            this.NumInningsPitched = p.NumInningsPitched;
            this.NumEarnedRunsAllowed = p.NumEarnedRunsAllowed;
            this.NumPitchingStrikeouts = p.NumPitchingStrikeouts;
            this.NumWalksAllowed = p.NumWalksAllowed;
            this.NumHomerunsAllowed = p.NumHomerunsAllowed;
        }

        public void AddGameStats(Player p)
        {
            this.NumAtBats += p.NumAtBats;
            this.NumHits += p.NumHits;
            this.NumHittingStrikeouts += p.NumHittingStrikeouts;
            this.NumHomeruns += p.NumHomeruns;
            this.NumRBI += p.NumRBI;
            this.NumWalks += p.NumWalks;
            this.NumPlays += p.NumPlays;
            this.NumErrors += p.NumErrors;
            this.NumInningsPitched += p.NumInningsPitched;
            this.NumEarnedRunsAllowed += p.NumEarnedRunsAllowed;
            this.NumPitchingStrikeouts += p.NumPitchingStrikeouts;
            this.NumWalksAllowed += p.NumWalksAllowed;
            this.NumHomerunsAllowed += p.NumHomerunsAllowed;
        }

        /// <summary>
        /// Makes this player a dummy player to build stats, does not change parameter Player
        /// </summary>
        /// <param name="p"></param>
        public void SetAsDummy(Player p)
        {
            this.FirstName = p.FirstName;
            this.LastName = p.LastName;
            this.DOB = p.DOB;
            this.Weight = p.Weight;
            this.Height = p.Height;
            this.Position = p.Position;
            this.NumAtBats = 0;
            this.NumHits = 0;
            this.NumHittingStrikeouts = 0;
            this.NumHomeruns = 0;
            this.NumRBI = 0;
            this.NumWalks = 0;
            this.NumPlays = 0;
            this.NumErrors = 0;
            this.NumInningsPitched = 0;
            this.NumEarnedRunsAllowed = 0;
            this.NumPitchingStrikeouts = 0;
            this.NumWalksAllowed = 0;
            this.NumHomerunsAllowed = 0;
        }
    }
}
