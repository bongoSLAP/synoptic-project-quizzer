import { Answer } from "./answer.model";
import { Quiz } from "./quiz.model";

export class Question {
    public id: string;
    public text: string;
    public questionIndex: number;
    public quizId: string;
    public quiz: Quiz;
    public answers: Answer[];
}
