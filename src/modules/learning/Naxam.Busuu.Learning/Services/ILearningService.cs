using System;
using Naxam.Busuu.Learning.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Services
{
    public interface ILearningService
    {
        Task<LessonModel[]> GetAllLesson(); 
        Task<UnitModel[]> GetUnitByExercise(ExerciseModel ex);
        Task<TipModel> GetTipByUnit(UnitModel unit);
        Task<UnitModel> GetUnitByType(UnitModel.UnitType type);
        Task<ExerciseModel> GetExerciseByiId(int id);
    }
}
