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

        //Offensive stats
        [Required(ErrorMessage = "Please enter the number of at-bats.")]
        public int NumAtBats { get; set; }
        [Required(ErrorMessage = "Please enter the number of hits.")]
        public int NumHits { get; set; }
        [Required(ErrorMessage = "Please enter the number of hitting strikeouts.")]
        public int NumHittingStrikeouts { get; set; }
        [Required(ErrorMessage = "Please enter the number of homeruns.")]
        public int NumHomeruns { get; set; }
        [Required(ErrorMessage = "Please enter the number of RBIs.")]
        public int NumRBI { get; set; }
        [Required(ErrorMessage = "Please enter the number of walks.")]
        public int NumWalks { get; set; }

        //Defensive stats
        [Required(ErrorMessage = "Please enter a position.")]
        public string Position { get; set; }
        public int NumPlays { get; set; }
        public int NumErrors { get; set; }

        //Pitching stats
        public int NumInningsPitched { get; set; }
        public int NumEarnedRunsAllowed { get; set; }
        public int NumWalksAllowed { get; set; }
        public int NumPitchingStrikeouts { get; set; }
        public int NumHomerunsAllowed { get; set; }

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
    }
}
