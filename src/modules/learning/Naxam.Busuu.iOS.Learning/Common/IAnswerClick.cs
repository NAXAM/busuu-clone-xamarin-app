using System;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Common
{
	public interface IAnswerClick
	{
		void DidAnswer(UIView view);
		void NextAnswer(UIView view);
        void GoLearnView(UIView view);
        void ResetExercise(UIView view);
	}
}
