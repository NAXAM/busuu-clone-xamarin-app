using System;
using System.Collections.Generic;
using Naxam.Busuu.Review.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Naxam.Busuu.Review.Services
{
    public class ReviewService : IReviewService
    {
        string[] words = { "hello", "I'm", "What's your name?", "Where are you from?" };

        public Task<List<ReviewModel>> GetAllReview()
        {
            var random = new Random();

            var items = new List<ReviewModel>();
            for (int i = 0; i < 100; i++)
            {
                items.Add(new ReviewModel
                {
                    Id = i,
                    Title = (char)random.Next(65, 90) + words[random.Next(0, words.Length - 1)] + " " + i,
                    SubTitle = words[random.Next(0, words.Length - 1)],
                    StrengthLevel = random.Next(0, 4),
                    IsFavorite = false,
                    ImgWord = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300),
                    SoundUrl = "Nokia-tune-Nokia-tune.mp3",
                    IsRead = false,
                    Sample = new ReviewModel
                    {
                        Title = (char)random.Next(65, 90) + words[random.Next(0, words.Length - 1)] + " " + i,
                        SubTitle = words[random.Next(0, words.Length - 1)],
                    }
                });
            }

            return Task.FromResult(items);
        }

        public Task<ReviewModel[]> SearchReview(ReviewModel[] my_AllReviews, string keyword)
        {
            return Task.FromResult(my_AllReviews.Where(d => d.Title.Contains(keyword) || d.SubTitle.Contains(keyword) || d.Sample.Title.Contains(keyword) || d.Sample.SubTitle.Contains(keyword)).ToArray());
        }
    }
}
