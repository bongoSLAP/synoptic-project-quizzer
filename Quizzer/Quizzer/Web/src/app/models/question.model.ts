import { Answer } from "./answer.model";
import { Quiz } from "./quiz.model";

export class Question {
    public Id: string;
    public Text: string;
    public QuestionIndex: number;
    public QuizId: string;
    public Quiz: Quiz;
    public Answers: Answer[];
}
