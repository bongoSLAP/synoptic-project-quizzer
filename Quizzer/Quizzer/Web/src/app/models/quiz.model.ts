import { QuestionInfo } from "./question-info.model";
import { Question } from "./question.model";

export class Quiz {
    public id: string;
    public title: string;
    public description: string;
    public questions: QuestionInfo[]
}
